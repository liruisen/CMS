using System;
using System.Collections.Generic;
using System.Text;

namespace DapperSample
{
    public class Comment
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 文章id
        /// </summary>
        public int content_id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; } = DateTime.Now;
        public override string ToString()
        {
            return $"id:{this.id}\ncontent_id:{this.content_id}\ncontent:{this.content}\nadd_time:{this.add_time}";
        }
    }
}
