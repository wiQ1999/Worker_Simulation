using System;
using System.Collections.Generic;
using System.Threading;

namespace ObjectWorker
{
    class Worker
    {
        #region Properties

        /// <summary>
        /// A char specifying a worker
        /// </summary>
        private char Name { get; set; }
        /// <summary>
        /// Power value reduce time of his job. It tells how strong is the worker
        /// </summary>
        private int Power { get; set; }
        /// <summary>
        /// Speed value reduce time of his job and movement. It tells how fast is the worker
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// Worker X position on a main board
        /// </summary>
        private int PosX { get; set; }
        /// <summary>
        /// Worker Y position on a main board
        /// </summary>
        private int PosY { get; set; }
        /// <summary>
        /// Size X of main board
        /// </summary>
        private int SizeX { get; set; }
        /// <summary>
        /// Size Y of main board
        /// </summary>
        private int SizeY { get; set; }

        #endregion

        #region Ctor

        public Worker(int x, int y, int[,,] a_oTreesArray)
        {
            //Deklaracja zmiennych
            this.Name = 'W';
            this.Power = 30;
            this.Speed = 50;
            this.SizeX = x;
            this.SizeY = y;

            //Obiekt randomowy
            Random rnd = new Random();
            
            while (true)//Do momentu aż zostanie wylosowane puste miejsce dla pracownika
            {
                //Losowanie miejsc w tablicy
                int _iRandomX = rnd.Next(0, x);
                int _iRandomY = rnd.Next(0, y);

                if (a_oTreesArray[_iRandomX, _iRandomY, 0] != 0)//Jeżeli miejsce jest zajęte
                {
                    continue;
                }

                //Przypisanie pozycji pracownika
                this.PosX = _iRandomX;
                this.PosY = _iRandomY;

                //Przerwanie pętli
                break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Worker interaction with object
        /// </summary>
        public void WorkerInteraction()
        {
            //Czas przeznaczony na interakcje z obiektem
            Thread.Sleep(50000 / (this.Speed + this.Power));
        }

        /// <summary>
        /// Worker movement
        /// </summary>
        /// <param name="a_Path">List of points to serching object</param>
        private void WorkerMove(ref Stack<Point> a_Path)
        {
            //Zczytanie aktualnego czasu
            DateTime Date = DateTime.Now;

            //Deklaracja zmiennej o atrybutach punktu oraz usunięcie go z listy
            var _vPoint = a_Path.Pop();

            //Ustawienie aktualnego miejsca Worker'a
            Console.SetCursorPosition(this.PosX + 1, this.PosY + 1);

            //Usunięcie Worker'a ze aktualnego miejsca
            Console.Write(" ");

            //Przesunięcie Worker'a na nastepne pole podane w liście Path
            Console.SetCursorPosition(_vPoint.X, _vPoint.Y);

            //Umieszczenie Worker'a na nową pozycję na planszy
            Console.Write(this.Name);

            //Aktualizacja globalnej zmiennej pozycji Worker'a
            this.PosX = _vPoint.X - 1;
            this.PosY = _vPoint.Y - 1;

            //Wartość tylko do odczytu czasu który minął na wykonanie operacji
            TimeSpan Now = DateTime.Now - Date;

            //Sprawdzanie czy wykonano operacje szybciej niż wartość szybkości Worker'a
            if(Now.Milliseconds < 20000 / this.Speed)
            {
                //Oczekiwanie (szybkość Workera - czas poruszania się)
                Thread.Sleep((20000 / this.Speed) - Now.Milliseconds);
            }
        }

        /// <summary>
        /// Method that responds to given path
        /// </summary>
        /// <param name="a_oObjects">Array of searching items</param>
        /// <param name="a_iSerch">Serching item</param>
        /// <returns>Last point in path</returns>
        public Stack<Point> TreeCutting(int[,,] a_oObjects, int a_iSerch)
        {
            //Twrzenie obiektu pathfindingu
            Dijkstra dijkstra = new Dijkstra(this.SizeX, this.SizeY);

            //Twrzenie kolejki punktów
            Stack<Point> Path = dijkstra.FindPath(this.PosX, this.PosY, a_oObjects, a_iSerch);

            //Dopóki w liście nie zostanie ostatnia wartość - szukany element
            while (Path.Count > 1)
            {
                //Ruch Worker'a
                WorkerMove(ref Path);
            }

            //Zwracanie ostatniego miejsca w liście
            return Path;
        }

        /// <summary>
        /// Draws worker on a specified position
        /// </summary>
        public void DrowWorker()
        {
            Console.SetCursorPosition(this.PosX + 1, this.PosY + 1);
            Console.Write(this.Name);
        }

        #endregion
    }
}
