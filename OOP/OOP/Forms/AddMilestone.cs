using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using OOP.Models;
using OOP.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace OOP.Forms
{
    public partial class AddMilestone : Form
    {
        public Milestone milestone { get; set; }
        public List<Milestone> milestones = new List<Milestone>();
        public AddMilestone()
        {
            InitializeComponent();
            UpdateComboBox();

        }

        public AddMilestone(List<Milestone> list)
        {
            this.milestones = list;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddMilestone_Load(object sender, EventArgs e)
        {

        }

        private async void btnMilestoneConfirm_Click(object sender, EventArgs e)
        {



            string taskName = txtbMilestoneName.Text;
            DateTime deadline = dtpMilestonedate.Value;
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



                var mileStone = new Milestone
                {
                    taskName = taskName,
                    status = "UnFinished", // Trạng thái ban đầu là "Chưa hoàn thành"
                    Description = "Inite milestone",
                    deadline = deadline,
                    ProjectID = projectId,
                    AssignedTo = User.LoggedInUser.ID // Gán người dùng đăng nhập là người được giao nhiệm vụ

                };

                milestone = mileStone;
                dbcontext.Milestones.Add(mileStone);
                dbcontext.SaveChanges();


                foreach (var member in members)
                {

                    if (member != null)
                    {
                        var mileStoneManagement = new MilestoneMemberManagement
                        {
                            MilestoneID = mileStone.taskID,
                            UserID = member.ID,
                        };

                        dbcontext.MilestoneMemberManagements.Add(mileStoneManagement);
                        dbcontext.SaveChanges();

                    }
                }

                var project =  (from p in dbcontext.Projects
                               where p.projectID == projectId
                               select p).FirstOrDefault();
                ActivityLogService activityLogService = new ActivityLogService(dbcontext);
                await activityLogService.LogActivityAsync(userId: User.LoggedInUser.ID, objectType: "MileStone", objectId: mileStone.taskID, action: "Create Milestone", details: $"{User.LoggedInUser.Username} đã tạo một cột mộc {mileStone.taskName} cho dự án {project.projectName}");
                MessageBox.Show("Activitlog thêm milestone");


                //milestone = new Milestone(tasknewID, taskName, "UnFinished", deadline, null, projectName, 0);
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
        private void btnMilestoneCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtbMilestoneName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbMilestoneName.Text))
            {
                e.Cancel = true; // Chặn chuyển focus nếu input trống
                errMilestoneName.SetError(txtbMilestoneName, "Please enter task name!");
            }
            else
            {
                e.Cancel = false; // Cho phép focus rời khỏi control
                errMilestoneName.SetError(txtbMilestoneName, null);
            }
        }
    }
}