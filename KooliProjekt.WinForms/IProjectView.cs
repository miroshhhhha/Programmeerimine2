using System.Collections.Generic;
using KooliProjekt.Models;

namespace KooliProjekt.WinForms
{
    // Интерфейс Вида (что форма должна уметь показывать)
    public interface IProjectView
    {
        void DisplayProjects(List<Project> projects);
        void ShowError(string message);
    }
}
