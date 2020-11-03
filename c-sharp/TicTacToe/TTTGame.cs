using System.Linq;

namespace TicTacToe
{
    public class TTTGame
    {
        private int lstMv;

        private int[,] cells = new int[3, 3];

        public bool isOver()
        {
            return Winner() != 0;
        }

        // 0 undecided
        // 1 circle
        // 2 cross
        // 3 draw
        private int Winner()
        {
            int[] results = new int[8];
            int j = 0;

            for (int i = 0; i < 3; i++)
            {
                results[j] = XX(cells[0, i], cells[1, i], cells[2, i]);
                j++;
                results[j] = XX(cells[i, 0], cells[i, 1], cells[i, 2]);
                j++;
            }

            results[j] = XX(cells[0, 0], cells[1, 1], cells[2, 2]);
            j++;
            results[j] = XX(cells[2, 0], cells[1, 1], cells[0, 2]);
            j++;

            int z = results.FirstOrDefault(i => i != 0);
            if (z != 0)
                return z;

            bool bEmpty = IsEmpty();
            return bEmpty ? 0 : 3;
        }

        private int XX(int a, int b, int c)
        {
            if (a == b && a == c && a != 0)
            {
                return a;
            }
            return 0;
        }

        private bool IsEmpty()
        {
            bool bEmpty = false;
            for (int i = 0; i < 3; i++)
            {
                bEmpty = bEmpty || IsEmptyColumn(i);
            }
            return bEmpty;
        }

        private bool IsEmptyColumn(int i)
        {
            bool bEmpty = false;
            for (int j = 0; j < 3; j++)
            {
                bEmpty = bEmpty || IsEmptyCell(i, j);
            }
            return bEmpty;
        }

        private bool IsEmptyCell(int i, int j)
        {
            return cells[j, i] == 0;
        }

        public void makeMove(int i, int i1)
        {
            ValidateMakeMove(i, i1);
            cells[i, i1] = 1;
        }

        private void ValidateMakeMove(int i, int i1)
        {
            if (cells[i, i1] == 1)
            {
                throw new TicTacException();
            }
        }

        public void placeCircle(int i, int i1)
        {
            ValidatePlaceCircle(i, i1);
            cells[i, i1] = 1;
            lstMv = 1;
        }

        private void ValidatePlaceCircle(int i, int i1)
        {
            if (isOver() || (lstMv == 1) || (cells[i, i1] != 0))
            {
                throw new TicTacException();
            }
        }

        public void placeCross(int i, int i1)
        {
            ValidatePlaceCross(i, i1);
            cells[i, i1] = 2;
            lstMv = 2;
        }

        private void ValidatePlaceCross(int i, int i1)
        {
            if (lstMv == 2 || cells[i, i1] != 0)
            {
                throw new TicTacException();
            }
        }

        public int winner()
        {
            return Winner();
        }
    }
}
