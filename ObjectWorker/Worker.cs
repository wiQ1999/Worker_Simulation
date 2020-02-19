using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectWorker
{
    class Worker
    {
        #region Properties

        public string[,,] WorkersArray { get; set; }
        public char Name { get; set; }
        public int Power { get; set; }
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

        public void DrowWorkers()
        {
            for(int x = 0; x < this.SizeX; x++)
            {
                for (int y = 0; y < this.SizeY; y++)
                {
                    if(this.WorkersArray[x, y, 0] != string.Empty)
                    {
                        Console.SetCursorPosition(x + 1, y + 1);
                        Console.Write(this.WorkersArray[x, y, 0]);
                    }
                }
            }
        }

        #endregion
    }
}
