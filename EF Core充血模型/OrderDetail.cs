using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    /// <summary>
    /// 订单明细
    /// //为什么划分聚合？便于以后进行微服务拆分
    /// </summary>
    internal class OrderDetail: IJuhegen
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Merchan Merchan { get; set; }
        //public long MerchanId { get; set; }//跨聚合进行实体引用，只能引用根实体，并且只能引用根实体的ID，而不是根实体的对象
        public int Count { get; set; }
    }
}
