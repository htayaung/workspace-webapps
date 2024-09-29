using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MyTasks.Data;

namespace MyTasks.Pages
{
    public abstract class AuthBaseComponent : ComponentBase
    {
        [Inject]
        protected IUnitOfWork UnitOfWork { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationState { get; set; }

        [Inject]
        protected UserManager<ApplicationUser> UserManager { get; set; }

        /// <summary>
        /// Current logged in user id
        /// </summary>
        protected Guid UserId
        {
            get
            {
                return GetUserId();
            }
        }

        protected Guid GetUserId()
        {
            var user = AuthenticationState.GetAuthenticationStateAsync().Result.User;
            return UserManager.GetUniqueUserId(user);
        }

        protected async Task<Guid> GetUserIdAsync()
        {
            var user = (await AuthenticationState.GetAuthenticationStateAsync()).User;
            return UserManager.GetUniqueUserId(user);
        }

        protected void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // TODO: log error message
                // TODO: show custom error message

                throw;
            }
        }

        protected async System.Threading.Tasks.Task ExecuteAsync(Func<System.Threading.Tasks.Task> function)
        {
            try
            {
                await function();
            }
            catch (Exception ex)
            {
                // TODO: log error message
                // TODO: show custom error message

                throw;
            }
        }
    }
}
