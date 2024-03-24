namespace AnketaAPI.ViewModels.IdentitiVM
{
    public class ApplicationUserVM
    {
        public int? AccessFailedCount { get;set; }
        public string? Email { get;set; }
        public bool? EmailConfirmed { get;set; } 
        public string? Id { get;set; }
        public bool? LockoutEnabled { get;set; }
        public bool? LockoutEnd { get;set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }  
        public bool? TwoFactorEnabled { get;set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string>? Roles { get; set; }
    }
}
