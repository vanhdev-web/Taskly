using Taskly.Models;
using Taskly.Services; // If TaskManagementDBContext is initialized here, otherwise in the form directly.
using System.Collections.Generic;

namespace Taskly.Forms
{
    public class TaskReportPresenter
    {
        private readonly ITaskReportView _view;
        private readonly TaskReportModel _model;
        private readonly int _taskId;

        public TaskReportPresenter(ITaskReportView view, int taskId)
        {
            _view = view;
            _taskId = taskId;
            _model = new TaskReportModel(new TaskManagementDBContext()); // Initialize your DbContext here or inject it
        }

        public void LoadActivities()
        {
            _view.ClearActivityLogView();
            _view.SetupActivityLogColumns();

            var logs = _model.GetActivityLogsForTask(_taskId);

            if (logs.Count == 0)
            {
                _view.DisplayNoActivitiesFoundMessage();
            }
            else
            {
                _view.SetActivityLogs(logs);
            }
        }
    }
}