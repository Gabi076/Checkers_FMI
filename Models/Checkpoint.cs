using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class Checkpoint
    {
        public ObservableCollection<ObservableCollection<Square>> Board { get; set; }
        public MultipleJumps MultipleJumps { get; set; }
        public Turn Turn { get; set; }
        public int TakenRedPieces { get; set; }
        public int TakenBlackPieces { get; set; }
    }
}
