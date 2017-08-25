using System.ComponentModel;

namespace EZPost.LevelingGame.Enums
{
    public enum OrderStatus
    {
        [Description("未付款")]
        NoPay=0,


        [Description("已付款")]
        Payed =1
    }
}