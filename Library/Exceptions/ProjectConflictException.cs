using Library.Models;
using System;

namespace Library.Exceptions
{

    internal class ProjectConflictException : Exception
    {
        public ProjectModel existing_project { get; }
        public ProjectModel incoming_project { get; }

        public ProjectConflictException(ProjectModel existing_project, ProjectModel incoming_project)
        {
            this.existing_project = existing_project;
            this.incoming_project = incoming_project;
        }

        public ProjectConflictException(string? message, ProjectModel existing_project, ProjectModel incoming_project) : base(message)
        {
            this.existing_project = existing_project;
            this.incoming_project = incoming_project;
        }

        public ProjectConflictException(string? message, Exception? innerException, ProjectModel existing_project, ProjectModel incoming_project) : base(message, innerException)
        {
            this.existing_project = existing_project;
            this.incoming_project = incoming_project;
        }
    }
}
