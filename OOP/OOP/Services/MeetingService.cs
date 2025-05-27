using OOP.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using ModelUser = OOP.Models.User;

namespace OOP.Services
{
    public class MeetingService
    {
        public Meeting CreateMeeting(string taskName, string status, DateTime deadline, string location, string hour, int projectId)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                var meeting = new Meeting
                {
                    taskName = taskName,
                    status = status,
                    deadline = deadline,
                    Location = location,
                    Hour = hour,
                    ProjectID = projectId,
                    AssignedTo = ModelUser.LoggedInUser.ID // Gán người dùng đăng nhập là người được giao nhiệm vụ [cite: 41]
                };
                dbcontext.Meetings.Add(meeting);
                dbcontext.SaveChanges();
                return meeting;
            }
        }

        public void AddMeetingMembers(int meetingId, List<ModelUser> members)
        {
            using (var dbcontext = new TaskManagementDBContext())
            {
                foreach (var member in members)
                {
                    if (member != null)
                    {
                        var meetingManagement = new MeetingMemberManagement
                        {
                            MeetingID = meetingId,
                            UserID = member.ID,
                        };
                        dbcontext.MeetingMemberManagements.Add(meetingManagement);
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
                // lấy userIds từ tasks [cite: 28, 29]
                var userIdsFromTasks = from t in dbcontext.Tasks
                                       where t.ProjectID == projectId
                                       select t.AssignedTo;
                // lấy userIds từ meetings [cite: 30, 31]
                var userIdsFromMeetings = from m in dbcontext.Meetings
                                          where m.ProjectID == projectId
                                          select m.AssignedTo;
                // lấy userIds từ milestones [cite: 32, 33]
                var userIdsFromMilestones = from ms in dbcontext.Milestones
                                            where ms.ProjectID == projectId
                                            select ms.AssignedTo;
                // kết hợp và distinct [cite: 34, 35, 36]
                var allUserIds = (from id in userIdsFromTasks
                                  select id)
                                 .Union(from id in userIdsFromMeetings
                                        select id)
                                 .Union(from id in userIdsFromMilestones
                                        select id)
                                 .Distinct();
                // lấy danh sách user thực sự [cite: 37]
                var members = (from u in dbcontext.Users
                               where allUserIds.Contains(u.ID)
                               select u).ToList();
                return members;
            }
        }
    }
}