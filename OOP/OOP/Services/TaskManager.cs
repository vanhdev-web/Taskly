using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OOP.Models;
using OOP.Services;

namespace OOP.Services
{
    public class TaskManager
    {
        private static TaskManager _instance;
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.json");

        public List<AbaseTask> Tasks { get; private set; }

        public static TaskManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TaskManager();
            }
            return _instance;
        }

        private TaskManager()
        {
            Tasks = new List<AbaseTask>();
            LoadTasksFromFile();
        }

        // ✅ Thêm task mới
        public async System.Threading.Tasks.Task AddTask(AbaseTask task)
        {
            if (task == null) return;
            if (task is OOP.Models.Task newtask)
            {
                using (var db = new TaskManagementDBContext())
                {
                    bool exists = db.Tasks.Any(t => t.taskID == newtask.taskID);
                    if (exists)
                    {
                        System.Diagnostics.Debug.WriteLine($"Task with ID {newtask.taskID} already exists! Skipping add.");
                        return;
                    }

                    db.Tasks.Add(newtask);
                    await db.SaveChangesAsync();

                    var assignedUser = db.Users.FirstOrDefault(u => u.ID == newtask.AssignedTo);
                    var project = db.Projects.FirstOrDefault(p => p.projectID == newtask.ProjectID);
                    ActivityLogService activityLogService = new ActivityLogService(db);
                    await activityLogService.LogActivityAsync(
                        userId: User.LoggedInUser.ID,
                        objectType: "Task",
                        objectId: newtask.taskID,
                        action: "Create Task",
                        details: $"{User.LoggedInUser.Username} đã tạo task {newtask.taskName} trong dự án {project?.projectName} cho {assignedUser?.Username}");
                }
            }

            if (!Tasks.Any(t => t.taskID == task.taskID))
            {
                Tasks.Add(task);
            }
        }
        public void RemoveTask(int taskID)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].taskID == taskID)
                {
                    Tasks.RemoveAt(i);
                    //SaveTasksToFile();
                    return;
                }
            }
        }

        // ✅ Tìm task theo User
        public List<AbaseTask> GetTasksByUser(User user)
        {
            List<AbaseTask> userTasks = new List<AbaseTask>();
            foreach (AbaseTask task in Tasks)
            {
                if (task.AssignedTo == user.ID)
                {
                    userTasks.Add(task);
                }
            }
            return userTasks;
        }

        // Logic originally in Tasks.cs moved to TaskManager.cs
        public List<AbaseTask> GetUserTasks(User currentUser, ProjectManager projectManager)
        {
            List<Project> userProjects = projectManager.FindProjectsByMember(currentUser);
            List<AbaseTask> userTasks = new List<AbaseTask>();

            if (userProjects.Count == 0)
            {
                Console.WriteLine("User không thuộc bất kỳ project nào.");
                return userTasks; // Trả về danh sách rỗng nếu user không có project
            }

            using (var db = new TaskManagementDBContext())
            {
                int userId = currentUser.ID;
                // lấy các task mà được assign cho user
                var tasks = db.Tasks
                    .Where(t => t.AssignedTo == userId && !t.taskName.Contains("AddUserToNewProject###"))
                    .ToList();
                userTasks.AddRange(tasks);

                // lấy các meeting mà được assign cho user
                var meetings = db.Meetings
                                 .Where(m => m.AssignedTo == userId)
                                 .ToList();
                userTasks.AddRange(meetings);

                // lấy các milestone mà được assign cho user
                var milestones = db.Milestones
                                   .Where(ms => ms.AssignedTo == userId)
                                   .ToList();

                userTasks.AddRange(milestones);
            }
            return userTasks;
        }

        public List<AbaseTask> GetFinishedTasks(User currentUser)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                var task = (from t in dbcontext.Tasks
                            where t.AssignedTo == currentUser.ID && t.status == "Finished"
                            select t).ToList<AbaseTask>();
                var meetings = (from m in dbcontext.Meetings
                                where m.AssignedTo == currentUser.ID && m.status == "Finished"
                                select m).ToList<AbaseTask>();
                var milestones = (from ms in dbcontext.Milestones
                                  where ms.AssignedTo == currentUser.ID && ms.status == "Finished"
                                  select ms).ToList<AbaseTask>();
                var taskslistother = task.Union(meetings).Union(milestones).ToList();
                return taskslistother;
            }
        }

        public List<AbaseTask> GetUnfinishedTasks(User currentUser)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                var task = (from t in dbcontext.Tasks
                            where t.AssignedTo == currentUser.ID && t.status != "Finished"
                            select t).ToList<AbaseTask>();
                var meetings = (from m in dbcontext.Meetings
                                where m.AssignedTo == currentUser.ID && m.status != "Finished"
                                select m).ToList<AbaseTask>();
                var milestones = (from ms in dbcontext.Milestones
                                  where ms.AssignedTo == currentUser.ID && ms.status != "Finished"
                                  select ms).ToList<AbaseTask>();
                var taskslistother = task.Union(meetings).Union(milestones).ToList();
                return taskslistother;
            }
        }


        // ✅ Tìm task theo Project
        public List<AbaseTask> GetTasksByProject(string projectName)
        {
            using (var db = new TaskManagementDBContext())
            {
                // tìm project theo tên
                var project = db.Projects.FirstOrDefault(p => p.projectName == projectName);
                if (project == null) return new List<AbaseTask>();

                int projectId = project.projectID;
                // lấy các task, meeting, milestone có cùng projectID
                var taskList = db.Tasks
                                 .Where(t => t.ProjectID == projectId && t.taskName != "AddUserToNewProject###")
                                 .Select(t => new OOP.Models.Task
                                 {
                                     taskID = t.taskID,
                                     taskName = t.taskName,
                                     status = t.status,
                                     deadline = t.deadline,
                                     AssignedTo = t.AssignedTo,
                                     ProjectID = t.ProjectID
                                 }).ToList<AbaseTask>();

                var meetingList = db.Meetings
                                    .Where(m => m.ProjectID == projectId)
                                    .Select(m => new Meeting
                                    {
                                        taskID = m.taskID,
                                        taskName = m.taskName,
                                        status = m.status,
                                        deadline = m.deadline,
                                        AssignedTo = m.AssignedTo,
                                        ProjectID = m.ProjectID,
                                        Location = m.Location,
                                        Hour = m.Hour
                                    }).ToList<AbaseTask>();
                var milestoneList = db.Milestones
                                      .Where(ms => ms.ProjectID == projectId)
                                      .Select(ms => new Milestone
                                      {
                                          taskID = ms.taskID,
                                          taskName = ms.taskName,
                                          status = ms.status,
                                          deadline = ms.deadline,
                                          AssignedTo = ms.AssignedTo,
                                          ProjectID = ms.ProjectID,
                                          Description = ms.Description
                                      }).ToList<AbaseTask>();
                // gộp tất cả lại
                var allTasks = new List<AbaseTask>();
                allTasks.AddRange(taskList);
                allTasks.AddRange(meetingList);
                allTasks.AddRange(milestoneList);

                return allTasks;
            }
        }
        public void LoadTasksFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    List<AbaseTask> loadedTasks = JsonConvert.DeserializeObject<List<AbaseTask>>(json, settings);
                    Tasks = loadedTasks ?? new List<AbaseTask>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                Tasks = new List<AbaseTask>();
            }
        }

        public void UpdateTask(AbaseTask updatedTask)
        {
            if (updatedTask == null) return;
            for (int i = 0; i < Tasks.Count; i++)
            {
                if (Tasks[i].taskID == updatedTask.taskID)
                {
                    Tasks[i] = updatedTask;
                    // Cập nhật task
                    //SaveTasksToFile();
                    // Lưu lại file
                    return;
                }
            }
        }
    }
}