using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class UserModel : IdentityUser
{
    public string Name { get; set; }
}