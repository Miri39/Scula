using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scula.DataBase;
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
    public async Task<TestResponse> Add(TestRequest request)
    {
        var test = new TestModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            SubjectId = request.Subject,
            Grade = request.Grade,
            Description = request.Description
        };
        var response = await _dbContext.AddAsync(test);
        await _dbContext.SaveChangesAsync();

        return new TestResponse()
        {
            Id = test.Id,
            Subject = test.SubjectId,
            Grade = test.Grade,
            Description = test.Description
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
                    Subject = test.SubjectId,
                    Description = test.Description,
                    Grade = test.Grade
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
            Subject = entity.SubjectId,
            Description = entity.Description,
            Grade = entity.Grade
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
            Subject = entity.SubjectId,
            Description = entity.Description,
            Grade = entity.Grade
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

        entity.SubjectId = request.Subject;
        entity.Description = request.Description;
        entity.Grade = request.Grade;
        entity.Updated = DateTime.UtcNow;
        
        return new TestResponse
        {
            Id = entity.Id,
            Subject = entity.SubjectId, 
            Description= entity.Description,
            Grade = entity.Grade
        };
    }
}