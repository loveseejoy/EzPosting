using Abp.Domain.Entities;

namespace EZPost.LevelingGame
{
    public class Product:Entity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public  string Title { set; get; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public  string Decs { set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public  decimal Price { set; get; } 

        /// <summary>
        /// 图片地址
        /// </summary>
        public  string PicUrl { set; get; }
    }
}