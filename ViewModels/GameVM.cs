using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    internal class GameVM
    {
        public ObservableCollection<ObservableCollection<SquareVM>> Board { get; set; }
        public GameLogic Logic { get; set; }
        public MultipleJumpsVM multipleJumpsVM { get; set; }
        public TurnVM playerTurn { get; set; }
        public ButtonCommands commands { get; set; }

        public GameVM()
        {
            ObservableCollection<ObservableCollection<Square>> board = initBoard();
            MultipleJumps mj = new MultipleJumps(false);
            Turn turn = new Turn(PieceColor.Red);
            Logic = new GameLogic(board, mj, turn);
            multipleJumpsVM = new MultipleJumpsVM(mj);
            playerTurn = new TurnVM(turn);
            Board = CellBoardToCellVMBoard(board);
            commands = new ButtonCommands(Logic);
        }

        private ObservableCollection<ObservableCollection<Square>> initBoard()
        {
            ObservableCollection<ObservableCollection<Square>> board = new ObservableCollection<ObservableCollection<Square>>();

            for (int row = 0; row < 8; ++row)
            {
                board.Add(new ObservableCollection<Square>());
                for (int column = 0; column < 8; ++column)
                {
                    if ((row + column) % 2 == 0)
                    {
                        board[row].Add(new Square(row, column, SquareColor.White, null));
                    }
                    else if (row < 3)
                    {
                        board[row].Add(new Square(row, column, SquareColor.Black, new Piece(PieceColor.Black)));
                    }
                    else if (row > 4)
                    {
                        board[row].Add(new Square(row, column, SquareColor.Black, new Piece(PieceColor.Red)));
                    }
                    else
                    {
                        board[row].Add(new Square(row, column, SquareColor.Black, null));
                    }
                }
            }

            return board;
        }
        private ObservableCollection<ObservableCollection<SquareVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Square>> board)
        {
            ObservableCollection<ObservableCollection<SquareVM>> result = new ObservableCollection<ObservableCollection<SquareVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<SquareVM> line = new ObservableCollection<SquareVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Square s = board[i][j];
                    SquareVM cellVM = new SquareVM(s, Logic);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
    }
}
