using Microsoft.EntityFrameworkCore;
using Scula.Features.Assignments.Models;

namespace Scula.DataBase;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<AssignmentModel> Assignments { get; set; }
}