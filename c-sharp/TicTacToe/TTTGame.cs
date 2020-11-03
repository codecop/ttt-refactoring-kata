using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class Position
    {
        public static readonly IList<Position> ALL = new List<Position>();

        public static readonly Position A1 = new Position(0, 0);
        public static readonly Position A2 = new Position(0, 1);
        public static readonly Position A3 = new Position(0, 2);
        public static readonly Position B1 = new Position(1, 0);
        public static readonly Position B2 = new Position(1, 1);
        public static readonly Position B3 = new Position(1, 2);
        public static readonly Position C1 = new Position(2, 0);
        public static readonly Position C2 = new Position(2, 1);
        public static readonly Position C3 = new Position(2, 2);

        private readonly int x;
        private readonly int y;

        private Position(int x, int y)
        {
            this.x = x;
            this.y = y;
            ALL.Add(this);
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

        private readonly int code;

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
        public static readonly Player CIRCLE = new Player(Winner.CIRCLE);
        public static readonly Player CROSS = new Player(Winner.CROSS);

        public readonly Winner asWinner;

        private Player(Winner asWinner)
        {
            this.asWinner = asWinner;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (Player)obj;
            return this.asWinner.Equals(other.asWinner);
        }

        public override int GetHashCode()
        {
            return asWinner.GetHashCode();
        }

    }

    class Grid
    {
        private readonly IDictionary<Position, Player> cells = new Dictionary<Position, Player>();

        public Player CellAt(Position position)
        {
            if (cells.ContainsKey(position))
            {
                return cells[position];
            }
            return null;
        }

        public void SetCell(Position position, Player player)
        {
            ValidateIsEmpty(position);
            cells[position] = player;
        }

        private void ValidateIsEmpty(Position position)
        {
            if (!IsCellEmpty(position))
            {
                throw new TicTacException();
            }
        }

        private bool IsCellEmpty(Position position)
        {
            return !cells.ContainsKey(position);
        }

        public bool HasAnyEmptyCells()
        {
            return Position.ALL. //
                Where(position => IsCellEmpty(position)). //
                Count() > 0;
        }

    }

    public class TTTGame
    {
        private Player lastPlayer;
        private Grid grid = new Grid();

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
            AddCombination(results, Position.A1, Position.A2, Position.A3);
            AddCombination(results, Position.B1, Position.B2, Position.B3);
            AddCombination(results, Position.C1, Position.C2, Position.C3);

            AddCombination(results, Position.A1, Position.B1, Position.C1);
            AddCombination(results, Position.A2, Position.B2, Position.C2);
            AddCombination(results, Position.A3, Position.B3, Position.C3);

            AddCombination(results, Position.A1, Position.B2, Position.C3);
            AddCombination(results, Position.A3, Position.B2, Position.C1);
        }

        private void AddCombination(List<Winner> results, Position posA, Position posB, Position posC)
        {
            Player cellA = grid.CellAt(posA);
            Player cellB = grid.CellAt(posB);
            Player cellC = grid.CellAt(posC);
            if (cellA == cellB && cellA == cellC && cellA != null)
            {
                results.Add(cellA.asWinner);
            }
        }

        private void AddUndecidedTo(List<Winner> results)
        {
            if (grid.HasAnyEmptyCells())
            {
                results.Add(TicTacToe.Winner.UNDECIDED);
            }
        }

        private void AddDrawTo(List<Winner> results)
        {
            if (!grid.HasAnyEmptyCells())
            {
                results.Add(TicTacToe.Winner.DRAW);
            }
        }

        public void placeCircle(Position position)
        {
            Place(position, Player.CIRCLE);
        }

        public void placeCross(Position position)
        {
            Place(position, Player.CROSS);
        }

        private void Place(Position position, Player current)
        {
            ValidateTurn(current);
            grid.SetCell(position, current);
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
