using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project;

namespace Visual_Interface
{
    class ItemToSelectForm : IForm
    {
        int Cursor { get; }
        List<IPrinteable> PrinteablesToPlay { get; } = new List<IPrinteable>();
        (string, IPrinteable[])[] Printeables { get; }
        public ItemToSelectForm(List<IPrinteable> printablesToPlay, (string,IPrinteable[])[] printeables, int cursor)
        {
            Cursor = cursor;
            Printeables = printeables;
            PrinteablesToPlay = printablesToPlay;
        }
        public void Show()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Printeables[Cursor].Item1+":");
            Console.ResetColor();
            for (int i = 0; i < Printeables[Cursor].Item2.Length; i++)
                Console.WriteLine(i + 1 + " - " + Printeables[Cursor].Item2[i].Print());

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Other Options:");
            Console.ResetColor();
            Console.WriteLine(Printeables[Cursor].Item2.Length + 1 + " - Back To Main");
            if (Cursor != 0)
                Console.WriteLine(Printeables[Cursor].Item2.Length + 2 + " - Back");

            string key = Console.ReadLine();
            bool isOkKey = false;


            for (int i = 0; i < Printeables[Cursor].Item2.Length; i++)
            {
                if (key == (i + 1).ToString())
                {
                    isOkKey = true;
                    PrinteablesToPlay.Add(Printeables[Cursor].Item2[i]);
                    break;
                }
            }
            if (key == (Printeables[Cursor].Item2.Length + 1).ToString())
            {
                IForm mainForm = new MainForm();
                mainForm.Show();
            }
            if (Cursor != 0 && key == (Printeables[Cursor].Item2.Length + 2).ToString())
            {
                PrinteablesToPlay.RemoveAt(PrinteablesToPlay.Count - 1);
                ItemToSelectForm itemToSelectForm = new ItemToSelectForm(PrinteablesToPlay, Printeables, Cursor - 1);
                itemToSelectForm.Show();
            }
            else if (!isOkKey)
            {
                MainForm.KeyIncorrect();
                Show();
            }
            if (isOkKey && Cursor + 1 == Printeables.Length)
            {
                PlayerForm playerForm = new PlayerForm(PrinteablesToPlay);
                playerForm.Show();
            }
            else if(isOkKey)
            {
                ItemToSelectForm itemToSelectForm = new ItemToSelectForm(PrinteablesToPlay, Printeables, Cursor + 1);
                itemToSelectForm.Show();
            }
        }
    }
}