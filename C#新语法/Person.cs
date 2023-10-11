using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_新语法
{
    internal record Person(int Id,string Name,int Age)
    {
        public string? NickName { get; set; }
        public Person(int Id, string Name, int Age,string NickName):this( Id,  Name,  Age)
        {
            this.NickName = NickName;
        }
    }
}
