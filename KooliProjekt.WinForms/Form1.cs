using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KooliProjekt.Models;
using KooliProjekt.WinForms;
using System.Drawing;
using System.Linq;

namespace KooliProjekt.WinForms
{
    public partial class Form1 : Form, IProjectView, IProjectTaskView, ITeamMemberView
    {
        private ProjectPresenter _projectPresenter;
        private ProjectTaskPresenter _taskPresenter;
        private TeamMemberPresenter _memberPresenter;

        public Form1()
        {
            InitializeComponent();
            _projectPresenter = new ProjectPresenter(this);
            _taskPresenter = new ProjectTaskPresenter(this);
            _memberPresenter = new TeamMemberPresenter(this);
            
            InitializeGrids();
            LoadAll();
        }

        private void LoadAll()
        {
            _projectPresenter.LoadProjects();
            _taskPresenter.LoadTasks();
            _memberPresenter.LoadMembers();
        }

        private void InitializeGrids()
        {
            // Project Grid
            ProjectList.AutoGenerateColumns = false;
            ProjectList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Name", Width = 200 });
            ProjectList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Start", Width = 100 });
            ProjectList.Columns.Add(new DataGridViewButtonColumn { Name = "Actions", HeaderText = "Actions", Text = "Edit/Delete", UseColumnTextForButtonValue = true });

            // Task Grid
            TaskList.AutoGenerateColumns = false;
            TaskList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Title", Width = 200 });
            TaskList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 100 });
            TaskList.Columns.Add(new DataGridViewButtonColumn { Name = "Actions", HeaderText = "Actions", Text = "Edit/Delete", UseColumnTextForButtonValue = true });

            // Member Grid
            MemberList.AutoGenerateColumns = false;
            MemberList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Name", Width = 200 });
            MemberList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 200 });
            MemberList.Columns.Add(new DataGridViewButtonColumn { Name = "Actions", HeaderText = "Actions", Text = "Edit/Delete", UseColumnTextForButtonValue = true });
        }

        public void DisplayProjects(List<Project> projects) => ProjectList.DataSource = projects;
        public void DisplayTasks(List<ProjectTask> tasks) => TaskList.DataSource = tasks;
        public void DisplayMembers(List<TeamMember> members) => MemberList.DataSource = members;
        public void ShowError(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        #region Events

        private async void LoadProjects_Click(object sender, EventArgs e) => await _projectPresenter.LoadProjects();
        private async void LoadTasks_Click(object sender, EventArgs e) => await _taskPresenter.LoadTasks();
        private async void LoadMembers_Click(object sender, EventArgs e) => await _memberPresenter.LoadMembers();

        private async void AddButton_Click(object sender, EventArgs e)
        {
            var win = new KooliProjekt.WPF.ProjectWindow();
            if (win.ShowDialog() == true) await _projectPresenter.AddProject(win.Project);
        }

        private async void AddTaskButton_Click(object sender, EventArgs e)
        {
            var win = new KooliProjekt.WPF.ProjectTaskWindow();
            if (win.ShowDialog() == true) await _taskPresenter.AddTask(win.Task);
        }

        private async void AddMemberButton_Click(object sender, EventArgs e)
        {
            var win = new KooliProjekt.WPF.TeamMemberWindow();
            if (win.ShowDialog() == true) await _memberPresenter.AddMember(win.TeamMember);
        }

        private async void ProjectList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != ProjectList.Columns["Actions"].Index) return;
            var p = ProjectList.Rows[e.RowIndex].DataBoundItem as Project;
            var res = MessageBox.Show("Edit (Yes) or Delete (No)?", "Action", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                var win = new KooliProjekt.WPF.ProjectWindow(p);
                if (win.ShowDialog() == true) await _projectPresenter.EditProject(win.Project);
            }
            else if (res == DialogResult.No) await _projectPresenter.DeleteProject(p.Id);
        }

        private async void TaskList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != TaskList.Columns["Actions"].Index) return;
            var t = TaskList.Rows[e.RowIndex].DataBoundItem as ProjectTask;
            var res = MessageBox.Show("Edit (Yes) or Delete (No)?", "Action", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                var win = new KooliProjekt.WPF.ProjectTaskWindow(t);
                if (win.ShowDialog() == true) await _taskPresenter.EditTask(win.Task);
            }
            else if (res == DialogResult.No) await _taskPresenter.DeleteTask(t.Id);
        }

        private async void MemberList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != MemberList.Columns["Actions"].Index) return;
            var m = MemberList.Rows[e.RowIndex].DataBoundItem as TeamMember;
            var res = MessageBox.Show("Edit (Yes) or Delete (No)?", "Action", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                var win = new KooliProjekt.WPF.TeamMemberWindow(m);
                if (win.ShowDialog() == true) await _memberPresenter.EditMember(win.TeamMember);
            }
            else if (res == DialogResult.No) await _memberPresenter.DeleteMember(m.Id);
        }

        #endregion
    }
}
