using Scula.Base;

namespace Scula.Features.Assignments.Models;

public class AssignmentModel : Model
{
    public string SubjectId { get; set; }
    public string Description { get; set; }
    
    public DateTime DeadLine { get; set; }

    public double Grade { get; set; }
}