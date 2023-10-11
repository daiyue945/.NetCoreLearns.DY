using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    /// <summary>
    /// 订单表
    /// 表名实现IJuhegen接口的都是聚合根对象，仅做区分用
    /// </summary>
    internal class Order: IJuhegen
    {
        public int Id { get; set; }

        public DateTime CreateDateTime { get; set; }
        public double TotalAmount { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public void AddDeatil(Merchan merchan,int count)
        {
            var detail = OrderDetails.SingleOrDefault(x => x.Merchan == merchan);
            if (detail == null)
            {
                detail=new OrderDetail { Merchan = merchan, Count = count };
                OrderDetails.Add(detail);
            }
            else
            {
                detail.Count += count;
            }
        }

    }
}
