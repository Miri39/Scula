using Microsoft.AspNetCore.Mvc;
using Scula.Features.Assignments.Models;
using Scula.Features.Assignments.Views;

namespace Scula.Features.Assignments;

[ApiController]
[Route("assignments")]
public class AssignmentsController
{
    private static List<AssignmentModel> _mockDb = new List<AssignmentModel>();

    public AssignmentsController() { }

    [HttpPost]
    public AssignmentResponse Add(AssignmentRequest request)
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
        _mockDb.Add(assignment);

        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
    }

    [HttpGet]
    public IEnumerable<AssignmentResponse> Get()
    {
        return _mockDb.Select(
            assignment => new AssignmentResponse()
            {
                Id = assignment.Id,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DeadLine = assignment.DeadLine
            }).ToList();
    }

    [HttpGet("id")]
    public AssignmentResponse Get([FromRoute] string id)
    {
        var assignment = _mockDb.FirstOrDefault(x => x.Id == id);
        if (assignment is null)
        {
            return null;
        }
        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
    }
    // fct de delete si de update -> tema
    //HttpDelete HttpPatch 
    //delete cauta o variabila, dupa id o verifica si o sterge
    //update are doi parametrii, unul de cautare si unul de update, primesti un request nou ca parametru, tema e bazata pe ultima functie
}