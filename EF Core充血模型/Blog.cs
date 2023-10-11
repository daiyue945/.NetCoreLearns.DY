using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    internal class Blog
    {
        public int Id { get; set; }
        public MultiLangString Title { get; set; }
        public MultiLangString Body { get; set; }
    }
}
