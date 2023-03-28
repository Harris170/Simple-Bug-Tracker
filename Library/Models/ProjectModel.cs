using Library.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Library.Models
{
    /// <summary>
    /// Represents individual project that resides within the project manager.
    /// </summary>
    public class ProjectModel
    {
        public string project_title;
        public string project_description;

        public List<BugModel> bugs { get; set; }
        public List<CategoryModel> categories { get; set; }

        public ProjectModel()
        {
            project_title = string.Empty;
            project_description = string.Empty;
            bugs = new List<BugModel>();
            categories = new List<CategoryModel>();
        }

        /// <summary>
        /// Get a bug for user
        /// </summary>
        /// <param name="_bug_title"></param>
        /// <returns>The bug for the user </returns>
        public IEnumerable<BugModel> ViewBug(string _bug_title)
        {
            return bugs.Where(r => r.bug_title == _bug_title);
        }


        /// <summary>
        /// Adds individual bug to a category of the project
        /// </summary>
        /// <param name="_new_bug"></param>
        /// <param name="_category"></param>
        /// <exception cref="BugConflictException"></exception>
        public void AddBugToCategory(BugModel _new_bug, CategoryModel _category)
        {
            foreach (CategoryModel existing_category in categories)
            {
                if (existing_category.Conflicts(_category)) // Checking if category with same name exists, so we can add new bug in it
                {

                    foreach (BugModel existing_bug in _category.category_bugs)  
                    {
                        if (existing_bug.Conflicts(_new_bug))   // Checking if bug with same name already exists within the category we want to add the bug in
                        {
                            throw new BugConflictException(existing_bug,_new_bug);
                        }
                    }

                   existing_category.category_bugs.Add(_new_bug);
                    _new_bug.bug_category = existing_category;
                }
            }
        }

        /// <summary>
        /// Removes an individual bug from a category of the project
        /// </summary>
        /// <param name="_bug"></param>
        /// <param name="_category"></param>
        public void DeleteBugFromCategory(BugModel _bug, CategoryModel _category)
        {
            foreach (CategoryModel existing_category in categories)
            {
                if (existing_category.Conflicts(_category)) // Checking if category with same name exists, so we can remove bug from it
                {
                    var bugs_list_of_category = new List<BugModel>(_category.category_bugs);

                    foreach (BugModel existing_bug in bugs_list_of_category)
                    {
                        if (existing_bug.Conflicts(_bug))   // Checking if bug with same name already exists within the category we want to remove the bug from
                        {
                            existing_category.category_bugs.Remove(_bug);
                        }
                    }


                }
            }
        }

        


        //---------------------------- BUG CRUD OPERATIONS ----------------------------//
        //-----------------------------------------------------------------------------//


        /// <summary>
        /// Creates a new bug of the project
        /// </summary>
        /// <param name="_new_bug"></param>
        /// <exception cref="BugConflictException"></exception>
        public void CreateBug(BugModel _new_bug, string _bug_title, string _bug_description, string _bug_status, CategoryModel _bug_category)
        {

            foreach (BugModel existing_bug in bugs) 
            {
                if (existing_bug.Conflicts(_new_bug)) // Checking if bug with same name already exists
                {
                    throw new BugConflictException(existing_bug, _new_bug);
                }
            }

            _new_bug.bug_title = _bug_title;
            _new_bug.bug_description = _bug_description;
            _new_bug.bug_status = _bug_status;
            _new_bug.bug_category = _bug_category;

            bugs.Add(_new_bug);
        }

        /// <summary>
        /// Deletes a bug of the project
        /// </summary>
        /// <param name="_bug"></param>
        public void DeleteBug(BugModel _bug)
        {
            var bugs_list = new List<BugModel>(bugs);

            foreach (BugModel existing_bug in bugs_list) 
            {
                if (existing_bug.Conflicts(_bug)) // Checking if bug with same name already exists, so we can delete it
                {
                    bugs.Remove(existing_bug);
                    DeleteBugFromCategory(existing_bug,existing_bug.bug_category);
                }
            }

        }

        /// <summary>
        /// Updates the bug of the project
        /// </summary>
        /// <param name="_bug"></param>
        /// <param name="_bug_title"></param>
        /// <param name="_bug_description"></param>
        /// <param name="_bug_status"></param>
        /// <param name="_bug_category"></param>
        public void UpdateBug(BugModel _bug, string? _bug_title = null, string? _bug_description = null, string? _bug_status = null, bool? _bug_is_completed = null, CategoryModel? _bug_category = null)
        {
            var bugs_list = new List<BugModel>(bugs);

            foreach (BugModel existing_bug in bugs_list) 
            {
                if (existing_bug.Conflicts(_bug)) // Checking if bug with same name already exists, so we can update it
                {
                    //  only update values if new values are provided, otherwise keep old values
                    if(_bug_title != null) _bug.bug_title = _bug_title;
                    if (_bug_description != null) _bug.bug_description = _bug_description;
                    if (_bug_status != null) _bug.bug_status = _bug_status;
                    if (_bug_is_completed != null) _bug.bug_is_completed = (bool)_bug_is_completed;
                    if (_bug_category != null) _bug.bug_category = _bug_category;
                }
            }
        }

         //-----------------------------------------------------------------------------//
        //-----------------------------------------------------------------------------//



         //------------------------- CATEGORY CRUD OPERATIONS --------------------------//
        //-----------------------------------------------------------------------------//


        /// <summary>
        /// Creates a new category of the project
        /// </summary>
        /// <param name="_new_category"></param>
        /// <exception cref="CategoryConflictException"></exception>
        public void CreateCategory(CategoryModel _new_category, string _category_title)
        {
            foreach (CategoryModel existing_category in categories)
            {
                if (existing_category.Conflicts(_new_category)) // Checking if category with same name already exists
                {
                    throw new CategoryConflictException(existing_category, _new_category);
                }
            }

            _new_category.category_title = _category_title;
            categories.Add(_new_category);
        }

        /// <summary>
        /// Deletes a category of the project
        /// </summary>
        /// <param name="_category"></param>
        public void DeleteCategory(CategoryModel _category)
        {
            var category_list = new List<CategoryModel>(categories);

            foreach (CategoryModel existing_category in category_list)
            {
                if (existing_category.Conflicts(_category)) // Checking if category with same name already exists, so we can delete it
                {
                    categories.Remove(existing_category);
                }
            }

        }

        /// <summary>
        /// Updates the category of the project
        /// </summary>
        /// <param name="_category"></param>
        /// <param name="_category_title"></param>
        /// <param name="_category_bugs"></param>
        public void UpdateCategory(CategoryModel _category, string? _category_title = null, List<BugModel>? _category_bugs = null)
        {
            var categories_list = new List<CategoryModel>(categories);

            foreach (CategoryModel existing_category in categories_list)
            {
                if (existing_category.Conflicts(_category)) // Checking if category with same name already exists, so we can update it
                {
                    //  only update values if new values are provided, otherwise keep old values
                    if (_category_title != null) _category.category_title = _category_title;
                    if (_category_bugs != null) _category.category_bugs = _category_bugs;
                }
            }

        }

        //-----------------------------------------------------------------------------//
        //-----------------------------------------------------------------------------//


        internal bool Conflicts(ProjectModel _new_project)
        {
            if(_new_project.project_title != project_title)
            {
                return false;
            }

            return true;    // Project with same name already exists
        }
    }
}
