using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe;

namespace TicTacToe.Tests
{

    [TestClass]
    public class TicTacToeTests
    {
        [TestMethod]
        public void GIVEN_new_game_THEN_game_is_not_over()
        {
            var game = new TTTGame();
            Assert.IsFalse(game.isOver());
        }

        [TestMethod]
        public void GIVEN_new_game_THEN_can_place_a_circle()
        {
            var game = new TTTGame();
            game.placeCircle(Position.B2);
        }

        [TestMethod]
        public void GIVEN_new_game_THEN_can_place_a_cross()
        {
            var game = new TTTGame();
            game.placeCross(Position.B2);
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_place_a_cross_THEN_can_place_a_circle_in_a_different_cell()
        {
            var game = new TTTGame();
            game.placeCross(Position.B2);
            game.placeCircle(Position.B3);
        }

        [TestMethod]
        [ExpectedException(typeof(TicTacException))]
        public void GIVEN_new_game_WHEN_last_move_placed_circle_THEN_can_not_place_a_circle()
        {
            var game = new TTTGame();
            game.placeCircle(Position.B2);
            game.placeCircle(Position.B3);
        }

        [TestMethod]
        [ExpectedException(typeof(TicTacException))]
        public void GIVEN_new_game_WHEN_last_move_placed_cross_THEN_can_not_place_a_cross()
        {
            var game = new TTTGame();
            game.placeCross(Position.B2);
            game.placeCross(Position.B3);
        }

        [TestMethod]
        [ExpectedException(typeof(TicTacException))]
        public void GIVEN_new_game_WHEN_cell_occupied_THEN_can_not_make_move_in_that_cell()
        {
            var game = new TTTGame();
            game.placeCross(Position.B2);
            game.placeCircle(Position.B2);
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_three_crosses_in_top_row_THEN_cross_wins()
        {
            var game = new TTTGame();
            game.placeCross(new Position(0, 0));
            game.placeCircle(new Position(1, 0));
            game.placeCross(new Position(0, 1));
            game.placeCircle(Position.B2);
            game.placeCross(new Position(0, 2));

            Assert.AreEqual(2, game.winner());
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_three_circles_in_top_row_THEN_circle_wins()
        {
            var game = new TTTGame();
            game.placeCircle(new Position(0, 0));
            game.placeCross(new Position(1, 0));
            game.placeCircle(new Position(0, 1));
            game.placeCross(Position.B2);
            game.placeCircle(new Position(0, 2));

            Assert.AreEqual(1, game.winner());
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_three_crosses_in_bottom_row_THEN_cross_wins()
        {
            var game = new TTTGame();
            game.placeCross(new Position(2, 0));
            game.placeCircle(new Position(1, 0));
            game.placeCross(new Position(2, 1));
            game.placeCircle(Position.B2);
            game.placeCross(new Position(2, 2));
            Assert.AreEqual(2, game.winner());
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_three_crosses_in_left_column_THEN_cross_wins()
        {
            var game = new TTTGame();
            game.placeCross(new Position(0, 0));
            game.placeCircle(new Position(2, 1));
            game.placeCross(new Position(1, 0));
            game.placeCircle(new Position(2, 2));
            game.placeCross(new Position(2, 0));
            Assert.AreEqual(2, game.winner());
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_three_crosses_in_right_column_THEN_cross_wins()
        {
            var game = new TTTGame();
            game.placeCross(new Position(0, 2));
            game.placeCircle(new Position(2, 0));
            game.placeCross(Position.B3);
            game.placeCircle(new Position(2, 1));
            game.placeCross(new Position(2, 2));
            Assert.AreEqual(2, game.winner());
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_three_crosses_in_diagonal_from_top_left_THEN_cross_wins()
        {
            var game = new TTTGame();
            game.placeCross(new Position(0, 0));
            game.placeCircle(new Position(2, 0));
            game.placeCross(Position.B2);
            game.placeCircle(new Position(2, 1));
            game.placeCross(new Position(2, 2));
            Assert.AreEqual(2, game.winner());
        }

        [TestMethod]
        public void GIVEN_new_game_WHEN_three_crosses_in_diagonal_from_top_right_THEN_cross_wins()
        {
            var game = new TTTGame();
            game.placeCross(new Position(0, 2));
            game.placeCircle(new Position(1, 0));
            game.placeCross(Position.B2);
            game.placeCircle(new Position(2, 1));
            game.placeCross(new Position(2, 0));
            Assert.AreEqual(2, game.winner());
        }

        [TestMethod]
        public void GIVEN_game_with_all_cells_occupied_WHEN_no_three_in_a_line_THEN_game_is_a_draw()
        {
            var game = new TTTGame();
            game.placeCircle(new Position(2, 2));
            game.placeCross(new Position(0, 0));
            game.placeCircle(new Position(0, 1));
            game.placeCross(new Position(0, 2));
            game.placeCircle(new Position(1, 0));
            game.placeCross(Position.B2);
            game.placeCircle(Position.B3);
            game.placeCross(new Position(2, 1));
            game.placeCircle(new Position(2, 0));

            Assert.AreEqual(3, game.winner());
        }

        [TestMethod]
        [ExpectedException(typeof(TicTacException))]
        public void GIVEN_game_is_over_WHEN_place_a_move_THEN_exception_is_thrown()
        {
            var game = new TTTGame();
            game.placeCross(new Position(0, 0));
            game.placeCircle(new Position(1, 0));
            game.placeCross(new Position(0, 1));
            game.placeCircle(Position.B2);
            game.placeCross(new Position(0, 2));
            game.placeCircle(Position.B3);
        }

        [TestMethod]
        public void GIVEN_game_has_a_winner_cross_THEN_game_is_over()
        {
            var game = new TTTGame();

            game.placeCross(new Position(0, 0));
            game.placeCircle(new Position(1, 0));
            game.placeCross(new Position(0, 1));
            game.placeCircle(Position.B2);
            game.placeCross(new Position(0, 2));

            Assert.IsTrue(game.isOver());
        }

        [TestMethod]
        public void GIVEN_game_has_a_winner_circle_THEN_game_is_over()
        {
            var game = new TTTGame();

            game.placeCircle(new Position(0, 0));
            game.placeCross(new Position(1, 0));
            game.placeCircle(new Position(0, 1));
            game.placeCross(Position.B2);
            game.placeCircle(new Position(0, 2));

            Assert.IsTrue(game.isOver());
        }
    }
}
