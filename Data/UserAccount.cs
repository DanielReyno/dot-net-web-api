using Microsoft.AspNetCore.Identity;

namespace WebAPITesting.Data
{
    public class UserAccount : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
