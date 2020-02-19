using System;
using System.Collections.Generic;
using System.Threading;

namespace ObjectWorker
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            //Specyfikacja planszy
            int _iSizeX = 10;
            int _iSizeY = 10;
            float _fFrequency = 1f;

            #region Consol settings

            //Kursor
            Console.CursorVisible = false;

            //ustawienia okna konsoli
            Console.WindowWidth = _iSizeX + 2;
            Console.WindowHeight = _iSizeY + 3;

            //Ustawienia buffora
            Console.BufferWidth = _iSizeX + 2;
            Console.BufferHeight = _iSizeY + 3;

            #endregion

            Forest forest = new Forest(_iSizeX, _iSizeY, _fFrequency);

            forest.DrawBoard();

            forest.Trees();


            Worker worker = new Worker(_iSizeX, _iSizeY, forest.TreesArray);

            worker.DrowWorkers();

            //Ilość drzew do ścięcia
            uint _uTrees = 5;

            //Za każdym razem zmniejsza ilość drzew o 1
            while (_uTrees-- > 0)
            {
                Stack<Point> Path = worker.TreeCutting(forest.TreesArray, 1);

                //W przypadku gdy nie znaleziono obiektu na planszy oraz ścieżki do niego~~!!
                if (Path == null)
                {
                    break;
                }
                Console.SetCursorPosition(Path.Peek().X, Path.Peek().Y);
                Console.Write("X");
                //forest.TreesArray[Path.Peek().X, Path.Peek().Y, 0] = 0;

                //forest.DrawTrees();
            }

            Console.ReadKey();
        }
    }
}
