using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Polley.WebApi.Data;

namespace Polley.WebApi.Context;

public class PolleyDbContext : IdentityDbContext<User>
{
    public PolleyDbContext(DbContextOptions<PolleyDbContext> options) : base(options)
    {
    }

    public DbSet<Poll> Polls { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
}