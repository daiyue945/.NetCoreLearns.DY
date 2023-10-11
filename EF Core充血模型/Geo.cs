using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core充血模型
{
    /// <summary>
    /// 值对象类型:卫星定位
    /// </summary>
    record Geo
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public Geo(double longitude , double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }
    }
}
