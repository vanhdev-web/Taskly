﻿using System.Collections.Generic;
using System.Linq;
using Taskly.Models;
using Taskly.Services; // Assuming TaskManagementDBContext is initialized here or injected

namespace Taskly.Forms
{
    public class ProjectReportPresenter
    {
        private readonly IProjectReportView _view;
        private readonly ProjectReportService _model;
        private readonly int _loggedInUserId;

        public ProjectReportPresenter(IProjectReportView view, int loggedInUserId)
        {
            _view = view;
            _loggedInUserId = loggedInUserId;
            _model = new ProjectReportService(new TaskManagementDBContext()); // Initialize your DbContext
        }

        public void LoadProjects()
        {
            var projects = _model.GetProjectsForUser(_loggedInUserId);
            _view.SetProjectList(projects);
        }

        public void OnProjectSelected(Project selectedProject)
        {
            _view.ClearActivityLogView();
            _view.SetupActivityLogColumns();

            if (selectedProject == null)
            {
                _view.DisplayProjectNotFoundMessage();
                return;
            }

            var activities = _model.GetProjectAndRelatedActivities(selectedProject.projectID);

            if (!activities.Any())
            {
                _view.DisplayNoActivitiesFoundMessage();
            }
            else
            {
                _view.SetActivityLogs(activities);
            }
        }
    }
}