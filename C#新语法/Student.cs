using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_新语法
{
    internal class Student
    {
        public string? Name { get; set; }
        public string PhoneNumber { get; set; }
        public Student(string PhoneNumber)
        {
            this.PhoneNumber = PhoneNumber;            
        }

    }
}
