using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerBrowser.Data.Models;

namespace ServerBrowser.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<ServerList> ServerLists { get; set; }

        public DbSet<ServerReview> ServerReviews { get; set; }

        public DbSet<ServerType> ServerTypes { get; set; }

        public DbSet<Timeline> Timelines { get; set; }
    }
}
