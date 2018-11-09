using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;

namespace Movies4U.Models
{
    public class Twitter
    {
        public Tweetinvi.Models.IAuthenticatedUser AuthenticatedUser;

        public Twitter()
        {
            Auth.SetUserCredentials("31cBCj2DFd7MfRoX1J6cveKT0", "4hw44qvNrETWRQksQmZWqyf08Py9TkrqJagXOqG1RiPnAzvAAy", "112794618-mFr8f3lkZ2X9by107AW5UNtFXqTC4KvH89qNRwJG", "Lgv4YVHc87v4nYcQXmEGGUbY8sNtpaoxUJ1urAGOnKmus");

            AuthenticatedUser = User.GetAuthenticatedUser();
        }
    }
}
