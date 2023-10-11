using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    /// <summary>
    /// 商品实体
    /// </summary>
    internal class CommodityEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CurrencyName currencyName { get; set; }//货币名称
    }
    /// <summary>
    /// 货币名称
    /// </summary>
    enum CurrencyName
    {
        CNY,USD,NZD
    }
}
