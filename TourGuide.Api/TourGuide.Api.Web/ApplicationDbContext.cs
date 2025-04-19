using TourGuide.Api.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace TourGuide.Api.Web;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
