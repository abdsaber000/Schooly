using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities
{
    public class Teacher : ApplicationUser
    {
        public override Role Role { get; set; } = Role.Teacher;
    }
}
