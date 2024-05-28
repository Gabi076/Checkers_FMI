using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    public class TurnVM: BaseNotification
    {
        private Turn turn;

        public TurnVM(Turn turn)
        {
            this.turn = turn;
        }

        public Turn PlayerTurn
        {
            get { return turn; } 
            set 
            { 
                turn = value;
                NotifyPropertyChanged();
            }
        }
    }
}
