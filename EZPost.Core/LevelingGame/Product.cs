using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using EZPost.LevelingGame.Enums;

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

        /// <summary>
        /// 代练类型
        /// </summary>
        public LevelingType LevelingType { set; get; }

        /// <summary>
        /// 所属游戏
        /// </summary>
        [ForeignKey("Game")]
        public  int GameId { set; get; }
        public  Game Game { set; get; }
    }
}