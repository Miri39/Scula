using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scula.DataBase;
using Scula.Features.Subject.Views;
using Scula.Features.Tests.Models;
using Scula.Features.Tests.Views;

namespace Scula.Features.Tests;

[ApiController]
[Route ("tests")]

public class TestsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TestsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<TestResponse>> Add(string subjectName, TestRequest request)
    {
        var subject = await _dbContext.Subjects
            .FirstOrDefaultAsync(x => subjectName == x.Name);
        if (subject is null) return NotFound("Subject does not exist");
        var test = new TestModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = subject,
            Grade = request.Grade,
            Description = request.Description
        };
        var response = await _dbContext.AddAsync(test);
        await _dbContext.SaveChangesAsync();

        return new TestResponse()
        {
            Id = test.Id,
            Grade = test.Grade,
            Description = test.Description,
            Subject = new SubjectResponse()
            {
                Id = test.Subject.Id,
                Name = test.Subject.Name,
                ProfessorMail = test.Subject.ProfessorMail
            }
        };
    }

    [HttpGet]
    public async Task<IEnumerable<TestResponse>> Get()
    {
        var entities = await _dbContext.Tests.ToListAsync();
        return entities.Select(
            test => new TestResponse()
                {
                    Id = test.Id,
                    Description = test.Description,
                    Grade = test.Grade,
                    Subject = new SubjectResponse()
                    {
                        Id = test.Subject.Id,
                        Name = test.Subject.Name,
                        ProfessorMail = test.Subject.ProfessorMail
                    }
                });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TestResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return NotFound();
        }

        return new TestResponse
        {
            Id = entity.Id,
            Description = entity.Description,
            Grade = entity.Grade,
            Subject = new SubjectResponse()
            {
                Id = entity.Subject.Id,
                Name = entity.Subject.Name,
                ProfessorMail = entity.Subject.ProfessorMail
            }
        };
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TestResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);

        if(entity is null)
        {
            return NotFound();
        }

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        
        return new TestResponse
        {
            Id = entity.Id,
            Description = entity.Description,
            Grade = entity.Grade,
            Subject = new SubjectResponse()
            {
                Id = entity.Subject.Id,
                Name = entity.Subject.Name,
                ProfessorMail = entity.Subject.ProfessorMail
            }
        };
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult<TestResponse>> Update([FromRoute] string id, TestRequest request)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);

        if(entity is null)
        {
            return NotFound();
        }
        
        entity.Description = request.Description;
        entity.Grade = request.Grade;
        entity.Updated = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        
        return new TestResponse
        {
            Id = entity.Id,
            Description= entity.Description,
            Grade = entity.Grade,
            Subject = new SubjectResponse()
            {
                Id = entity.Subject.Id,
                Name = entity.Subject.Name,
                ProfessorMail = entity.Subject.ProfessorMail
            }
        };
    }
}