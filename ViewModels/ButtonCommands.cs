using Checkers.Commands;
using Checkers.Services;
using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    internal class ButtonCommands : BaseNotification
    {
        private GameLogic gameLogic;
        private ICommand saveCommand;
        private ICommand aboutCommand;
        private ICommand loadCommand;
        private ICommand statsCommand;
        private ICommand exitCommand;

        public ButtonCommands(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new NonGenericCommand(gameLogic.SaveGame);
                }
                return saveCommand;
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new NonGenericCommand(gameLogic.LoadGame);
                }
                return loadCommand;
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                {
                    aboutCommand = new NonGenericCommand(gameLogic.About);
                }
                return aboutCommand;
            }
        }
        public ICommand StatsCommand
        {
            get
            {
                if (statsCommand == null)
                {
                    statsCommand = new NonGenericCommand(gameLogic.Stats);
                }
                return statsCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new NonGenericCommand(gameLogic.Exit);
                }
                return exitCommand;
            }
        }
    }
}

