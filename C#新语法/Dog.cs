using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_新语法
{
    internal class Dog
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public Dog(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
