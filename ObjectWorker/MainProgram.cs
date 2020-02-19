using System;

namespace ObjectWorker
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;


            int _iSizeX = 24;
            int _iSizeY = 6;
            float _fFrequency = 0f;


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
