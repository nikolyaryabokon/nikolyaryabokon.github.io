using System;

namespace Task8R
{
    [Serializable]
    public class GameResult
    {
        public string PlayerName { get; set; }
        public string Difficulty { get; set; }
        public string Route { get; set; }
        public int TotalTime { get; set; }
        public bool IsOptimal { get; set; }
        public DateTime Date { get; set; }

        public GameResult(string playerName, string difficulty, string route, int totalTime, bool isOptimal)
        {
            PlayerName = playerName;
            Difficulty = difficulty;
            Route = route;
            TotalTime = totalTime;
            IsOptimal = isOptimal;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Date:dd.MM.yyyy HH:mm} | {PlayerName} | {Difficulty} | {Route} | {TotalTime} мин | {(IsOptimal ? "Оптимальный" : "Не оптимальный")}";
        }
    }
}