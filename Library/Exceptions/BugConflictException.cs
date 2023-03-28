using Library.Models;
using System;

namespace Library.Exceptions
{
    internal class BugConflictException : Exception
    {
        BugModel existing_bug { get; }
        BugModel incoming_bug { get; }

        public BugConflictException(BugModel existing_bug, BugModel incoming_bug)
        {
            this.existing_bug = existing_bug;
            this.incoming_bug = incoming_bug;
        }

        public BugConflictException(string? message, BugModel existing_bug, BugModel incoming_bug) : base(message)
        {
            this.existing_bug = existing_bug;
            this.incoming_bug = incoming_bug;
        }

        public BugConflictException(string? message, Exception? innerException, BugModel existing_bug, BugModel incoming_bug) : base(message, innerException)
        {
            this.existing_bug = existing_bug;
            this.incoming_bug = incoming_bug;
        }

    }
}
