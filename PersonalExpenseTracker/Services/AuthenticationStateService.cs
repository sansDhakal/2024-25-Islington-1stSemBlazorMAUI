using PersonalExpenseTracker.Models;
using System;


namespace PersonalExpenseTracker.Services
{
    public class AuthenticationStateService
    {
        private User authenticatedUser;

        public event Action? OnStateChanged;

        private void NotifyStateChanged() => OnStateChanged?.Invoke();

        public User GetAuthenticatedUser()
        {
            return authenticatedUser;
        }

        public void SetAuthenticatedUser(User user)
        {
            authenticatedUser = user;
            NotifyStateChanged();
        }

        public bool IsAuthenticated()
        {
            if (authenticatedUser != null)
            {
                return true;
            }
            return false;
        }

        public void Logout()
        {
            authenticatedUser = null;
            NotifyStateChanged();
        }
    }
}