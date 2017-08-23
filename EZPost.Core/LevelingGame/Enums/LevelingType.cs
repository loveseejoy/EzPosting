using System.ComponentModel;

namespace EZPost.LevelingGame.Enums
{
    public enum LevelingType
    {
        /// <summary>
        /// 防止长时间不玩游戏导致游戏掉级
        /// </summary>
        [Description("Anti Decay")]
        Antidecay=1,

        /// <summary>
        /// 定级赛
        /// </summary>
        [Description(" Placement Matches Boosting")]
        Placement=2,

        /// <summary>
        /// 快速升级
        /// </summary>
        [Description("Overwatch Leveling")]
        QuickPlay
    }
}