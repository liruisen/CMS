using System;
using System.Collections.Generic;
using System.Text;

namespace DapperSample
{
    public class Content
    {
        /// <summary>
        /// 主键
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
        public int status { get; set; } = 1;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime add_time { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? modify_time { get; set; }
        public override string ToString()
        {
            return $"id:{this.id}\ntitle:{this.title}\ncontent:{this.content}\nstatus:{this.status}\nadd_time:{this.add_time}\nmodify_time:{this.modify_time}";
        }
    }
}
