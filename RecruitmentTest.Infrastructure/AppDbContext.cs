using Microsoft.EntityFrameworkCore;

namespace RecruitmentTest.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}