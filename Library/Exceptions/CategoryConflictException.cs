using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    internal class CategoryConflictException : Exception
    {
        public CategoryModel existing_category { get; }
        public CategoryModel incoming_category { get; }

        public CategoryConflictException(CategoryModel existing_category, CategoryModel incoming_category)
        {
            this.existing_category = existing_category;
            this.incoming_category = incoming_category;
        }

        public CategoryConflictException(string? message, CategoryModel existing_category, CategoryModel incoming_category) : base(message)
        {
            this.existing_category = existing_category;
            this.incoming_category = incoming_category;
        }

        public CategoryConflictException(string? message, Exception? innerException, CategoryModel existing_category, CategoryModel incoming_category) : base(message, innerException)
        {
            this.existing_category = existing_category;
            this.incoming_category = incoming_category;
        }
    }
}
