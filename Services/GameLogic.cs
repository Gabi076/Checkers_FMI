using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Checkers.Models;
using Checkers;
using Checkers.ViewModels;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;
using System.Text.Json.Serialization;

namespace Checkers.Services
{
    public class GameLogic: BaseNotification
    {
        private ObservableCollection<ObservableCollection<Square>> board;
        private MultipleJumps mj;
        private Turn turn;
        public GameLogic(ObservableCollection<ObservableCollection<Square>> board, MultipleJumps mj, Turn turn)
        {
            this.board = board;
            this.turn = turn;
            this.mj = mj;
        }

        private static Dictionary<Square, Square> Destinations = new Dictionary<Square, Square>();
        private bool TookPiece = false;
        private bool JumpPossible = false;
        private int takenRedPieces = 0, takenBlackPieces = 0;
        private Square CurrentSquare = null;

        private void NextPlayer()
        {
            if (turn.Player == PieceColor.Red)
                turn.Player = PieceColor.Black;
            else turn.Player = PieceColor.Red;
        }
        private void GetDestinations(Square square)
        {
            var possibleMoves = new List<Tuple<int, int>>();
            if (square.Pic.Type == PieceType.King)
            {
                possibleMoves.Add(new Tuple<int, int>(-1, -1));
                possibleMoves.Add(new Tuple<int, int>(-1, 1));
                possibleMoves.Add(new Tuple<int, int>(1, -1));
                possibleMoves.Add(new Tuple<int, int>(1, 1));
            }
            else if(square.Pic.Color == PieceColor.Red)
            {
                possibleMoves.Add(new Tuple<int, int>(-1, -1));
                possibleMoves.Add(new Tuple<int, int>(-1, 1));
            }
            else
            {
                possibleMoves.Add(new Tuple<int, int>(1, -1));
                possibleMoves.Add(new Tuple<int, int>(1, 1));
            }

            foreach (var move in possibleMoves)
                if (square.Row + move.Item1 >= 0 && square.Row + move.Item1 < 8 &&
                    square.Col + move.Item2 >= 0 && square.Col + move.Item2 <8)
                {
                    if (board[square.Row + move.Item1][square.Col + move.Item2].Pic == null)
                        {if (!TookPiece)
                            Destinations.Add(board[square.Row + move.Item1][square.Col + move.Item2], null);
                    }
                    else if ((square.Row + move.Item1 * 2 >= 0 && square.Row + move.Item1 * 2 < 8 &&
                    square.Col + move.Item2 * 2 >= 0 && square.Col + move.Item2 * 2 < 8) 
                    && board[square.Row + move.Item1][square.Col + move.Item2].Pic.Color!=square.Pic.Color
                    && board[square.Row + move.Item1 * 2][square.Col + move.Item2 * 2].Pic == null)
                    {
                        Destinations.Add(board[square.Row + move.Item1 * 2][square.Col + move.Item2 * 2], board[square.Row + move.Item1][square.Col + move.Item2]);
                        JumpPossible = true;
                    }
                }
        }
        private void ShowMoves(Square square)
        {
            if (CurrentSquare != null && CurrentSquare != square)
            {
                board[CurrentSquare.Row][CurrentSquare.Col].Texture = "/Checkers;component/Resources/BlackSquare.png";

                foreach (var neighbour in Destinations.Keys)
                {
                    neighbour.Hint = null;
                }
                Destinations.Clear();
                CurrentSquare = null;
            }

            if (CurrentSquare != square)
            {
                if(CurrentSquare != null)
                {
                    board[CurrentSquare.Row][CurrentSquare.Col].Texture = "/Checkers;component/Resources/BlackSquare.png";

                    foreach (var neighbour in Destinations.Keys)
                    {
                        neighbour.Hint = null;
                    }
                    Destinations.Clear();
                }
                GetDestinations(square);
                if(TookPiece && !mj.IsChecked)
                {
                    Destinations.Clear();
                    TookPiece = false;
                    NextPlayer();
                }
                else
                {
                    if(TookPiece && !JumpPossible)
                    {
                        TookPiece = false;
                        NextPlayer();
                    }
                    else
                    {
                        foreach (var neighbour in Destinations.Keys)
                        {
                            neighbour.Hint = "/Checkers;component/Resources/Hint.png";
                        }
                        CurrentSquare = square;
                        JumpPossible = false;
                    }
                }

            }
            else
            {
                foreach (var neighbour in Destinations.Keys)
                {
                    neighbour.Hint = null;
                }
                Destinations.Clear();
                CurrentSquare = null;
            }
        }
        public void ClickPiece(Square square)
        {
            if (square.Pic != null)
                if (turn.Player == PieceColor.Red && square.Pic.Color == PieceColor.Red ||
                   turn.Player == PieceColor.Black && square.Pic.Color == PieceColor.Black) 
                {
                    board[square.Row][square.Col].Texture = "/Checkers;component/Resources/SelectedPiece.png";
                    ShowMoves(square);
                }
        }
        public void MovePiece(Square square)
        {
            square.Pic = CurrentSquare.Pic;
            square.Pic.Sqr = square;

            if (Destinations[square] != null)
            {
                Destinations[square].Pic = null;
                TookPiece = true;
            }
            else
            {
                TookPiece = false;
                NextPlayer();
            }
            board[CurrentSquare.Row][CurrentSquare.Col].Texture = "/Checkers;component/Resources/BlackSquare.png";

            foreach (var neighbour in Destinations.Keys)
            {
                neighbour.Hint = null;
            }
            Destinations.Clear();
            CurrentSquare.Pic = null;
            CurrentSquare = null;

            if (square.Pic.Type == PieceType.Normal)
            {
                if (square.Row == 0 && square.Pic.Color == PieceColor.Red)
                {
                    square.Pic.Type = PieceType.King;
                    square.Pic.Texture = "/Checkers;component/Resources/RedKing.png";
                }
                else if (square.Row == board.Count - 1 && square.Pic.Color == PieceColor.Black)
                {
                    square.Pic.Type = PieceType.King;
                    square.Pic.Texture = "/Checkers;component/Resources/BlackKing.png";
                }
            }

            if (TookPiece)
            {
                if (turn.Player == PieceColor.Red)
                {
                    takenBlackPieces++;
                }
                if (turn.Player == PieceColor.Black)
                {
                    takenRedPieces++;
                }
                ShowMoves(square);
            }
            if (takenBlackPieces == 12 || takenRedPieces == 12)
                GameOver();
        }

        private void GameOver()
        {
            if (takenBlackPieces == 12)
            {
                UpdateScores(PieceColor.Red);
                MessageBox.Show("Red won!", "Congratulations!"); 
            }
            else
            {
                UpdateScores(PieceColor.Black);
                MessageBox.Show("Black won!", "Congratulations!");
            }
            ResetGame(board);
        }
        private void ResetGame(ObservableCollection<ObservableCollection<Square>> squares)
        {
            foreach (var square in Destinations.Keys)
            {
                square.Hint = null;
            }

            if (CurrentSquare != null)
            {
                CurrentSquare.Texture = "/Checkers;component/Resources/BlackSquare.png";
            }

            Destinations.Clear();
            CurrentSquare = null;
            TookPiece = false;
            JumpPossible = false;
            takenBlackPieces = 0;
            takenRedPieces = 0;
            mj.IsChecked = false;
            turn.Player = PieceColor.Red;

            for (int index1 = 0; index1 < 8; index1++)
            {
                for (int index2 = 0; index2 < 8; index2++)
                {
                    if ((index1 + index2) % 2 == 0)
                    {
                        squares[index1][index2].Pic = null;
                    }
                    else
                        if (index1 < 3)
                    {
                        squares[index1][index2].Pic = new Piece(PieceColor.Black);
                        squares[index1][index2].Pic.Sqr = squares[index1][index2];
                    }
                    else
                        if (index1 > 4)
                    {
                        squares[index1][index2].Pic = new Piece(PieceColor.Red);
                        squares[index1][index2].Pic.Sqr = squares[index1][index2];
                    }
                    else
                    {
                        squares[index1][index2].Pic = null;
                    }
                }
            }
        }
        public void SaveGame()
        {
            Checkpoint check = new Checkpoint
            {
                Board = board,
                MultipleJumps = mj,
                Turn = turn,
                TakenRedPieces = takenRedPieces,
                TakenBlackPieces = takenBlackPieces
            };
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                string jsonData = JsonSerializer.Serialize(check, new JsonSerializerOptions { WriteIndented = true});
                File.WriteAllText(filePath, jsonData);
            }
        }
        public void LoadGame() 
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            Checkpoint check = new Checkpoint();

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string jsonData = File.ReadAllText(filePath);
                check = JsonSerializer.Deserialize<Checkpoint>(jsonData);
            }
            for (int index1 = 0; index1 < 8; index1++)
                for (int index2 = 0; index2 < 8; index2++)
                    if (check.Board[index1][index2].Pic != null)
                        board[index1][index2].Pic = new Piece(check.Board[index1][index2].Pic.Color, check.Board[index1][index2].Pic.Type);
                    else
                        board[index1][index2].Pic = null;
            mj.IsChecked = check.MultipleJumps.IsChecked;
            turn.Player = check.Turn.Player;
            takenRedPieces = check.TakenRedPieces;
            takenBlackPieces = check.TakenBlackPieces;
        }
        public void About() 
        {
            string parentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            string aboutPath = Path.Combine(parentDirectory, "Resources", "about.txt");
            using (var reader = new StreamReader(aboutPath))
            {
                MessageBox.Show(reader.ReadToEnd(), "About", MessageBoxButton.OK);
            }
        }
        public void Stats() 
        {
            int redWins, blackWins;
            string parentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            string scoresPath = Path.Combine(parentDirectory, "Resources", "scores.txt");
            using (var reader = new StreamReader(scoresPath))
            {
                string line = reader.ReadLine();
                var scores = line.Split('-');
                redWins = int.Parse(scores[0]);
                blackWins = int.Parse(scores[1]);
            }
            MessageBox.Show($"Red: {redWins}\nBlack: {blackWins}", "Stats");
        }
        public void Exit()
        {
            Application.Current.Windows.OfType<PlayMenu>().SingleOrDefault(w => w.IsActive)?.Close();
        }
        private void UpdateScores(PieceColor winner)
        {
            int redWins, blackWins;
            string parentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            string scoresPath = Path.Combine(parentDirectory, "Resources", "scores.txt");
            using (var reader = new StreamReader(scoresPath))
            {
                string line = reader.ReadLine();
                var scores = line.Split('-');
                redWins = int.Parse(scores[0]);
                blackWins = int.Parse(scores[1]);
            }
            if (winner == PieceColor.Red)
                redWins++;
            else blackWins++;
            using (var writer = new StreamWriter(scoresPath))
            {
                writer.WriteLine(redWins + "-" + blackWins);
            }
        }
    }
}
