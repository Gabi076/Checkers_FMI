using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    public class MultipleJumpsVM : BaseNotification
    {
        private MultipleJumps multipleJumps;

        public MultipleJumpsVM(MultipleJumps multipleJumps)
        {
            this.multipleJumps = multipleJumps;
        }

        public MultipleJumps IsChecked
        {
            get { return multipleJumps; }
            set
            {
                multipleJumps = value;
                NotifyPropertyChanged();
            }
        }
    }
}
