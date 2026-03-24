using System.Collections.Generic;
using KooliProjekt.Models;

namespace KooliProjekt.WinForms
{
    public interface IProjectTaskView
    {
        void DisplayTasks(List<ProjectTask> tasks);
        void ShowError(string message);
    }
}
