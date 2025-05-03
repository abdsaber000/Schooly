using SchoolManagement.Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Teacher : ApplicationUser
    {
        public override Role Role { get; set; } = Role.Teacher;
    }
}
