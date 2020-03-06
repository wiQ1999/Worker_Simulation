using System;
using System.Collections.Generic;
using System.Threading;

namespace ObjectWorker
{
    class MainProgram
    {
        static void Simulation(int a_iSizeX, int a_iSizeY, float a_fFrequency)
        {
            //Tworzenie nowego obiektu
            Forest forest = new Forest(a_iSizeX, a_iSizeY, a_fFrequency);

            //Rysowanie planszy
            forest.DrawBoard();
            //Generowanie drzew
            forest.Trees();

            //Tworzenie nowego obiektu
            Worker worker = new Worker(a_iSizeX, a_iSizeY, forest.TreesArray);

            //Umieszczenie pracownika na planszy
            worker.DrowWorker();

            //Ścinanie drzew
            while (true)
            {
                //Kolejka składająca punktów
                Stack<Point> Path = worker.TreeCutting(forest.TreesArray, 1);

                //W przypadku gdy nie znaleziono obiektu na planszy oraz ścieżki do niego
                if (Path.Count == 0)
                {
                    //Czekanie w przypadku gdy nie ma na planszy drzew
                    Thread.Sleep(2000);

                    //Powtórzenie szukania
                    continue;
                }

                //Czas interakcji z obiektem
                worker.WorkerInteraction();

                //Usuwanie ściętego drzewa z planszy oraz tablicy drzew
                Console.SetCursorPosition(Path.Peek().X, Path.Peek().Y);
                Console.Write(" ");
                forest.TreesArray[Path.Peek().X - 1, Path.Peek().Y - 1, 0] = 0;

                //Zasymulowanie chwili przerwy po ścięciu drzewa
                Thread.Sleep(10000 / worker.Speed);

                //Regeneracja drzew ~~ ToDo
            }
        }

        static void Main(string[] args)
        {
            //Specyfikacja planszy

            int _iSizeX = 60;            //Szerokość planszy
            int _iSizeY = 20;            //Wysokość planszy
            float _fFrequency = 3.4f;    //Częstotliwość wygenerowania drzew na planszy

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

            Simulation(_iSizeX, _iSizeY, _fFrequency);

            Console.ReadKey();
        }
    }
}
