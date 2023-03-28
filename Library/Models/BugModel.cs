using System;

namespace Library.Models
{
    /// <summary>
    /// Represents an individual bug, that resides within an individual project and within the category list of bugs, of the project.
    /// </summary>
    public class BugModel
    {
        public string bug_title;
        public string bug_description;
        public string bug_status;
        public bool bug_is_completed;
        public DateTime bug_created_on;
        public CategoryModel bug_category;

        public BugModel()
        {
            bug_title = string.Empty;
            bug_description = string.Empty;
            bug_status = string.Empty;
            bug_is_completed = false;
            bug_created_on = DateTime.Now;
            bug_category = new CategoryModel();
        }


        internal bool Conflicts(BugModel _new_bug)
        {
            if (_new_bug.bug_title != bug_title)
            {
                return false;
            }

            return true;    // Bug with same name already exists
        }
    }
}
