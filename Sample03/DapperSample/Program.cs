using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperSample
{
    class Program
    {
        private static string connectionStr = "Data Source=localhost;User ID=test;integrated security=true;Initial Catalog=DapperSample;Pooling=true;Max Pool Size=100;";
        static void Main(string[] args)
        {
            int beginCount = Count();
            Console.WriteLine($"当前数据条数为：{beginCount}");
            //update(16);
            select(new int[] { 1, 4, 6, 7 });
            //delete(3);
            //delete(11, 15, 19);
            int afterCount = Count();
            //insert(a+1,5);
            //select();
            //insertComment(4,3);
            //selectCommentWithContent(4);
            Console.WriteLine($"当前数据条数为：{afterCount}");
            Console.Read();
        }
        #region 插入Content
        /// <summary>
        /// 插入
        /// </summary>
        static void insert()
        {
            var content = new Content()
            {
                title = "标题2",
                content = "这是正文2"
            };
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"insert into [Content] (title,[content],status,add_time,modify_time) values(@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql, content);
                Console.WriteLine($"insert插入了{result}条数据!");
            }
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        static void insert(int begin, int count)
        {
            List<Content> list = new List<Content>();
            for (int i = begin, after = begin + count; i < after; i++)
            {
                list.Add(new Content()
                {
                    title = $"标题{i}",
                    content = $"这是正文{i}"
                });
            }
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"insert into [Content] (title, [content], status, add_time, modify_time) values(@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql, list);
                Console.WriteLine($"insert插入了{result}条数据!");
            }
        }
        #endregion

        #region 查询Content
        static void select(int id)
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"select * from [content] where id=@id";
                var result = conn.QuerySingleOrDefault<Content>(sql, new { id = id });
                Console.WriteLine($"查到的数据为:\n{result.ToString()}");
            }
        }
        static void select(int[] intarry)
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                //string sql = @"SELECT * FROM [content] WHERE id in @ids";
                string sql = @"select * from [dbo].[content] where id in @ids";
                var result = conn.Query<Content>(sql, new { ids = intarry });
                foreach (var item in result)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
        #endregion

        #region 查询Content总数
        /// <summary>
        /// 查询Content总数
        /// </summary>
        /// <returns></returns>
        static int Count()
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"select Count(1) from [content] ";
                var result = conn.QueryFirstOrDefault<int>(sql);
                //Console.WriteLine($"查到的数据为:\n{result.ToString()}");
                return result;
            }
        } 
        #endregion

        #region 更新Content
        static void update(int id)
        {
            var content = new Content()
            {
                id = id,
                title = $"标题{id}",
                content = $"修改过后的正文{id}"
            };
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"update [content] set title=@title,content=@content,modify_time=GETDATE() where id=@id";
                var result = conn.Execute(sql, content);
                Console.WriteLine($"修改了{result}条数据");
                select(id);
            }
        }
        static void update(params int[] intlist)
        {
            List<Content> list = new List<Content>();
            foreach (var item in intlist)
            {
                list.Add(new Content()
                {
                    id = item,
                    content = $"批量修改正文{item}"
                });
            }
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"UPDATE [content] SET content=@content,modify_time=GETDATE() WHERE id=@id";
                var result = conn.Execute(sql, list);
                Console.WriteLine($"修改了{result}条数据");
            }
        }
        #endregion

        #region 删除Content
        static void delete(int id)
        {
            var content = new Content()
            {
                id = id
            };
            string sql = @"delete from [content] where id=@id";
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                var result = conn.Execute(sql, content);
                Console.WriteLine($"删除了{result}条数据");
            }
        }
        static void delete(params int[] intlist)
        {
            List<Content> list = new List<Content>();
            foreach (var item in intlist)
            {
                list.Add(new Content() { id = item });
            }
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                string sql = @"delete from [content] where id=@id";
                var result = conn.Execute(sql, list);
                Console.WriteLine($"删除了{result}条数据");
            }
        }
        #endregion

        #region 添加Comment
        static void insertComment(int contentId)
        {
            Comment comment = new Comment()
            {
                content = $"文章{contentId}的评论",
                content_id = contentId
            };
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"insert into [comment](content,content_id,add_time) values (@content,@content_id,GETDATE())";
                var result = conn.Execute(sql, comment);
                Console.WriteLine($"添加条数:{result}");
            }
        }
        static void insertComment(int contentId, int count)
        {
            List<Comment> list = new List<Comment>();
            for (int i = 1; i <= count; i++)
            {
                list.Add(new Comment()
                {
                    content = $"文章{contentId}的评论{i}",
                    content_id = contentId
                });
            }

            using (var conn = new SqlConnection(connectionStr))
            {
                string sql = @"insert into [comment](content,content_id,add_time) values (@content,@content_id,GETDATE())";
                var result = conn.Execute(sql, list);
                Console.WriteLine($"添加条数:{result}");
            }
        } 
        #endregion

        static void selectCommentWithContent(int id)
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                string sql_insert = @"select * from content where id=@id;select * from comment where content_id=@id;";
                using (var result = conn.QueryMultiple(sql_insert, new { id = 5 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容5的评论数量{content.comments.Count()}");
                }
            }
        }
    }
}
