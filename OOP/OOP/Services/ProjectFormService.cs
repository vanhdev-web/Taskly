using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using OOP.Models;
using OOP.Services;

namespace OOP.Services
{
    internal class ProjectsService
    {
        public List<Project> Projects { get; set; }
        private TaskManager _taskManager;

        public ProjectsService()
        {
            Projects = new List<Project>();
            _taskManager = TaskManager.GetInstance();
        }

        public List<Project> LoadProjects()
        {
            using (var context = new TaskManagementDBContext())
            {
                var query = from p in context.Projects
                            select new Project
                            {
                                projectID = p.projectID,
                                projectName = p.projectName,
                                projectDescription = p.projectDescription,
                                AdminID = p.AdminID
                            };
                Projects = query.ToList();
                Console.WriteLine($"Đã load {Projects.Count} project từ database.");
                return Projects;
            }
        }

        public void SaveProjectsToFile()
        {
            using (var context = new TaskManagementDBContext())
            {
                foreach (var project in Projects)
                {
                    var existingProject = (from p in context.Projects
                                           where p.projectID == project.projectID
                                           select p).FirstOrDefault();

                    if (existingProject != null)
                    {
                        existingProject.projectName = project.projectName;
                        existingProject.projectDescription = project.projectDescription;
                        existingProject.AdminID = project.AdminID;
                    }
                    else
                    {
                        context.Projects.Add(project);
                    }
                }
                context.SaveChanges();
                Console.WriteLine("Danh sách project đã được lưu vào database.");
            }
        }

        public async System.Threading.Tasks.Task CreateProjectAsync(Project newProject)
        {
            using (var dbContext = new TaskManagementDBContext())
            {
                dbContext.Projects.Add(newProject);
                dbContext.SaveChanges();

                ActivityLogService activityLogService = new ActivityLogService(dbContext);
                await activityLogService.LogActivityAsync(userId: User.LoggedInUser.ID, objectType: "Project", objectId: newProject.projectID, action: "Create Project", details: $"{User.LoggedInUser.Username} đã tạo dự án {newProject.projectName} lúc {DateTime.Now}");
                MessageBox.Show("Activitlog tạo project");
            }
        }

        public async System.Threading.Tasks.Task<bool> AddMemberToProjectAsync(int projectID, string newMemberUsername, User currentUser)
        {
            using (var dbContext = new TaskManagementDBContext())
            {
                var selectedProject = (from p in dbContext.Projects
                                       where p.projectID == projectID
                                       select p).FirstOrDefault();

                if (selectedProject == null)
                {
                    MessageBox.Show("Không tìm thấy project!");
                    return false;
                }

                var user = (from u in dbContext.Users
                            where u.Username.ToLower() == newMemberUsername.ToLower()
                            select u).FirstOrDefault();

                if (user == null)
                {
                    MessageBox.Show("Người dùng không tồn tại! Vui lòng nhập tên thành viên hợp lệ.");
                    return false;
                }

                var assignedTasks = from t in dbContext.Tasks
                                    where t.ProjectID == selectedProject.projectID && t.AssignedTo == user.ID
                                    select t;
                if (assignedTasks.Any())
                {
                    MessageBox.Show($"Thành viên {newMemberUsername} đã được gán vào dự án này!");
                    return false;
                }

                Task newTask = new Task
                {
                    taskName = "AddUserToNewProject###",
                    status = "Unfinished",
                    deadline = DateTime.Now.AddDays(7),
                    AssignedTo = user.ID,
                    ProjectID = selectedProject.projectID
                };
                dbContext.Tasks.Add(newTask);
                dbContext.SaveChanges();

                ActivityLogService activityLogService = new ActivityLogService(dbContext);
                await activityLogService.LogActivityAsync(userId: currentUser.ID, objectType: "Project", objectId: selectedProject.projectID, action: "Add Member", details: $"{currentUser.Username} đã mời {user.Username} vào dự án {selectedProject.projectName} ");
                return true;
            }
        }

        public void DeleteProject(int projectID, int userId)
        {
            if (Projects == null || Projects.Count == 0)
            {
                throw new Exception("Danh sách project rỗng. Không thể xóa!");
            }

            Console.WriteLine($"Đang tìm project với ID: {projectID}");
            using (var context = new TaskManagementDBContext())
            {
                var projectToRemove = (from p in context.Projects
                                       where p.projectID == projectID
                                       select p).FirstOrDefault();
                if (projectToRemove != null)
                {
                    context.Projects.Remove(projectToRemove);
                    context.SaveChanges();

                    LoadProjects();
                    Console.WriteLine($"Project {projectID} đã bị xóa.");
                    ActivityLogService activityLogService = new ActivityLogService(context);
                    activityLogService.LogActivityAsync(userId: userId, objectType: "Project", objectId: projectToRemove.projectID, action: "Delete Project", details: $"Project Name : {projectToRemove.projectName} ").Wait();
                    MessageBox.Show("Activitlog xóa project");
                }
                else
                {
                    Console.WriteLine($"Lỗi: Không tìm thấy project {projectID}.");
                    throw new Exception($"Project với ID {projectID} không tồn tại.");
                }
            }
        }

        public Project FindProjectByName(string ProjectName)
        {
            using (var context = new TaskManagementDBContext())
            {
                var project = (from p in context.Projects
                               where p.projectName == ProjectName
                               select new Project
                               {
                                   projectID = p.projectID,
                                   projectName = p.projectName,
                                   projectDescription = p.projectDescription,
                                   AdminID = p.AdminID
                               }).FirstOrDefault();
                return project;
            }
        }

        public Project FindProjectById(int projectID)
        {
            using (var context = new TaskManagementDBContext())
            {
                var project = (from p in context.Projects
                               where p.projectID == projectID
                               select new Project
                               {
                                   projectID = p.projectID,
                                   projectName = p.projectName,
                                   projectDescription = p.projectDescription,
                                   AdminID = p.AdminID
                               }).FirstOrDefault();
                return project;
            }
        }

        public List<Project> FindProjectsByMember(User user)
        {
            using (var dbContext = new TaskManagementDBContext())
            {
                int userId = user.ID;
                var taskProjectIds = dbContext.Tasks
                    .Where(t => t.AssignedTo == userId)
                    .Select(t => t.ProjectID);
                var meetingProjectIds = dbContext.Meetings
                    .Where(m => m.AssignedTo == userId)
                    .Select(m => m.ProjectID);
                var milestoneProjectIds = dbContext.Milestones
                    .Where(ms => ms.AssignedTo == userId)
                    .Select(ms => ms.ProjectID);
                var memberProjectIds = taskProjectIds
                    .Union(meetingProjectIds)
                    .Union(milestoneProjectIds)
                    .ToList();
                var projects = dbContext.Projects
                    .Where(p => p.AdminID == userId || memberProjectIds.Contains(p.projectID))
                    .ToList();
                return projects;
            }
        }

        public List<Project> GetAllUserProjects(int userId)
        {
            using (var context = new TaskManagementDBContext())
            {
                var adminProjects = from p in context.Projects
                                    where p.AdminID == userId
                                    select p;

                var taskProjects = from t in context.Tasks
                                   where t.AssignedTo == userId
                                   join p in context.Projects on t.ProjectID equals p.projectID
                                   select p;

                var meetingProjects = from m in context.Meetings
                                      where m.AssignedTo == userId
                                      join p in context.Projects on m.ProjectID equals p.projectID
                                      select p;

                var milestoneProjects = from m in context.Milestones
                                        where m.AssignedTo == userId
                                        join p in context.Projects on m.ProjectID equals p.projectID
                                        select p;

                var allProjects = adminProjects
                                  .Union(taskProjects)
                                  .Union(meetingProjects)
                                  .Union(milestoneProjects)
                                  .Distinct()
                                  .ToList();
                return allProjects;
            }
        }

        public void UpdateProjectDescription(int projectId, string newDescription)
        {
            using (var context = new TaskManagementDBContext())
            {
                var projectToUpdate = context.Projects.FirstOrDefault(p => p.projectID == projectId);
                if (projectToUpdate != null)
                {
                    projectToUpdate.projectDescription = newDescription;
                    context.SaveChanges();
                }
            }
        }

        public List<User> GetProjectMembers(int projectID, int adminID)
        {
            using (var dbContext = new TaskManagementDBContext())
            {
                var taskMemberIds = dbContext.Tasks
                    .Where(t => t.ProjectID == projectID)
                    .Select(t => t.AssignedTo);
                var meetingMemberIds = dbContext.Meetings
                    .Where(m => m.ProjectID == projectID)
                    .Select(m => m.AssignedTo);
                var milestoneMemberIds = dbContext.Milestones
                    .Where(ms => ms.ProjectID == projectID)
                    .Select(ms => ms.AssignedTo);
                var allMemberIds = taskMemberIds
                    .Union(meetingMemberIds)
                    .Union(milestoneMemberIds)
                    .Distinct()
                    .ToList();

                List<User> members = new List<User>();

                var admin = dbContext.Users.FirstOrDefault(u => u.ID == adminID);
                if (admin != null)
                {
                    members.Add(admin);
                }

                foreach (var userId in allMemberIds)
                {
                    if (userId == adminID)
                        continue;
                    var user = dbContext.Users.FirstOrDefault(u => u.ID == userId);
                    if (user != null)
                    {
                        members.Add(user);
                    }
                }
                return members;
            }
        }

        public List<AbaseTask> GetTasksByProjectName(string projectName)
        {
            return _taskManager.GetTasksByProject(projectName);
        }
    }
}