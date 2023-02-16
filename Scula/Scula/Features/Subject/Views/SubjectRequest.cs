using System.ComponentModel.DataAnnotations;

namespace Scula.Features.Subject.Views;

public class SubjectRequest
{
    [Required] public string Name { get; set; }
    [Required] public string ProfessorMail { get; set; }
}