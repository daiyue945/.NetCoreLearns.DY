using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    class Book
    {
        public long Id { get; set; }//主键
        public string TiTle { get; set; }//标题
        public DateTime PubTime { get; set; }//发布日期
        public double Price { get; set; }//单价
        public string AuthorName { get; set; }//作者名称
        public int Age { get; set; }
        public int Age2 { get; set; }
    }
}
