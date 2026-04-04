using System.Collections.Generic;

namespace Task_6_R
{
    public class Question
    {
        public string Text { get; set; }           // Текст вопроса
        public string Type { get; set; }           // grammar, stress, ending
        public string CorrectAnswer { get; set; }  // Правильный ответ
        public List<string> Answers { get; set; }  // Варианты ответов
        public string ImagePath { get; set; }      // Путь к изображению

        public Question()
        {
            Answers = new List<string>();
            ImagePath = "";
        }
    }
}