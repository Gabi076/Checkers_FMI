using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class MultipleJumps: BaseNotification
    {
        private bool isChecked;

        public MultipleJumps(bool isChecked)
        {
            this.isChecked = isChecked;
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set 
            { 
                isChecked = value;
                NotifyPropertyChanged();
            }
        }
    }
}
