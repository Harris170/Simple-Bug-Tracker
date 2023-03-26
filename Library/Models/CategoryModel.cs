using Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// Represents an individual category, that resides within an individual project. Used for grouping of bugs. 
    /// </summary>
    public class CategoryModel
    {
        public string category_title;
        public List<BugModel> category_bugs;

        public CategoryModel()
        {
            category_title = string.Empty;
            category_bugs = new List<BugModel>();
        }

        internal bool Conflicts(CategoryModel _new_category)
        {
            if (_new_category.category_title != category_title)
            {
                return false;
            }

            return true;    // Category with same name already exists
        }
    }
}
