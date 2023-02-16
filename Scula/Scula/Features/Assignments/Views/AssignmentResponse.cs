using Scula.Features.Subject.Views;

namespace Scula.Features.Assignments.Views;

public class AssignmentResponse
{
    public string Id { get; set; }
    
    public SubjectResponse Subject { get; set; }
    
    public string Description { get; set; }
    
    public double Grade { get; set; }
    
    public DateTime DeadLine { get; set; }
}