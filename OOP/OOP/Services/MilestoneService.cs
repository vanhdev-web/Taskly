using OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ModelUser = OOP.Models.User; // Alias for clarity

namespace OOP.Services
{
    public class MilestoneService
    {
        public Milestone CreateMilestone(string taskName, DateTime deadline, int projectId)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                var mileStone = new Milestone
                {
                    taskName = taskName,
                    status = "UnFinished", // Trạng thái ban đầu là "Chưa hoàn thành" [cite: 23]
                    Description = "Inite milestone",
                    deadline = deadline,
                    ProjectID = projectId,
                    AssignedTo = ModelUser.LoggedInUser.ID // Gán người dùng đăng nhập là người được giao nhiệm vụ [cite: 24]
                };
                dbcontext.Milestones.Add(mileStone);
                dbcontext.SaveChanges();
                return mileStone;
            }
        }

        public void AddMilestoneMembers(int milestoneId, List<ModelUser> members)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                foreach (var member in members)
                {
                    if (member != null)
                    {
                        var mileStoneManagement = new MilestoneMemberManagement
                        {
                            MilestoneID = milestoneId,
                            UserID = member.ID,
                        };
                        dbcontext.MilestoneMemberManagements.Add(mileStoneManagement);
                        dbcontext.SaveChanges();
                    }
                }
            }
        }

        public int GetProjectIdByName(string projectName)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                var projectIdQuery = (from p in dbcontext.Projects
                                      where p.projectName == projectName
                                      select p.projectID);
                return projectIdQuery.FirstOrDefault();
            }
        }

        public List<ModelUser> GetProjectMembers(int projectId)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                // lấy userIds từ tasks [cite: 11, 12]
                var userIdsFromTasks = from t in dbcontext.Tasks
                                       where t.ProjectID == projectId
                                       select t.AssignedTo;
                // lấy userIds từ meetings [cite: 13, 14]
                var userIdsFromMeetings = from m in dbcontext.Meetings
                                          where m.ProjectID == projectId
                                          select m.AssignedTo;
                // lấy userIds từ milestones [cite: 15, 16]
                var userIdsFromMilestones = from ms in dbcontext.Milestones
                                            where ms.ProjectID == projectId
                                            select ms.AssignedTo;
                // kết hợp và distinct [cite: 17, 18, 19]
                var allUserIds = (from id in userIdsFromTasks
                                  select id)
                                 .Union(from id in userIdsFromMeetings
                                        select id)
                                 .Union(from id in userIdsFromMilestones
                                        select id)
                                 .Distinct();
                // lấy danh sách user thực sự [cite: 20]
                var members = (from u in dbcontext.Users
                               where allUserIds.Contains(u.ID)
                               select u).ToList();
                return members;
            }
        }
    }
}