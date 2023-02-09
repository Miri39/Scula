using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scula.DataBase;
using Scula.Features.Subject.Models;
using Scula.Features.Subject.Views;

namespace Scula.Features.Subject;

[ApiController]
[Route("subjects")]

public class SubjectController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SubjectController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpPost]
    public async Task<SubjectResponse> Add(SubjectRequest request)
    {
        var subject = new SubjectModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProfessorMail = request.ProfessorMail,
            Grades = request.grades
        };
        var response = await _dbContext.AddAsync(subject);
        await _dbContext.SaveChangesAsync();

        return new SubjectResponse
        {
            Id = response.Entity.Id,
            Name = response.Entity.Name,
            ProfessorMail = response.Entity.ProfessorMail,
            Grades = response.Entity.Grades
        };
    }

    [HttpGet]
    public async Task<IEnumerable<SubjectResponse>> Get()
    {
        var entities = await _dbContext.Subjects.ToListAsync();
        return entities.Select(
            subject => new SubjectResponse()
            {
                Id = subject.Id,
                Name = subject.Name,
                ProfessorMail = subject.ProfessorMail,
                Grades = subject.Grades
            });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        return new SubjectResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfessorMail = entity.ProfessorMail,
            Grades = entity.Grades
        };
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<SubjectResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        
        return new SubjectResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfessorMail = entity.ProfessorMail,
            Grades = entity.Grades
        };
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<SubjectResponse>> Update([FromRoute] string id, SubjectRequest request)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        entity.Name = request.Name;
        entity.ProfessorMail = request.ProfessorMail;
        entity.Grades = request.grades;
        entity.Updated = DateTime.UtcNow;
        
        return new SubjectResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfessorMail = entity.ProfessorMail,
            Grades = entity.Grades
        };
    }
}