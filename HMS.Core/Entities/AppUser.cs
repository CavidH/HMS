using Microsoft.AspNetCore.Identity;

namespace HMS.Core.Entities
{
    public class AppUser: IdentityUser
    {   
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsActivated { get; set; }
        //public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
