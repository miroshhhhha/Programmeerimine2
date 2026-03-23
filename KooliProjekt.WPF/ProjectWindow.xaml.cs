using System;
using System.Windows;
using KooliProjekt.Models;

namespace KooliProjekt.WPF
{
    public partial class ProjectWindow : Window
    {
        public Project Project { get; private set; }

        public ProjectWindow(Project project = null)
        {
            InitializeComponent();
            Project = project ?? new Project { StartDate = DateTime.Now };
            
            NameTextBox.Text = Project.Name;
            DescriptionTextBox.Text = Project.Description;
            StartDatePicker.SelectedDate = Project.StartDate;
            EndDatePicker.SelectedDate = Project.EndDate;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Name is required!");
                return;
            }

            Project.Name = NameTextBox.Text;
            Project.Description = DescriptionTextBox.Text;
            Project.StartDate = StartDatePicker.SelectedDate ?? DateTime.Now;
            Project.EndDate = EndDatePicker.SelectedDate;

            DialogResult = true;
            Close();
        }
    }
}
