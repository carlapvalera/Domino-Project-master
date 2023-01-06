using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;

namespace Visual_Interface
{
    interface IForm
    {
        void Show();
    }
    class MainForm : IForm
    {
        (string,IPrinteable[])[] Printeables;
        public MainForm()
        {
            Printeables = new (string,IPrinteable[])[]{
                    ("Distribution",new IDistribution[] { new ClassicDistributionTen(),new DoublesToTrashDistribution()}),
                    ("End Game",new IEndGame[] { new ClassicEndGame(), new ChickenEndGame(), new TwicePassesEndGame()}),
                    ("End Round",new IEndRound[] {new ClassicEndRound()}),
                    ("Score Calculator",new IScoreCalculator[]{new ScoreCalculatorA(),new ScoreCalculatorB()}),
                    ("First PLayer",new IFirstPlayer[]{new FirstPlayerA()}),
                    ("Steps",new IStep[]{ new ClassicStep(), new ChangeDirectionWithPassStep()}),
                    ("Action Moderator To Add",new IActionModeratorToAdd[]{new ClassicActionToAdd(),new DoubleWhiteActionToAdd()}),
                    ("Action Moderator To Sub",new IActionModeratorToSub[]{new ClassicActionToSub()}),
                    ("Variant",new ITokensGenerator[]{ new Generator_9_Variant(),new Generator_6_Variant()})
            };
            
        }
        public void Show()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" ||||     ||||||    ||||  ||||    |||    ||||  ||    ||||||");
            Console.WriteLine(" ||  ||   ||  ||    || |||| ||    |||    || || ||    ||  ||");
            Console.WriteLine(" ||||     ||||||    ||  ||  ||    |||    ||  ||||    ||||||");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("1 - Classic Game");
            Console.WriteLine("2 - Custom Game");

            string key = Console.ReadLine();

            List<IPrinteable> printeablesToPlay = new List<IPrinteable>();

            if (key == "1")
            {
                for (int i = 0; i < Printeables.Length; i++)
                    printeablesToPlay.Add(Printeables[i].Item2[0]);
                GameForm gameForm = new GameForm(printeablesToPlay, PlayerForm.CreationOfPlayers(4));
                gameForm.Show();
            }

            else if (key == "2")
            {
                ItemToSelectForm distributionForm = new ItemToSelectForm(printeablesToPlay, Printeables,0);
                distributionForm.Show();
            }
            else
            {
                KeyIncorrect();
                Show();
            }
        }
        public static void KeyIncorrect()
        {
            Console.Clear();
            Console.WriteLine("The key is incorrect");
            Console.WriteLine("Try Again");
            Thread.Sleep(2000);
        }
    }
}
