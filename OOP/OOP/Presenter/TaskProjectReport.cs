using OOP.Models;
using OOP.Services; // If TaskManagementDBContext is initialized here, otherwise in the form directly.
using System.Collections.Generic;

namespace OOP.Forms
{
    public class TaskReportPresenter
    {
        private readonly ITaskReportView _view;
        private readonly TaskReportService _model;
        private readonly int _taskId;

        public TaskReportPresenter(ITaskReportView view, int taskId)
        {
            _view = view;
            _taskId = taskId;
            _model = new TaskReportService(new TaskManagementDBContext()); // Initialize your DbContext here or inject it
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