using Abp.Domain.Entities;

namespace EZPost.LevelingGame
{
    public class Game:Entity
    {
        public  string GameName { set; get; }

        public  string GameDesc { set; get; }

        public  string GamePic { set; get; }
    }
}