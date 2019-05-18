using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample01.Models
{
    public class Content
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 状态 1正常 0删除
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime modify_time { get; set; }
    }
    public class ContentViewModel
    {
        public List<Content> Contents { get; set; }
    }
}
