using Microsoft.CSharp.RuntimeBinder;
using Nest;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ConsoleApp8
{
    /// <summary>
    /// VendorPrice 实体
    /// </summary>
    [ElasticsearchType(IdProperty = "priceID", Name = "VendorPriceInfo")]
    public class VendorPriceInfo
    {
        public Int64 priceID { get; set; }

        public DateTime modifyTime { get; set; }
        /// <summary>
        /// 如果string 类型的字段不需要被分析器拆分，要作为一个正体进行查询，需标记此声明，否则索引的值将被分析器拆分
        /// </summary>

        public string pvc_Name { get; set; }
        /// <summary>
        /// 设置索引时字段的名称
        /// </summary>

        public string pvc_Desc { get; set; }
        /// <summary>
        /// 如需使用坐标点类型需添加坐标点特性，在maping时会自动映射类型
        /// </summary>

        public GeoLocation Location { get; set; }
    }
}
