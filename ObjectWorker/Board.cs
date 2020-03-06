using System;

namespace ObjectWorker
{
    class Board
    {
        #region Properties

        /// <summary>
        /// Height of board
        /// </summary>
        public int SizeX { get; set; }
        /// <summary>
        /// Width of board
        /// </summary>
        public int SizeY { get; set; }

        #endregion

        #region Ctor

        public Board() { }

        public Board(int x, int y)
        {
            this.SizeX = x;
            this.SizeY = y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method that draws board with specified height and width
        /// </summary>
        public void DrawBoard()
        {
            for (int y = 0; y < this.SizeY + 2; y++)
            {
                if(y == 0 || y == this.SizeY + 1)
                {
                    Console.Write("+");
                    for (int x = 0; x < this.SizeX; x++)
                    {
                        Console.Write("-");
                    }
                    Console.WriteLine("+");
                }
                else
                {
                    Console.Write("|");
                    for (int x = 0; x < this.SizeX; x++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine("|");
                }
            }
        }

        #endregion

    }
}
