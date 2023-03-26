using Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ProjectManager projectBook = new ProjectManager();
            ProjectModel project1 = new ProjectModel();
            ProjectModel project2 = new ProjectModel();
            BugModel new_bug = new BugModel();
            CategoryModel new_category = new CategoryModel();


            projectBook.CreateNewProject(project1, "First Project", "Project Description");
            projectBook.CreateNewProject(project2, "Second Project", "Project Description");

            project1.CreateBug(new_bug, "Bug 1", "Bug Description", "Incomplete", new_category);
            project1.CreateCategory(new_category, "High Priority");
            project1.AddBugToCategory(new_bug,new_category);
            project1.UpdateBug(new_bug, null,null, "In Progress");
            project1.DeleteBug(new_bug);
            project1.DeleteBugFromCategory(new_bug, new_category);
            project1.DeleteCategory(new_category);


            projectBook.DeleteProject(project1);
            base.OnStartup(e);
        }
    }
}
