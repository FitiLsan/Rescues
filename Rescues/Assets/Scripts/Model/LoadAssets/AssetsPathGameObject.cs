using System.Collections.Generic;


namespace Rescues
{
    public sealed class AssetsPathGameObject
    {
        #region Fields

        public static readonly Dictionary<GameObjectType, string> Object = new Dictionary<GameObjectType, string>()
        {
            { GameObjectType.Character, "Prefabs/Player/Prefabs_Player_Character" },
            { GameObjectType.Enemy, "Prefabs/Enemies/Prefabs_Enemies_Patrolling" },
            { GameObjectType.Canvas, "Prefabs/UI/Prefabs_UI_Canvas" },
            { GameObjectType.Levels, "Data/Levels" },
            { GameObjectType.Puzzles, "Prefabs/Puzzles/"},
            { GameObjectType.DialoguesComponents, "Prefabs/DialoguesComponents/"},
            { GameObjectType.BasicWritePattern, "Data/WritePatterns/BasicWrite"},
        };

        public static readonly Dictionary<ScreenType, string> Screens = new Dictionary<ScreenType, string>()
        {
            {ScreenType.GameOver, "Prefabs/UI/Screen/Prefabs_UI_Screen_GameOver"},
            {ScreenType.MainMenu, "Prefabs/UI/Screen/MainMenu"},
            {ScreenType.GameMenu, "Prefabs/UI/Screen/GameMenu"}
        };

        public static readonly Dictionary<AudioDataType, string> AudioData = new Dictionary<AudioDataType, string>()
        {
            {AudioDataType.AudioMixer, "Data/Audio/AudioMixer"},
            {AudioDataType.AudioControllerContext, "Data/Audio/AudioControllerContext"},
            {AudioDataType.HotelMusicTheme, "Prefabs/Locations/Hotel/HotelMusicTheme"},
        };

        #endregion
    }
}