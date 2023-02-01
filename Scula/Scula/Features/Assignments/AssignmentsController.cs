using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scula.DataBase;
using Scula.Features.Assignments.Models;
using Scula.Features.Assignments.Views;

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
    public async Task<AssignmentResponse> Add(AssignmentRequest request)
    {
        var assignment = new AssignmentModel //mapping
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            Description = request.Description,
            DeadLine = request.DeadLine
        };
        var response = await _dbContext.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();
        //_mockDb.Add(assignment);
        
        return new AssignmentResponse
        {
            Id = response.Entity.Id,
            Subject = response.Entity.Subject,
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
                Subject = assignment.Subject,
                Description = assignment.Description,
                DeadLine = assignment.DeadLine
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
            Subject = entity.Subject,
            Description = entity.Description,
            DeadLine = entity.DeadLine
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
            Subject = entity.Subject,
            Description = entity.Description,
            DeadLine = entity.DeadLine
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
        entity.Subject = request.Subject;
        entity.Description = request.Description;
        entity.DeadLine = request.DeadLine;

        await _dbContext.SaveChangesAsync();

        return new AssignmentResponse
        {
            Id = entity.Id,
            Subject = entity.Subject,
            Description = entity.Description,
            DeadLine = entity.DeadLine
        };
    }
    // fct de delete si de update -> tema
    //HttpDelete HttpPatch 
    //delete cauta o variabila, dupa id o verifica si o sterge
    //update are doi parametrii, unul de cautare si unul de update, primesti un request nou ca parametru, tema e bazata pe ultima functie
}