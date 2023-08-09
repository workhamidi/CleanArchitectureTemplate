using CleanArcTemp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArcTemp.Application.Common.Interfaces;

public interface IIdentityService
{
    public Task<(IdentityResult, string?)> CreateUserAsync(User user, string password, string role);

    public Task<(IdentityResult, string?, string?)> LoginWithUsernameAsync(string username, string password);

    public Task LogOutUserAsync();

    Task<(IdentityResult, string?)> IsInRoleAsync(string userId, string role);

    Task<(IdentityResult, string?)> AuthorizeAsync(string userId, string policyName);

}