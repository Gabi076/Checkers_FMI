using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class Turn: BaseNotification 
    {
        private PieceColor player;
        
        public Turn(PieceColor player)
        {
            this.player = player;
        }
        public PieceColor Player
        {
            get { return player; }
            set
            {
                player = value;
                NotifyPropertyChanged();
            }
        }
    }
}
