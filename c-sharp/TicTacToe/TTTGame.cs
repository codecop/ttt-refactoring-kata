using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class Position
    {
        public static readonly Position A1 = new Position(0, 0);
        public static readonly Position A2 = new Position(0, 1);
        public static readonly Position A3 = new Position(0, 2);
        public static readonly Position B1 = new Position(1, 0);
        public static readonly Position B2 = new Position(1, 1);
        public static readonly Position B3 = new Position(1, 2);
        public static readonly Position C1 = new Position(2, 0);
        public static readonly Position C2 = new Position(2, 1);
        public static readonly Position C3 = new Position(2, 2);

        public int x { get; private set; }
        public int y { get; private set; }

        private Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (Position)obj;
            return this.x == other.x && this.y == other.y;
        }

        public override int GetHashCode()
        {
            return x ^ y;
        }
    }

    public class Winner
    {
        public static readonly Winner UNDECIDED = new Winner(0);
        public static readonly Winner CIRCLE = new Winner(1);
        public static readonly Winner CROSS = new Winner(2);
        public static readonly Winner DRAW = new Winner(3);

        public int code { get; private set; }

        public Winner(int code)
        {
            this.code = code;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (Winner)obj;
            return this.code == other.code;
        }

        public override int GetHashCode()
        {
            return code;
        }
    }

    public class TTTGame
    {
        private int lstMv;

        private int[,] cells = new int[3, 3];

        public bool IsOver()
        {
            return Winner() != TicTacToe.Winner.UNDECIDED;
        }

        public Winner Winner()
        {
            var results = new List<Winner>();
            AddWinningCombinationsTo(results);
            AddUndecidedTo(results);
            AddDrawTo(results);
            return results.First();
        }

        private void AddWinningCombinationsTo(List<Winner> results)
        {
            int i = 0;
            AddCombination(results, cells[0, i], cells[1, i], cells[2, i]);
            AddCombination(results, cells[i, 0], cells[i, 1], cells[i, 2]);

            i++;
            AddCombination(results, cells[0, i], cells[1, i], cells[2, i]);
            AddCombination(results, cells[i, 0], cells[i, 1], cells[i, 2]);

            i++;
            AddCombination(results, cells[0, i], cells[1, i], cells[2, i]);
            AddCombination(results, cells[i, 0], cells[i, 1], cells[i, 2]);

            AddCombination(results, cells[0, 0], cells[1, 1], cells[2, 2]);
            AddCombination(results, cells[2, 0], cells[1, 1], cells[0, 2]);
        }

        private void AddCombination(List<Winner> results, int cellA, int cellB, int cellC)
        {
            if (cellA == cellB && cellA == cellC && cellA != 0)
            {
                results.Add(new Winner(cellA));
            }
        }

        private void AddUndecidedTo(List<Winner> results)
        {
            if (IsEmpty())
            {
                results.Add(TicTacToe.Winner.UNDECIDED);
            }
        }

        private void AddDrawTo(List<Winner> results)
        {
            if (!IsEmpty())
            {
                results.Add(TicTacToe.Winner.DRAW);
            }
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

        public void placeCircle(Position p)
        {
            ValidatePlaceCircle(p);
            cells[p.x, p.y] = 1;
            lstMv = 1;
        }

        private void ValidatePlaceCircle(Position p)
        {
            if (IsOver() || (lstMv == 1) || (cells[p.x, p.y] != 0))
            {
                throw new TicTacException();
            }
        }

        public void placeCross(Position p)
        {
            ValidatePlaceCross(p);
            cells[p.x, p.y] = 2;
            lstMv = 2;
        }

        private void ValidatePlaceCross(Position p)
        {
            if (lstMv == 2 || cells[p.x, p.y] != 0)
            {
                throw new TicTacException();
            }
        }
    }
}
