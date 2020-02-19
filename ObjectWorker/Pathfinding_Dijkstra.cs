﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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

            //Deklaracja listy
            this.Visited = new Dictionary<uint, Data>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method that search by given point and stores information in generic list
        /// </summary>
        /// <param name="a_iX">Point X position</param>
        /// <param name="a_iY">Point Y position</param>
        /// <param name="a_uClosed">Number of closed / actually point</param>
        /// <param name="a_uPoint">Number of next point</param>
        /// <param name="a_oObstacles">Array represents space of objects</param>
        /// <param name="a_iSearch">Searched object</param>
        /// <returns>Bool value tells about searching (true - still searching)</returns>
        private bool PointAnalysis(int a_iX, int a_iY, uint a_uClosed,ref uint a_uPoint, int[,,] a_oObstacles, int a_iSearch)
        {
            //Deklaracja mziennych
            bool _bIsFiding = true;

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
                this.Visited.Add(a_uPoint, point);

                //Zapisanie punktu na tablicy ściezki i przejście do nastepnego punktu (++)
                this.PathBoard[a_iX, a_iY] = (int)a_uPoint++;

                //Znalezienie szukanego elementu
                if (a_oObstacles[a_iX - 1, a_iY - 1, 0] == a_iSearch)
                {
                    _bIsFiding = false;
                }
            }

            //Zwraca wartość boolowską
            return _bIsFiding;
        }

        /// <summary>
        /// Method that starts searching path for Worker to given object
        /// </summary>
        /// <param name="a_iStartX">Worker X position</param>
        /// <param name="a_iStartY">Worker Y position</param>
        /// <param name="a_oObstacles">Array represents space of objects</param>
        /// <param name="a_iSearch">Searched object</param>
        public void FindPath(int a_iStartX, int a_iStartY, int[,,] a_oObstacles, int a_iSearch)
        {
            //Deklaracja zmiennych
            int _iArraySize = ((this.PathBoard.GetLength(0) - 2) * (this.PathBoard.GetLength(1) - 2)) - 1;//Określenie ilości maksymalnyhc punktów na planszy po której może poruszać się Worker
            a_iStartX += 1;//Przestawienie względem większej tablicy PathBoard
            a_iStartY += 1;
            uint _uClosed = 0;//Punkty zamknięte
            uint _uPoint = 1;
            int _iX;
            int _iY;
            bool _bIsFiding = true;//Sprawdza czy metoda jeszcze nie znalazła szukanego obiektu

            //Zapisanie miejsca startowego
            this.PathBoard[a_iStartX, a_iStartY] = 0;

            //Główna pętla
            //Dopóki nie znalazł elementu oraz dopóki rozmiar tablicy jest wiekszy lub równy odwiedzonym punktom
            while (_bIsFiding && _iArraySize >= _uPoint)
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

                

                //Przeszukanie sąsiednich miejsc punktu w 4 strony
                for (int i = 0; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0://Góra
                            _bIsFiding = PointAnalysis(_iX, _iY - 1, _uClosed, ref _uPoint, a_oObstacles, a_iSearch);
                            break;
                        case 1://Dół
                            _bIsFiding = PointAnalysis(_iX, _iY + 1, _uClosed, ref _uPoint, a_oObstacles, a_iSearch);
                            break;
                        case 2://Prawo
                            _bIsFiding = PointAnalysis(_iX + 1, _iY, _uClosed, ref _uPoint, a_oObstacles, a_iSearch);
                            break;
                        case 3://Lewo
                            _bIsFiding = PointAnalysis(_iX - 1, _iY, _uClosed, ref _uPoint, a_oObstacles, a_iSearch);
                            break;
                    }

                    //Wyjście ze sprawdzania jeżeli znaleziono szukany obiekt
                    if (!_bIsFiding)
                    {
                        break;
                    }
                }

                //Zamknięcie przeszukanego już punktu
                _uClosed++;
            }

            //!!Komunikat w przypadku gdy nei znaleziono szukanego obiektu na całej planszyXD
            if(!(_iArraySize >= _uPoint))
            {
                Console.WriteLine("Szukany obiekt nie istnieje!");
            }
        }

        #endregion
    }
}
