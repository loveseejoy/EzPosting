using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EZPost.LevelingGame.Enums;

namespace EZPost.LevelingGame
{
    public class Order:Entity,ICreationAudited,IDeletionAudited
    {
        public  string Title { set; get; }

        public  decimal Amount { set; get; }

        public OrderStatus OrderStatus { set; get; }
       
        [ForeignKey("OrderUser")]
        public  int OrderUserId { set; get; }
        public  OrderUser OrderUser { set; get; }


        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
    }
}