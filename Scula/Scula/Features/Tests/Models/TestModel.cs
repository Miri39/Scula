using Scula.Base;
using Scula.Features.Subject.Models;

namespace Scula.Features.Tests.Models;

public class TestModel : Model
{
    public string SubjectId { get; set; }

    public string Description { get; set; }
    
    public double Grade { get; set; }
    
}