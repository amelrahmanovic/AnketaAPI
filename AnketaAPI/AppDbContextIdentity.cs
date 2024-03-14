﻿using AnketaAPI.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnketaAPI
{
    public class AppDbContextIdentity : IdentityDbContext<ApplicationUser>
    {
        public AppDbContextIdentity(DbContextOptions<AppDbContextIdentity> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
