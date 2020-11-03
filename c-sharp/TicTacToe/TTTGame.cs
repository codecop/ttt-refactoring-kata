using System;
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

        public int x { get; private set; } // TODO encapsulate
        public int y { get; private set; } // TODO encapsulate

        public Position(int x, int y) // TODO encapsulate
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

        private int code { get; set; }

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

    class Player
    {
        public static readonly Player CIRCLE = new Player(1);
        public static readonly Player CROSS = new Player(2);

        public int code { get; private set; } // TODO encapsulate

        private Player(int code)
        {
            this.code = code;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (Player)obj;
            return this.code == other.code;
        }

        public override int GetHashCode()
        {
            return code;
        }

    }

    class Cells
    {
        private IDictionary<Position, Player> cells = new Dictionary<Position, Player>();

        public Player Get(int x, int y)
        {
            return Get(new Position(x, y));
        }

        public Player Get(Position position)
        {
            if (cells.ContainsKey(position))
            {
                return cells[position];
            }
            return null;
        }

        public void Set(Position position, Player player)
        {
            ValidateIsEmpty(position);
            cells[position] = player;
        }

        private void ValidateIsEmpty(Position position)
        {
            if (!IsEmpty(position))
            {
                throw new TicTacException();
            }
        }

        public bool IsEmpty(Position position)
        {
            return !cells.ContainsKey(position);
        }
    }

    public class TTTGame
    {
        private Player lastPlayer;

        private Cells cells  = new Cells();

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
            AddCombination(results, cells.Get(0, i), cells.Get(1, i), cells.Get(2, i));
            AddCombination(results, cells.Get(i, 0), cells.Get(i, 1), cells.Get(i, 2));

            i++;
            AddCombination(results, cells.Get(0, i), cells.Get(1, i), cells.Get(2, i));
            AddCombination(results, cells.Get(i, 0), cells.Get(i, 1), cells.Get(i, 2));

            i++;
            AddCombination(results, cells.Get(0, i), cells.Get(1, i), cells.Get(2, i));
            AddCombination(results, cells.Get(i, 0), cells.Get(i, 1), cells.Get(i, 2));

            AddCombination(results, cells.Get(0, 0), cells.Get(1, 1), cells.Get(2, 2));
            AddCombination(results, cells.Get(2, 0), cells.Get(1, 1), cells.Get(0, 2));
        }

        private void AddCombination(List<Winner> results, Player cellA, Player cellB, Player cellC)
        {
            if (cellA == cellB && cellA == cellC && cellA != null)
            {
                results.Add(new Winner(cellA.code));
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
            return cells.IsEmpty(new Position(i, j));
        }

        public void placeCircle(Position p)
        {
            Player current = Player.CIRCLE;
            ValidateTurn(current);
            cells.Set(p, current);
            lastPlayer = current;
        }

        public void placeCross(Position p)
        {
            Player current = Player.CROSS;
            ValidateTurn(current);
            cells.Set(p, current);
            lastPlayer = current;
        }

        private void ValidateTurn(Player current)
        {
            if (IsOver() || lastPlayer == current)
            {
                throw new TicTacException();
            }
        }
    }
}
