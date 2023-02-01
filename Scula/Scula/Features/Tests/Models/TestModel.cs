using Scula.Base;
using Scula.Features.Subject.Models;

namespace Scula.Features.Tests.Models;

public class TestModel : Model
{
    public string Title { get; set; }

    public DateTime TestDate { get; set; }
    public List<SubjectModel> Subjects { get; set; }
}