using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectWorker
{
    struct Data
    {
        public int X;
        public int Y;
        public uint Neighbor;
        public uint Distance;
    }

    class Dijkstra
    {
        #region Properties

        /// <summary>
        /// Array stores numbers represents elements on a main board
        /// </summary>
        private int[,] PathBoard { get; set; }
        /// <summary>
        /// List of points stores date on a PathBoard
        /// </summary>
        private Dictionary<uint, Data> Visited { get; set; }

        #endregion

        #region Ctor

        public Dijkstra(int a_iSizeX, int a_iSizeY)
        {
            //Tworzenie tablicy ścieżek
            this.PathBoard = new int[a_iSizeX + 2, a_iSizeY + 2];

            //Wypełnianie tablicy ścieżek
            for (int x = 0; x < a_iSizeX + 2; x++)
            {
                this.PathBoard[x, 0] = -1;
                this.PathBoard[x, a_iSizeY + 1] = -1;

                for (int y = 1; y < a_iSizeY + 1; y++)
                {
                    if(x == 0 || x == a_iSizeX + 1)
                    {
                        this.PathBoard[x, y] = -1;
                    }
                    else
                    {
                        this.PathBoard[x, y] = int.MaxValue;
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void PointAnalysis(int a_iX, int a_iY, uint a_uClosed, uint a_uPoint)
        {
            //Sprawdza czy miejsce w tablicy nie jest przeszkodą do ominiecią oraz czy nie jest to wartość już sprawdzona
            if(this.PathBoard[a_iX, a_iY] >= 0 && this.PathBoard[a_iX, a_iY] > a_uPoint)
            {
                //Tworzenie nowej przestrzeni danyhc punktu
                Data point = new Data
                {
                    X = a_iX,
                    Y = a_iY,
                    Neighbor = a_uClosed,
                    Distance = 1//!!DO MODYFIKACJI (METHOD)
                };

                //Dodanie punmtu do listy
                Visited.Add(a_uPoint, point);
            }
        }

        public void FindPath(int a_iStartX, int a_iStartY, int[,,] a_oObstacles, int a_iSearch)
        {
            //deklaracja zmiennych
            a_iStartX += 1;//Przestawienie względem większej tablicy PathBoard
            a_iStartY += 1;
            uint _uClosed = 0;//Punkty zamknięte
            uint _uPoint = 1;
            int _iX;
            int _iY;

            //Zapisanie miejsca startowego
            this.PathBoard[a_iStartX, a_iStartY] = 0;

            //Główna pętla
            while (true)
            {
                //Ustalenie miejsca punktu, który musi być przeszukiwany jako nastepny
                if (_uClosed == 0)//Startowo punktem początkowym jest punkt startowy poszukiwań
                {
                    _iX = a_iStartX;
                    _iY = a_iStartY;
                }
                else//Sprawdzanie poprzedniego punktu zamkniętego i jego położenia
                {
                    _iX = Visited[_uClosed].X;
                    _iY = Visited[_uClosed].Y;
                }

                //Znalezienie szukanego elementu
                if (a_oObstacles[_iX - 1, _iY - 1, 0] == a_iSearch)
                {
                    break;
                }

                //Przeszukanie sąsiednich miejsc punktu w 4 strony
                for (int i = 0; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0://Góra
                            PointAnalysis(_iX, _iY - 1, _uClosed, _uPoint);
                            break;
                        case 1://Dół
                            PointAnalysis(_iX, _iY + 1, _uClosed, _uPoint);
                            break;
                        case 2://Prawo
                            PointAnalysis(_iX + 1, _iY, _uClosed, _uPoint);
                            break;
                        case 3://Lewo
                            PointAnalysis(_iX - 1, _iY, _uClosed, _uPoint);
                            break;
                    }
                }

                //Zamknięcie przeszukanego już punktu
                _uClosed++;
            }


        }

        #endregion
    }
}
