using System.Windows;
using KooliProjekt.Models;

namespace KooliProjekt.WPF
{
    public partial class TeamMemberWindow : Window
    {
        public TeamMember TeamMember { get; private set; }

        public TeamMemberWindow()
        {
            InitializeComponent();
            TeamMember = new TeamMember();
        }

        public TeamMemberWindow(TeamMember member)
        {
            InitializeComponent();
            TeamMember = new TeamMember
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email
            };

            NameTextBox.Text = TeamMember.Name;
            EmailTextBox.Text = TeamMember.Email;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Name is required.");
                return;
            }

            TeamMember.Name = NameTextBox.Text;
            TeamMember.Email = EmailTextBox.Text;

            DialogResult = true;
        }
    }
}
