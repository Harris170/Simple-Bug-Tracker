using Library.Models;
using Library.Views;

using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        public MainWindow()
        {

            ProjectManager projectManager = new();
            ProjectModel project1 = new();
            BugModel new_bug = new();
            CategoryModel new_category = new();


            projectManager.CreateNewProject(project1, "First Project", "Project Description");

            project1.CreateBug(new_bug, "Bug 1", "Bug Description", "Incomplete", new_category);
            project1.CreateCategory(new_category, "High Priority");
            project1.AddBugToCategory(new_bug, new_category);
            project1.UpdateBug(new_bug, null, null, "In Progress");
            project1.DeleteBug(new_bug);
            project1.DeleteBugFromCategory(new_bug, new_category);
            project1.DeleteCategory(new_category);
            

            //TODO: A wrapper ontop of ProjectManager to manage projects and stuff

            InitializeComponent();
        }
    }
}
