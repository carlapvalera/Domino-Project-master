using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;

namespace Visual_Interface
{
    class GameForm : IForm
    {
        public Game Game_ { get; }
        IStrategy[] Strategies { get; }
        bool PlayToTheEnd { get; set; } = false;

        public GameForm(List<IPrinteable> printeables, List<Player> players)
        {
            Game_ = new Game((IDistribution)printeables[0], (IEndGame)printeables[1], (IEndRound)printeables[2],
                            (IScoreCalculator)printeables[3], (IFirstPlayer)printeables[4],
                            (IStep)printeables[5], (IActionModeratorToAdd)printeables[6],
                            (IActionModeratorToSub)printeables[7], (ITokensGenerator)printeables[8], players);
            Strategies = new IStrategy[] {new IntelligentStrategy(),
                                          new IntelligentBotagordaStrategy(),
                                          new IntelligentRandomStrategy(),
                                          new BotagordaStrategy(),
                                          new RandomStrategy()};
           
        }
        public void Show()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("What strategy do you want to play now?");
            Console.ResetColor();
            for (int i = 0; i < Strategies.Length; i++)
                Console.WriteLine(i + 1 + " - " + Strategies[i].Print());

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Other Options:");
            Console.ResetColor();
            Console.WriteLine(Strategies.Length + 1 + " - Play Random To The End");
            Console.WriteLine(Strategies.Length + 2 + " - Log");
            Console.WriteLine(Strategies.Length + 3 + " - Back");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Game_.Table_);
            Console.ResetColor();
            Console.WriteLine();

            if (Game_.Log.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Game_.Log[Game_.Log.Count - 1]);
                Console.ResetColor();
            }

            for (int i = 0; i < Game_.Players.Count; i++)
            {
                Console.Write(Game_.Players[i].Name + " Score: " +Game_.Players[i].ScoreGame+" ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int j = 0; j < Game_.Players[i].Hand.Count; j++)
                {
                    Console.Write(Game_.Players[i].Hand[j]);
                }
                Console.ResetColor();
                Console.WriteLine();
            }

            bool strategyIsSelected = false;
            IStrategy strategy = new RandomStrategy();
            string key = (Strategies.Length + 1).ToString();
            if (!PlayToTheEnd)
                key = Console.ReadLine();
            else
                Thread.Sleep(1000);
            for (int i = 0; i < Strategies.Length; i++)
            {
                if ((i + 1).ToString() == key)
                {
                    strategyIsSelected = true;
                    strategy = Strategies[i];
                    break;
                }
            }
            if (strategyIsSelected)
                Auxiliar_(strategy);
            else if ((Strategies.Length + 1).ToString() == key)
            {
                PlayToTheEnd = true;
                Auxiliar_(strategy);
            }
            else if ((Strategies.Length + 2).ToString() == key)
            {
                LogForm logForm = new LogForm(this);
                logForm.Show();
            }
            else if ((Strategies.Length + 3).ToString() == key)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else
            {
                MainForm.KeyIncorrect();
                Show();
            }
        }
        public void Auxiliar_(IStrategy strategy)
        {
            bool arePlaying = Game_.Play(strategy);
            if (arePlaying)
                Show();
            else
            {
                LogForm logForm = new LogForm(this);
                logForm.Show();
            }
        }
    }
}