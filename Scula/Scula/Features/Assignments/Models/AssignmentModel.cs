using Scula.Base;
using Scula.Features.Subject.Models;

namespace Scula.Features.Assignments.Models;

public class AssignmentModel : Model
{
    public SubjectModel Subject { get; set; }
    public string Description { get; set; }
    
    public DateTime DeadLine { get; set; }

    public double Grade { get; set; }
}