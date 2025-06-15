namespace SchoolManagement.Domain.HelperClass;

public class StudentHomeWorkDto
{
    public string studentId { get; set; }
    public string studentName { get; set; }
    public string fileUrl { get; set; }
    public string fileName { get; set; }
    public DateTime Dateline { get; set; }
    public DateTime submittedDate { get; set; }
}