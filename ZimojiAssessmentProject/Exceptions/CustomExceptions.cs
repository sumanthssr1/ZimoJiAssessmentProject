namespace ZimojiAssessmentProject.Exceptions
{
    
        public class UserNotFoundException : Exception
        {
            public UserNotFoundException() : base("User not found.") { }

            public UserNotFoundException(string message) : base(message) { }

            public UserNotFoundException(string message, Exception innerException)
                : base(message, innerException) { }
        }

        public class InvalidUserRoleException : Exception
        {
            public InvalidUserRoleException() : base("Invalid user Task.") { }

            public InvalidUserRoleException(string message) : base(message) { }
        }

    }


