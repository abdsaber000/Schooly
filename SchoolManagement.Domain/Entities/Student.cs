using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities
{
    public class Student : ApplicationUser
    {
        public string ParentId { get; set; }
        public Parent Parent { get; set; }
        public string Address { get; set; }
        public DateOnly DateOfJoining { get; set; }
        public Department Department { get; set; }
        public Grade Grade { get; set; }
        public ICollection<StudentClassRoom> StudentClassRooms { get; set; }
        public override Role Role { get; set; } = Role.Student;
    }
}
