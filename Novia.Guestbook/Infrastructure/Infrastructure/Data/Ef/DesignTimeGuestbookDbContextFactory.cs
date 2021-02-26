using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace Novia.Guestbook.Infrastructure.Data.Ef
{
    public class DesignTimeGuestbookDbContextFactory :
   IDesignTimeDbContextFactory<GuestbookDbContext>
    {
        public GuestbookDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<GuestbookDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new GuestbookDbContext(builder.Options);
        }
    }
}