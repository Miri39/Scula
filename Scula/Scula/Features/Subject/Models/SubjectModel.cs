using Scula.Base;
using Scula.Features.Assignments.Models;
using Scula.Features.Tests.Models;

namespace Scula.Features.Subject.Models;

public class SubjectModel : Model
{
    public string Name { get; set; }

    public string ProfessorMail { get; set; }

    public List<Double> Grades { get; set; }
    public AssignmentModel Assignment { get; set; }
    public List<TestModel> Tests { get; set; }
}