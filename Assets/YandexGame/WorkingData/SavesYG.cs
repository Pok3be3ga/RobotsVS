
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "en";
        public bool promptDone;
        // Ваши сохранения

        // ...
        public int HealthLevel;
        public int DamageLevel;
        public int LootLevel;
        public int Chapter;
        public float Coins;
        public int NumberOfWaves = 5;
        public int NumberOfEnvironment;

        public bool[] RobotBuy = { false, false, false };
        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
        }
    }
}
