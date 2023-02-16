using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scula.DataBase;
using Scula.Features.Assignments.Models;
using Scula.Features.Assignments.Views;
using Scula.Features.Subject.Views;

namespace Scula.Features.Assignments;

[ApiController]
[Route("assignments")]
public class AssignmentsController : ControllerBase
{
    // private static List<AssignmentModel> _mockDb = new List<AssignmentModel>();
    private readonly AppDbContext _dbContext;

        // Dependency Injections (DI) face rost de serviciile de care avem nevoie
    
    public AssignmentsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentResponse>> Add(string subjectName, AssignmentRequest request)
    {
        var subject = await _dbContext.Subjects.FirstOrDefaultAsync(x => subjectName == x.Name);
        if (subject is null) return NotFound("Subject does not exist");
        
        var assignment = new AssignmentModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = subject,
            Description = request.Description,
            DeadLine = request.DeadLine
        };
        var response = await _dbContext.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();
        //_mockDb.Add(assignment);
        
        return new AssignmentResponse
        {
            Id = response.Entity.Id,
            Description = response.Entity.Description,
            DeadLine = response.Entity.DeadLine
        };
    }

    [HttpGet]
    public async Task<IEnumerable<AssignmentResponse>> Get()
    {
        var entities = await _dbContext.Assignments.ToListAsync();
        return entities.Select(
            assignment => new AssignmentResponse()
            {
                Id = assignment.Id,
                Description = assignment.Description,
                Grade = assignment.Grade,
                DeadLine = assignment.DeadLine,
                Subject = new SubjectResponse()
                {
                    Id = assignment.Subject.Id,
                    ProfessorMail = assignment.Subject.ProfessorMail,
                    Name = assignment.Subject.Name
                }
            });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return NotFound();
        }
        return new AssignmentResponse
        {
            Id = entity.Id,
            Description = entity.Description,
            Grade = entity.Grade,
            DeadLine = entity.DeadLine,
            Subject = new SubjectResponse()
            {
                Id = entity.Subject.Id,
                ProfessorMail = entity.Subject.ProfessorMail,
                Name = entity.Subject.Name
            }
        };
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AssignmentResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        
        return new AssignmentResponse
        {
            Id = entity.Id,
            Grade = entity.Grade,
            Description = entity.Description,
            DeadLine = entity.DeadLine,
            Subject = new SubjectResponse()
            {
                Id = entity.Subject.Id,
                ProfessorMail = entity.Subject.ProfessorMail,
                Name = entity.Subject.Name
            }
        };
    }
    
    [HttpPatch("{id}") ]
    
    public async Task<ActionResult<AssignmentResponse>> Update([FromRoute] string id, AssignmentRequest request)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return NotFound();
        }

        entity.Updated = DateTime.UtcNow;
        entity.Grade = request.Grade;
        entity.Description = request.Description;
        entity.DeadLine = request.DeadLine;

        await _dbContext.SaveChangesAsync();

        return new AssignmentResponse
        {
            Id = entity.Id,
            Grade = entity.Grade,
            Description = entity.Description,
            DeadLine = entity.DeadLine,
            Subject = new SubjectResponse()
            {
                Id = entity.Subject.Id,
                ProfessorMail = entity.Subject.ProfessorMail,
                Name = entity.Subject.Name
            }
        };
    }
}