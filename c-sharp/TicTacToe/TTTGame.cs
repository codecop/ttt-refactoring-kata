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

    public class Result
    {

        public static readonly Result UNDECIDED = new Result(0);
        public static readonly Result WIN_CIRCLE = new Result(1);
        public static readonly Result WIN_CROSS = new Result(2);
        public static readonly Result DRAW = new Result(3);

        public int code { get; private set; }

        public Result(int code)
        {
            this.code = code;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (Result)obj;
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

        public bool isOver()
        {
            return Winner() != Result.UNDECIDED;
        }

        private Result Winner()
        {
            var results = WinningCombinations();
            AppendEmpty(results);
            AppendDraw(results);
            return results.ElementAt(0);
        }

        private List<Result> WinningCombinations()
        {
            Result[] results = new Result[8];
            int j = 0;

            int i = 0;
            results[j] = XX(cells[0, i], cells[1, i], cells[2, i]);
            j++;
            results[j] = XX(cells[i, 0], cells[i, 1], cells[i, 2]);
            j++;
            i++;

            results[j] = XX(cells[0, i], cells[1, i], cells[2, i]);
            j++;
            results[j] = XX(cells[i, 0], cells[i, 1], cells[i, 2]);
            j++;
            i++;

            results[j] = XX(cells[0, i], cells[1, i], cells[2, i]);
            j++;
            results[j] = XX(cells[i, 0], cells[i, 1], cells[i, 2]);
            j++;
            i++;

            results[j] = XX(cells[0, 0], cells[1, 1], cells[2, 2]);
            j++;
            results[j] = XX(cells[2, 0], cells[1, 1], cells[0, 2]);
            j++;
            return results.Where(x => x != Result.UNDECIDED).Take(1).ToList();
        }

        private Result XX(int a, int b, int c)
        {
            if (a == b && a == c && a != 0)
            {
                return new Result(a);
            }
            return Result.UNDECIDED;
        }

        private void AppendEmpty(List<Result> results)
        {
            if (IsEmpty())
            {
                results.Add(Result.UNDECIDED);
            }
        }

        private void AppendDraw(List<Result> results)
        {
            if (!IsEmpty())
            {
                results.Add(Result.DRAW);
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
            if (isOver() || (lstMv == 1) || (cells[p.x, p.y] != 0))
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

        public int winner()
        {
            return Winner().code;
        }
    }
}
