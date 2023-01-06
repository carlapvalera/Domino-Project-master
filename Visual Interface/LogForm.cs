using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;

namespace Visual_Interface
{
    class LogForm : IForm
    {
        GameForm CurrentGameForm { get; }
        public LogForm(GameForm gameForm)
        {
            CurrentGameForm = gameForm;
        }
        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Press any key - Back");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Log: ");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < CurrentGameForm.Game_.Log.Count; i++)
                Console.Write(CurrentGameForm.Game_.Log[i]);
            ConsoleKeyInfo key = Console.ReadKey();
            Console.ResetColor();
            if (CurrentGameForm.Game_.IsEndGame.Item1)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else 
                CurrentGameForm.Show();
        }
    }
}
