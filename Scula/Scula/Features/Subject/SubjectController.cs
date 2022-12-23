using Microsoft.AspNetCore.Mvc;
using Scula.Features.Subject.Models;
using Scula.Features.Subject.Views;

namespace Scula.Features.Subject;

[ApiController]
[Route("subjects")]

public class SubjectController
{
    private static List<SubjectModel> _mockDb = new List<SubjectModel>();

    [HttpPost]
    public SubjectResponse Add(SubjectRequest request)
    {
        var subject = new SubjectModel()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProfessorMail = request.ProfessorMail,
            Grades = request.grades
        };
        _mockDb.Add(subject);

        return new SubjectResponse()
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }

    [HttpGet]
    public IEnumerable<SubjectResponse> Get()
    {
        return _mockDb.Select(
            subject => new SubjectResponse()
            {
                Id = subject.Id,
                Name = subject.Name,
                ProfessorMail = subject.ProfessorMail,
                Grades = subject.Grades
            }).ToList();
    }

    [HttpGet("id")]
    public SubjectResponse Get([FromRoute] string id)
    {
        var subject = _mockDb.FirstOrDefault(x => x.Id == id);
        if (subject is null)
        {
            return null;
        }

        return new SubjectResponse()
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }
    
    [HttpDelete("{id}")]
    public SubjectResponse Delete([FromRoute] string id)
    {
        var subject = _mockDb.FirstOrDefault(x => x.Id == id);
        if (subject is null)
        {
            return null;
        }

        _mockDb.Remove(subject);
        return new SubjectResponse()
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }

    [HttpPatch("{id}")]
    public SubjectResponse Update([FromRoute] string id, SubjectRequest request)
    {
        var subject = _mockDb.FirstOrDefault(x => x.Id == id);
        if (subject is null)
        {
            return null;
        }

        subject.Name = request.Name;
        subject.ProfessorMail = request.ProfessorMail;
        subject.Grades = request.grades;
        subject.Updated = DateTime.UtcNow;
        
        return new SubjectResponse()
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }
}