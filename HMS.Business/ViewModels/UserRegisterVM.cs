namespace HMS.Business.ViewModels
{
    public class UserRegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Detail { get; set; }
        public string  PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public bool StayLoggedIn { get; set; }
    }
}