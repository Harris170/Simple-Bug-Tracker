using Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Represents collections of all projects that have been created by the user.
    /// </summary>
    
    internal class ProjectManager
    {
        private readonly List<ProjectModel> projects;

        public ProjectManager()
        {
               projects = new List<ProjectModel>();
        }


        /// <summary>
        /// Gets a project for the user
        /// </summary>
        /// <param name="_project_title"></param>
        /// <returns>The project for the user </returns>
        public IEnumerable<ProjectModel> ViewProject(string _project_title) {
            return projects.Where(r => r.project_title == _project_title);
        }


        //---------------------------- PROJECT CRUD OPERATIONS ----------------------------//
       //---------------------------------------------------------------------------------//

        /// <summary>
        /// Creates a new empty project
        /// </summary>
        /// <param name="_new_project"></param>
        /// <exception cref="ProjectConflictException"></exception>
        public void CreateNewProject(ProjectModel _new_project, string _project_title, string _project_description) {

            foreach (ProjectModel existing_project in projects) // Checking if project with same name already exists
            {
                if (existing_project.Conflicts(_new_project))
                {
                    throw new ProjectConflictException(existing_project, _new_project);
                }
            }

            _new_project.project_title = _project_title;
            _new_project.project_description = _project_description;

            projects.Add(_new_project);
        }

        /// <summary>
        /// Deletes a project
        /// </summary>
        /// <param name="_project"></param>
        public void DeleteProject(ProjectModel _project)
        {
            var project_list = new List<ProjectModel>(projects);

            foreach (ProjectModel existing_project in project_list) // Checking if project with same name already exists, so we can delete it
            {
                if (existing_project.Conflicts(_project))
                {
                    projects.Remove(existing_project);
                }
            }

        }

        //---------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------//

    }
}
