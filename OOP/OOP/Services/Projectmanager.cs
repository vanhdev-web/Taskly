using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP.Models;
using OOP;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
namespace OOP.Services
{
    public class ProjectManager
    {
        public List<Project> Projects { get; set; }


        public ProjectManager()
        {
            Projects = new List<Project>();
            LoadProjectsFromFile();

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
        public void LoadProjectsFromFile()
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
            }

        }
        public void AddProject(Project newProject)
        {
            using (var context = new TaskManagementDBContext())
            {
                var existing = (from p in context.Projects
                                where p.projectName == newProject.projectName
                                select p).FirstOrDefault();

                if (existing != null)
                {
                    MessageBox.Show("Project với tên này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                context.Projects.Add(newProject);
                context.SaveChanges();

                ///xem lại xem có cần thiết không

                Projects.Add(newProject); // cập nhật danh sách local nếu cần dùng
                MessageBox.Show($"Project '{newProject.projectName}' đã được thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public async void DeleteProject(int projectID)
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

                    // Cập nhật lại danh sách Projects trong bộ nhớ
                    LoadProjectsFromFile();

                    Console.WriteLine($"Project {projectID} đã bị xóa.");
                    ActivityLogService activityLogService = new ActivityLogService(context);
                    await activityLogService.LogActivityAsync(userId: User.LoggedInUser.ID, objectType: "Project", objectId: projectToRemove.projectID, action: "Delete Project", details: $"Project Name : {projectToRemove.projectName} ");
                    MessageBox.Show("Activitlog xóa project");
                }
                else
                {
                    Console.WriteLine($"Lỗi: Không tìm thấy project {projectID}.");
                    throw new Exception($"Project với ID {projectID} không tồn tại.");
                }
            }
        }
        public Project FindProject(string ProjectName)
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
        public List<Project> FindProjectsByMember(User user)
        {
            using (var dbContext = new TaskManagementDBContext())
            {
                // Lấy ID user
                int userId = user.ID;

                // Lấy tất cả các ProjectID mà user này có AssignedTo trong bất kỳ bảng nào
                var taskProjectIds = dbContext.Tasks
                    .Where(t => t.AssignedTo == userId)
                    .Select(t => t.ProjectID);

                var meetingProjectIds = dbContext.Meetings
                    .Where(m => m.AssignedTo == userId)
                    .Select(m => m.ProjectID);

                var milestoneProjectIds = dbContext.Milestones
                    .Where(ms => ms.AssignedTo == userId)
                    .Select(ms => ms.ProjectID);

                // Gộp tất cả ProjectID liên quan và loại trùng
                var memberProjectIds = taskProjectIds
                    .Union(meetingProjectIds)
                    .Union(milestoneProjectIds)
                    .ToList();

                // Truy vấn tất cả các project mà user là admin hoặc được giao nhiệm vụ
                var projects = dbContext.Projects
                    .Where(p => p.AdminID == userId || memberProjectIds.Contains(p.projectID))
                    .ToList();

                return projects;
            }

        }

    }
}