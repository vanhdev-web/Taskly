using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using OOP.Models;
using OOP.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using User = OOP.Models.User;

namespace OOP.Forms
{
    public partial class AddMeeting : Form
    {
        public Meeting newMeeting { get; set; }
        public List<User> users { get; set; }

        public List<Meeting> meetings { get; set; }
        public AddMeeting(List<User> users)
        {
            InitializeComponent();
            this.users = users;
            UpdateComboBox();
        }



        private void AddMeeting_Load(object sender, EventArgs e)
        {


        }


        private async void btnMeetingConfirm_Click(object sender, EventArgs e)
        {

            //string location = txtbMeetingLocation.Text;
            //DateTime meetingTime = dtpMeetingTime.Value;
            //string hour = lblHour.Text;

            //string taskName = txtbMeetingName.Text;
            //string status = "Incompleted";
            //string projectName = cbbSelectProject.Text;
            //using (var dbcontext = new TaskManagementDBContext())
            //{
            //    var projectId= (from p in dbcontext.Projects
            //                   where p.projectName == projectName
            //                   select p.projectID).FirstOrDefault();
            //    var meeting = new Meeting
            //    {
            //        taskName = taskName,
            //        status = status,
            //        deadline = meetingTime,
            //        Location = location,
            //        Hour = hour,
            //        ProjectID = projectId,


            //    };
            //    newMeeting = meeting;

            //    dbcontext.Meetings.Add(meeting);
            //    dbcontext.SaveChanges();
            //    var userIdsFromTasks = from t in dbcontext.Tasks
            //                           where t.ProjectID == projectId
            //                           select t.AssignedTo;

            //    // lấy userIds từ meetings
            //    var userIdsFromMeetings = from m in dbcontext.Meetings
            //                              where m.ProjectID == projectId
            //                              select m.AssignedTo;

            //    // lấy userIds từ milestones
            //    var userIdsFromMilestones = from ms in dbcontext.Milestones
            //                                where ms.ProjectID == projectId
            //                                select ms.AssignedTo;

            //    // kết hợp và distinct
            //    var allUserIds = (from id in userIdsFromTasks
            //                      select id)
            //                     .Union(from id in userIdsFromMeetings
            //                            select id)
            //                     .Union(from id in userIdsFromMilestones
            //                            select id)
            //                     .Distinct();

            //    // lấy danh sách user thực sự
            //    var members = (from u in dbcontext.Users
            //                   where allUserIds.Contains(u.ID)
            //                   select u).ToList();

            //}




            //MessageBox.Show("Meeting xong");
            ////newMeeting = new Meeting(tasknewID, taskName, status, meetingTime, hour, location, members, projectName, 0);
            //DialogResult = DialogResult.OK;
            //Close();

            string location = txtbMeetingLocation.Text;
            DateTime meetingTime = dtpMeetingTime.Value;
            string hour = lblHour.Text;

            string taskName = txtbMeetingName.Text;
            string status = "Unfinished";
            string projectName = cbbSelectProject.Text;
            using (var dbcontext = new TaskManagementDBContext())
            {
                // lấy projectId theo projectName
                var projectIdQuery = (from p in dbcontext.Projects
                                      where p.projectName == projectName
                                      select p.projectID);

                int projectId = projectIdQuery.FirstOrDefault();

                if (projectId == 0)
                {
                    MessageBox.Show("Dự án không tồn tại.");
                    return;
                }

                // lấy userIds từ tasks
                var userIdsFromTasks = from t in dbcontext.Tasks
                                       where t.ProjectID == projectId
                                       select t.AssignedTo;

                // lấy userIds từ meetings
                var userIdsFromMeetings = from m in dbcontext.Meetings
                                          where m.ProjectID == projectId
                                          select m.AssignedTo;

                // lấy userIds từ milestones
                var userIdsFromMilestones = from ms in dbcontext.Milestones
                                            where ms.ProjectID == projectId
                                            select ms.AssignedTo;

                // kết hợp và distinct
                var allUserIds = (from id in userIdsFromTasks
                                  select id)
                                 .Union(from id in userIdsFromMeetings
                                        select id)
                                 .Union(from id in userIdsFromMilestones
                                        select id)
                                 .Distinct();

                // lấy danh sách user thực sự
                var members = (from u in dbcontext.Users
                               where allUserIds.Contains(u.ID)
                               select u).ToList();

                if (members.Count == 0)
                {
                    MessageBox.Show("Không có thành viên nào thuộc dự án này.");
                    return;
                }

                // lấy danh sách member được chọn trong checkedListBoxMember (giả sử bạn có CheckedListBox)



                var meeting = new Meeting
                {
                    taskName = taskName,
                    status = status,
                    deadline = meetingTime,
                    Location = location,
                    Hour = hour,
                    ProjectID = projectId,
                    AssignedTo= User.LoggedInUser.ID // Gán người dùng đăng nhập là người được giao nhiệm vụ

                };

                newMeeting = meeting;
                dbcontext.Meetings.Add(meeting);
                dbcontext.SaveChanges();


                foreach (var member in members)
                {

                    if (member != null)
                    {
                        var meetingManagement = new MeetingMemberManagement
                        {
                            MeetingID = meeting.taskID,
                            UserID = member.ID, 
                        };
                      
                        dbcontext.MeetingMemberManagements.Add(meetingManagement);
                        dbcontext.SaveChanges();
                  
                    }
                }

                var project = (from p in dbcontext.Projects
                               where p.projectID == projectId
                               select p).FirstOrDefault();
                ActivityLogService activityLogService = new ActivityLogService(dbcontext);
                await activityLogService.LogActivityAsync(userId: User.LoggedInUser.ID, objectType: "Meeting", objectId: meeting.taskID, action: "Create Meeting", details: $"{User.LoggedInUser.Username} đã tạo một cuộc họp {meeting.taskName} cho dự án {project.projectName} ");
                MessageBox.Show("Activitlog thêm meeting");


                //newMeeting = new Meeting(tasknewID, taskName, status, meetingTime, hour, location, members, projectName, 0);
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        private ProjectManager projectManager = new ProjectManager();
        private void UpdateComboBox()
        {
            cbbSelectProject.Items.Clear();

            if (User.LoggedInUser == null) return; // Kiểm tra user đăng nhập

            foreach (Project project in projectManager.Projects)
            {
                if (project == null || project.members == null) continue; // Kiểm tra null tránh lỗi

                Console.WriteLine($"Project: {project.projectID} - {project.projectName}, AdminID: {project.AdminID}, Members: {string.Join(", ", project.members)}");

                bool isMember = false;
                //foreach (string member in project.members)
                //{
                //    string memberUsername = member.Split('(')[0].Trim(); // Lấy username trước dấu "(" và Trim()
                //    if (memberUsername == User.LoggedInUser.Username)
                //    {
                //        isMember = true;
                //        break;
                //    }
                //}

                if (project.AdminID == User.LoggedInUser.ID || isMember)
                {
                    cbbSelectProject.Items.Add($"{project.projectName}");
                }
            }
        }

        private void txtbMeetingName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbMeetingName.Text))
            {
                e.Cancel = true; // Chặn chuyển focus nếu input trống
                errMeetingName.SetError(txtbMeetingName, "Please enter task name!");
            }
            else
            {
                e.Cancel = false; // Cho phép focus rời khỏi control
                errMeetingName.SetError(txtbMeetingName, null);
            }
        }
    }
}