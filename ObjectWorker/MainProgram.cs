using System;

namespace ObjectWorker
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            //Specyfikacja planszy
            int _iSizeX = 60;
            int _iSizeY = 15;
            float _fFrequency = 5f;

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



            Dijkstra dijkstra = new Dijkstra(_iSizeX, _iSizeY);

            dijkstra.FindPath(worker.PosX, worker.PosY, forest.TreesArray, 1);


            Console.ReadKey();
        }
    }
}
