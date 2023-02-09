using Microsoft.EntityFrameworkCore;
using Scula.Features.Assignments.Models;
using Scula.Features.Subject.Models;
using Scula.Features.Tests.Models;

namespace Scula.DataBase;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<AssignmentModel> Assignments { get; set; }
    
    public DbSet<SubjectModel> Subjects { get; set; }
    
    public DbSet<TestModel> Tests { get; set; }
}