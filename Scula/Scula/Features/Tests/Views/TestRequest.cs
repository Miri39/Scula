using System.ComponentModel.DataAnnotations;

namespace Scula.Features.Tests.Views;

public class TestRequest
{
    [Required] public string Description { get; set; }
    
    [Required] public double Grade { get; set; }
}