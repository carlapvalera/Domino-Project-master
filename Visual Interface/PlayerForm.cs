using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;

namespace Visual_Interface
{
    class PlayerForm:IForm
    {
        List<IPrinteable> PrinteablesToPlay { get; }
        public PlayerForm(List<IPrinteable> printeablesToPlay)
        {
            PrinteablesToPlay = printeablesToPlay;
        }
        public void Show()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("How many players do you want?");
            Console.ResetColor();
            Console.WriteLine(" (1 <= x <= 4)");

            string key = Console.ReadLine();
            bool isOkKey = false;

            for (int i = 1; i <= 4; i++)
            {
                if (key == i.ToString())
                {
                    isOkKey = true;
                    break;
                }
            }
            if (!isOkKey)
            {
                MainForm.KeyIncorrect();
                Show();
            }
            else
            {
                GameForm gameForm = new GameForm(PrinteablesToPlay, CreationOfPlayers(int.Parse(key)));
                gameForm.Show();
            }
        }
        public static List<Player> CreationOfPlayers(int count)
        {
            List<Player> players = new();
            for (int i = 0; i < count; i++)
                players.Add(new Player("Player " + (i + 1).ToString(), new List<Player>()));
            return players;
        }
    }
}