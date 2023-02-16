using Scula.Features.Subject.Views;

namespace Scula.Features.Tests.Views;

public class TestResponse
{
    public string Id { get; set; }

    public string Description { get; set; }    
    
    public double Grade { get; set; }
    
    public SubjectResponse Subject { get; set; }
}