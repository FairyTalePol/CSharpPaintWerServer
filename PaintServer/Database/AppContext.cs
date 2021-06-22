using Microsoft.EntityFrameworkCore;

namespace PaintServer.Database
{
    public class AppContext:DbContext
    {
        private static AppContext _appContext;
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatistics> Statistics { get; set; }

        public DbSet<Pictures> Pictures { get; set; }
        private AppContext()
        {
            Database.EnsureCreated();
        }

        public static AppContext Create()
        {
            if(_appContext==null)
            {
                _appContext = new AppContext(); 
            }
            
            return _appContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          //Так как база локальная, то у каждого будет свой сервер...его перезадать на свой SQL сервер можно в Resource.resx в значении Server
          optionsBuilder.UseSqlServer($"Server={Resource.Server};Database={Resource.Database};Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            builder.Entity<User>()
                .HasOne(u => u.Statistics)
                .WithOne(s => s.User)
                .HasForeignKey<UserStatistics>(s => s.UserId);

            builder.Entity<User>()
                .HasMany(u => u.Pictures)
                .WithOne(s => s.User);


            builder.Entity<UserStatistics>()
                .Property(s => s.AmountBMP)
                .HasDefaultValue(0);

            builder.Entity<UserStatistics>()
               .Property(s => s.AmountJson)
               .HasDefaultValue(0);

            builder.Entity<UserStatistics>()
              .Property(s => s.AmountTotal)
              .HasComputedColumnSql("[AmountBMP]+[AmountJson]");
        }
    }
}
