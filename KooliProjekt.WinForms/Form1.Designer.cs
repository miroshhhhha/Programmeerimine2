namespace KooliProjekt.WinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.projectsTab = new System.Windows.Forms.TabPage();
            this.tasksTab = new System.Windows.Forms.TabPage();
            this.teamTab = new System.Windows.Forms.TabPage();
            this.ProjectList = new System.Windows.Forms.DataGridView();
            this.TaskList = new System.Windows.Forms.DataGridView();
            this.MemberList = new System.Windows.Forms.DataGridView();
            this.LoadProjectsButton = new System.Windows.Forms.Button();
            this.AddProjectButton = new System.Windows.Forms.Button();
            this.LoadTasksButton = new System.Windows.Forms.Button();
            this.AddTaskButton = new System.Windows.Forms.Button();
            this.LoadMembersButton = new System.Windows.Forms.Button();
            this.AddMemberButton = new System.Windows.Forms.Button();

            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);

            // Projects Tab
            this.projectsTab.Controls.Add(this.ProjectList);
            this.projectsTab.Controls.Add(this.LoadProjectsButton);
            this.projectsTab.Controls.Add(this.AddProjectButton);
            this.projectsTab.Text = "Projects";

            this.LoadProjectsButton.Location = new System.Drawing.Point(10, 10);
            this.LoadProjectsButton.Size = new System.Drawing.Size(100, 30);
            this.LoadProjectsButton.Text = "Load Projects";
            this.LoadProjectsButton.Click += new System.EventHandler(this.LoadProjects_Click);

            this.AddProjectButton.Location = new System.Drawing.Point(120, 10);
            this.AddProjectButton.Size = new System.Drawing.Size(100, 30);
            this.AddProjectButton.Text = "Add Project";
            this.AddProjectButton.Click += new System.EventHandler(this.AddButton_Click);

            this.ProjectList.Location = new System.Drawing.Point(10, 50);
            this.ProjectList.Size = new System.Drawing.Size(770, 360);
            this.ProjectList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProjectList_CellContentClick);

            // Tasks Tab
            this.tasksTab.Controls.Add(this.TaskList);
            this.tasksTab.Controls.Add(this.LoadTasksButton);
            this.tasksTab.Controls.Add(this.AddTaskButton);
            this.tasksTab.Text = "Tasks";

            this.LoadTasksButton.Location = new System.Drawing.Point(10, 10);
            this.LoadTasksButton.Size = new System.Drawing.Size(100, 30);
            this.LoadTasksButton.Text = "Load Tasks";
            this.LoadTasksButton.Click += new System.EventHandler(this.LoadTasks_Click);

            this.AddTaskButton.Location = new System.Drawing.Point(120, 10);
            this.AddTaskButton.Size = new System.Drawing.Size(100, 30);
            this.AddTaskButton.Text = "Add Task";
            this.AddTaskButton.Click += new System.EventHandler(this.AddTaskButton_Click);

            this.TaskList.Location = new System.Drawing.Point(10, 50);
            this.TaskList.Size = new System.Drawing.Size(770, 360);
            this.TaskList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TaskList_CellContentClick);

            // Team Tab
            this.teamTab.Controls.Add(this.MemberList);
            this.teamTab.Controls.Add(this.LoadMembersButton);
            this.teamTab.Controls.Add(this.AddMemberButton);
            this.teamTab.Text = "Team";

            this.LoadMembersButton.Location = new System.Drawing.Point(10, 10);
            this.LoadMembersButton.Size = new System.Drawing.Size(100, 30);
            this.LoadMembersButton.Text = "Load Members";
            this.LoadMembersButton.Click += new System.EventHandler(this.LoadMembers_Click);

            this.AddMemberButton.Location = new System.Drawing.Point(120, 10);
            this.AddMemberButton.Size = new System.Drawing.Size(100, 30);
            this.AddMemberButton.Text = "Add Member";
            this.AddMemberButton.Click += new System.EventHandler(this.AddMemberButton_Click);

            this.MemberList.Location = new System.Drawing.Point(10, 50);
            this.MemberList.Size = new System.Drawing.Size(770, 360);
            this.MemberList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MemberList_CellContentClick);

            this.tabControl.TabPages.Add(this.projectsTab);
            this.tabControl.TabPages.Add(this.tasksTab);
            this.tabControl.TabPages.Add(this.teamTab);

            this.Controls.Add(this.tabControl);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Project Management System";
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage projectsTab;
        private System.Windows.Forms.TabPage tasksTab;
        private System.Windows.Forms.TabPage teamTab;
        private System.Windows.Forms.DataGridView ProjectList;
        private System.Windows.Forms.DataGridView TaskList;
        private System.Windows.Forms.DataGridView MemberList;
        private System.Windows.Forms.Button LoadProjectsButton;
        private System.Windows.Forms.Button AddProjectButton;
        private System.Windows.Forms.Button LoadTasksButton;
        private System.Windows.Forms.Button AddTaskButton;
        private System.Windows.Forms.Button LoadMembersButton;
        private System.Windows.Forms.Button AddMemberButton;
    }
}
