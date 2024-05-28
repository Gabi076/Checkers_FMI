using Checkers.Services;
using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Checkers.Command;

namespace Checkers.ViewModels
{
    public class SquareVM: BaseNotification
    {
        private GameLogic gameLogic;
        private Square gameSquare;
        private ICommand clickPieceCommand;
        private ICommand movePieceCommand;

        public SquareVM(Square square, GameLogic gameLogic)
        {
            gameSquare = square;
            this.gameLogic = gameLogic;
        }

        public Square GameSquare
        {
            get
            { return gameSquare; }
            set
            { 
                this.gameSquare = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ClickPieceCommand
        {
            get
            {
                if (clickPieceCommand == null)
                {
                    clickPieceCommand = new RelayCommand<Square>(gameLogic.ClickPiece);
                }
                return clickPieceCommand;
            }
        }

        public ICommand MovePieceCommand
        {
            get
            {
                if (movePieceCommand == null)
                {
                    movePieceCommand = new RelayCommand<Square>(gameLogic.MovePiece);
                }
                return movePieceCommand;
            }
        }
    }
}
