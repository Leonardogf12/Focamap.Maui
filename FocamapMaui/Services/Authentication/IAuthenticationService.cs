using FocamapMaui.Models;

namespace FocamapMaui.Services.Authentication
{
    public interface IAuthenticationService
	{
		Task LoginAsync(string email, string password);

		Task ResetPasswordAsync(string email);

		Task RegisterNewUserAsync(string name, string email, string password, City city);

        Task<string> UpdateUserProfile(string email, string password, string newName);
    }
}

