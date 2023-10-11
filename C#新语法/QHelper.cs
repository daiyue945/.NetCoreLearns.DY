using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_新语法;

    internal class QHelper : IDisposable
    {
        public string GetName()
        {
            return "aa";
        }

        void IDisposable.Dispose()
        {
            Console.WriteLine($"QHelper Disposed");
        }
    }
