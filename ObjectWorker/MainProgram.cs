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
            float _fFrequency = 50f;

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

            //Console.ReadKey();

            //Ścinanie drzew w nieskończoność
            while (true)
            {
                Stack<Point> Path = worker.TreeCutting(forest.TreesArray, 1);

                //W przypadku gdy nie znaleziono obiektu na planszy oraz ścieżki do niego ~~ niedopracowane!!
                if (Path.Count == 0)
                {
                    break;
                }

                Console.SetCursorPosition(Path.Peek().X, Path.Peek().Y);
                Console.Write(" ");
                forest.TreesArray[Path.Peek().X - 1, Path.Peek().Y - 1, 0] = 0;

                //Zasymulowanie chwili przerwy po ścięciu drzewa
                Thread.Sleep(500);

                //Regeneracja drzew ~~ ToDo
            }

            Console.ReadKey();
        }
    }
}
