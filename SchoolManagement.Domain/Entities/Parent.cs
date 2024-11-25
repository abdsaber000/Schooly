using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities;

public class Parent
{
    public string ParentId { get; set; }
    public string ParentName { get; set; }
    public Relation Relation { get; set; }
    public string Job { get; set; }
    public string Phone1 { get; set; }
    public string? Phone2 { get; set; }
}