using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ObjectWorker
{
    class Worker
    {
        #region Properties

        public string[,,] WorkersArray { get; set; }
        public char Name { get; set; }
        public int Power { get; set; }
        public int Speed { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        #endregion

        #region Ctor

        public Worker(int x, int y, int[,,] a_oTreesArray)
        {
            //Deklaracja zmiennych
            this.Name = 'W';
            this.Power = 20;
            this.Speed = 300;
            this.SizeX = x;
            this.SizeY = y;
            this.WorkersArray = new string[x, y, 1];

            //Obiekt randomowy
            Random rnd = new Random();
            
            while (true)//Do momentu aż zostanie wylosowane puste miejsce dla pracownika
            {
                //Losowanie miejsc w tablicy
                int _iRandomX = rnd.Next(0, x);
                int _iRandomY = rnd.Next(0, y);

                if (this.WorkersArray[_iRandomX, _iRandomY, 0] != string.Empty && a_oTreesArray[_iRandomX, _iRandomY, 0] != 0)//Jeżeli miejsce jest zajęte
                {
                    continue;
                }

                //Przypisanie pozycji pracownika
                this.PosX = _iRandomX;
                this.PosY = _iRandomY;

                //Przypisanie pracownika na tablicę
                this.WorkersArray[_iRandomX, _iRandomY, 0] = this.Name.ToString();

                //Przerwanie pętli
                break;
            }
        }

        #endregion

        #region Methods

        private void WorkerInteraction()
        {
            //Czas przeznaczony na interakcje z obiektem
            Thread.Sleep(this.Power * this.Speed);
        }

        private void WorkerMove(ref Stack<Point> a_Path)
        {
            //Zczytanie aktualnego czasu
            DateTime Date = DateTime.Now;

            //Deklaracja zmiennej o atrybutach punktu oraz usunięcie go z listy
            var _vPoint = a_Path.Pop();

            //Ustawienie aktualnego miejsca Worker'a
            Console.SetCursorPosition(this.PosX, this.PosY);

            //Usunięcie Worker'a ze aktualnego miejsca
            Console.Write(" ");

            //Przesunięcie Worker'a na nastepne pole podane w liście Path
            Console.SetCursorPosition(_vPoint.X, _vPoint.Y);

            //Umieszczenie Worker'a na nową pozycję na planszy
            Console.Write(this.Name);

            //Aktualizacja globalnej zmiennej pozycji Worker'a
            this.PosX = _vPoint.X;
            this.PosY = _vPoint.Y;

            //Wartość tylko do odczytu czasu który minął na wykonanie operacji
            TimeSpan Now = DateTime.Now - Date;

            //Sprawdzanie czy wykonano operacje szybciej niż wartość szybkości Worker'a
            if(Now.Milliseconds < this.Speed)
            {
                //Oczekiwanie (szybkość Workera - czas wykonania operacji)
                Thread.Sleep(this.Speed - Now.Milliseconds);
            }
        }

        public Stack<Point> TreeCutting(int[,,] a_oObjects, int a_iSerch)
        {

            Dijkstra dijkstra = new Dijkstra(this.SizeX, this.SizeY);

            Stack<Point> Path = dijkstra.FindPath(this.PosX, this.PosY, a_oObjects, a_iSerch);

            //Dopóki w liście nie zostanie ostatnia wartość - szukany element
            while (Path.Count > 0)
            {
                WorkerMove(ref Path);
            }

            //Inerakcja Worker'a z szukanym elementem
            WorkerInteraction();

            return Path;
        }

        public void DrowWorkers()
        {
            Console.SetCursorPosition(this.PosX + 1, this.PosY + 1);
            Console.Write(this.WorkersArray[this.PosX, this.PosY, 0]);
        }

        #endregion
    }
}
