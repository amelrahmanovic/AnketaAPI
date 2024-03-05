using AnketaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnketaAPI
{
    public class AppDbConext : DbContext
    {
        IConfiguration appConfig;
        public AppDbConext(IConfiguration config)
        {
            appConfig = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(appConfig.GetConnectionString("MSSQLDBConnection"));
        }
        public DbSet<User> User { get; set; }
        public DbSet<CatalogSurvey> CatalogSurvey { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<CatalogSurveyQuestion> CatalogSurveyQuestion { get; set; }
        public DbSet<UserCatalogSurvery> UserCatalogSurvery { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public DbSet<UserAnswer> UserAnswer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<CatalogSurveyQuestion>().HasKey(sc => new { sc.CatalogSurveyId, sc.QuestionId });
            modelBuilder.Entity<UserCatalogSurvery>().HasKey(sc => new { sc.CatalogSurveyId, sc.UserId });
            modelBuilder.Entity<UserAnswer>().HasKey(sc => new { sc.UserId, sc.QuestionAnswerId });
        }
    }
}
