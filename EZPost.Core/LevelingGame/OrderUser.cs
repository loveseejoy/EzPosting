using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace EZPost.LevelingGame
{
    public class OrderUser:Entity
    {
        public string Email { set; get; }
        public string Phone { set; get; }
        public  string AccountName { set; get; }
        public  string Password { set; get; }
        public  string ServerName { set; get; }
        public  string CharacterName { set; get; }

        public  int OrderId { set; get; }
    }
}