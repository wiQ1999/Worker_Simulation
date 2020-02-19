using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectWorker
{
    class Forest : Board
    {
        #region Properties

        /// <summary>
        /// Trees frequency on 10 x 10 units square
        /// </summary>
        public float Frequency { get; set; }
        /// <summary>
        /// Array that stores place where trees exist on board
        /// </summary>
        public int[,,] TreesArray { get; set; }

        #endregion

        #region Ctor

        public Forest() { }

        public Forest(int x, int y, float a_Frequency) : base (x, y)
        {
            this.Frequency = a_Frequency;
            this.TreesArray = new int[x, y, 1];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method responsible for calculate about trees on board and next draw them
        /// </summary>
        public void Trees()
        {
            //Obliczanie częstotliwości wystepowania drzew na danej powierzchni oraz usupłenianie ich w tablicy drzew
            FreqPerSquare(this.SizeX, this.SizeY);

            //Rysowanie tablicy drzew
            DrawTrees();
        }

        /// <summary>
        /// Method that calculate tree frequency on specifidy square
        /// </summary>
        /// <param name="a_iLengthX">Side X length square</param>
        /// <param name="a_iLengthY">Side Y length square</param>
        /// <returns>Float value of frequency per square</returns>
        private float FreqCount(int a_iLengthX, int a_iLengthY)
        {
            //Deklaracja zmiennych
            float _fFrequency = 0;

            //Obliczanie częstotliwości z podanymi długościmi boków pola
            _fFrequency = this.Frequency * a_iLengthX * a_iLengthY / 100f;

            //Zwracanie wartości częstotliwosści
            return _fFrequency;
        }

        /// <summary>
        /// Method that divide the board into Squares and inserts them with trees by specific frequency
        /// </summary>
        /// <param name="a_iSizeX">Board size X</param>
        /// <param name="a_iSizeY">Board size Y</param>
        private void FreqPerSquare(int a_iSizeX, int a_iSizeY)
        {
            //Deklaracja zmiennych
            int _iLengthX = 0;
            int _iLengthY = 0;
            int _iPlaceX = 0;
            int _iPlaceY = 0;

            
            while(a_iSizeY != 0)//Pętla dopóki nie dojdzie do końca planszy Y
            {
                //Sprawdzanie pozostałej długości Y
                if (a_iSizeY > 10)
                    _iLengthY = 10;
                else
                    _iLengthY = a_iSizeY;

                while(a_iSizeX != 0)//Pętla dopóki nie dojdzie do końca planszy X
                {
                    //Sprawdzanie pozostałej długości X
                    if (a_iSizeX > 10)
                        _iLengthX = 10;
                    else
                        _iLengthX = a_iSizeX;

                    if(a_iSizeX > 10)//Warunek jeżeli pozostało więcej niż 10 długości X
                    {
                        //Uzupęłnianie tablicy
                        InsertTrees(FreqCount(_iLengthX, _iLengthY), _iPlaceX, _iPlaceY, _iLengthX, _iLengthY);

                        //Przesuniecie miejsca X w odniesieniu do tablicy drzew
                        _iPlaceX += 10;
                    }
                    else//Jeżeli jest mniej długości X
                    {
                        //Uzupęłnianie tablicy
                        InsertTrees(FreqCount(_iLengthX, _iLengthY), _iPlaceX, _iPlaceY, _iLengthX, _iLengthY);

                        //Zresetowanie miejsca X do 0
                        _iPlaceX = 0;
                    }
                    //Zmniejszenie pozostałej wielkości X planszy
                    a_iSizeX -= _iLengthX;
                }
                //Zmniejszenie pozostałej wielkości Y planszy
                a_iSizeY -= _iLengthY;
                //Zresetowanie wielkości planszy X do standardowej wielkości
                a_iSizeX = this.SizeX;
                //Przesuniecie miejsca Y w odniesieniu do tablicy drzew
                _iPlaceY += _iLengthY;
            }

        }

        /// <summary>
        /// Method that inserts trees on specific square
        /// </summary>
        /// <param name="a_fFrequency">Frequency on square</param>
        /// <param name="a_iPlaceX">Place X on console</param>
        /// <param name="a_iPlaceY">Place Y on console</param>
        /// <param name="a_iLengthX">Square X length</param>
        /// <param name="a_iLengthY">Square Y length</param>
        private void InsertTrees(float a_fFrequency, int a_iPlaceX, int a_iPlaceY, int a_iLengthX, int a_iLengthY)
        {
            //Deklaracja zmiennych
            Random rnd = new Random();
            int _iTreesOnSquare = 0;
            int _iSquareX = a_iPlaceX + a_iLengthX;
            int _iSquareY = a_iPlaceY + a_iLengthY;

            //Sprawdzenie ile jest drzew w obszarze
            for (int x = a_iPlaceX; x < _iSquareX; x++)
            {
                for (int y = a_iPlaceY; y < _iSquareY; y++)
                {
                    if(this.TreesArray[x, y, 0] != 0)
                    {
                        _iTreesOnSquare++;
                    }
                }
            }

            //Umieszczanie drzew w różnych miejscach na danym obszarze w ilości podanej częstotliwości na obszar
            while(_iTreesOnSquare < Math.Ceiling(a_fFrequency))
            {
                if (a_fFrequency - _iTreesOnSquare < 1)//Gdy częstotliwość drzew ma mijesce po przecinku
                {
                    //Oblcizanie szansy (reszta po przecinku * 100)
                    int _iChance = (int)((a_fFrequency - Math.Floor(a_fFrequency)) * 100);

                    //Losowanie szansy
                    if(rnd.Next(1, 101) < _iChance)
                    {
                        _iTreesOnSquare++;
                        continue;
                    }
                }

                //Losowanie miejsca na planszy
                int _iRandomX = rnd.Next(a_iPlaceX, _iSquareX);
                int _iRandomY = rnd.Next(a_iPlaceY, _iSquareY);

                if (this.TreesArray[_iRandomX, _iRandomY, 0] != 0)//Sprawdzenie czy drzewo istnieje już w wylosowanym miejscu
                {
                    continue;
                }

                //Zapisanie wylosowanego pustego miejsca
                this.TreesArray[_iRandomX, _iRandomY, 0] = 1;

                //Zwiększenie liczby drzew na obszarze
                _iTreesOnSquare++;
            }
        }

        /// <summary>
        /// Checks each place in an array and writes it on the board
        /// </summary>
        public void DrawTrees()
        {
            for (int x = 0; x < this.SizeX; x++)
            {
                for (int y = 0; y < this.SizeY; y++)
                {
                    //Tylko elementy tablicy drzew które nie są puste
                    if (this.TreesArray[x, y, 0] != 0)
                    {
                        //Działanie na podstawie elementów tablicy (cyfr)
                        switch (this.TreesArray[x, y, 0])
                        {
                            case 1://Drzewo
                                Console.SetCursorPosition(x + 1, y + 1);
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.Write(" ");
                                Console.ResetColor();
                                break;
                        }
                    }
                }
            }
        }

        #endregion

    }
}
