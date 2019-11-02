using System;
using System.Collections.Generic;

namespace CheckersLogic.States
{
    public class MenuState:State
    {
        public int index { get; set; } = 0;
        public List<MenuOption> options { get; set; } = new List<MenuOption>()
        {
            new MenuOption("New Game VS PLAYER", new PlayerVsPlayer()),
            //TO DO: Change to PlayerVsComputer
            new MenuOption("New Game VS COMPUTER", new PlayerVsPlayer())
        };

    }

    public class MenuOption
    {
        private string _name;
        private State _optionState;
        public State OptionState
        {
            get
            {
                return _optionState;
            }
            set
            {
                _optionState = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public MenuOption(string name, State optionState)
        {
            this.Name = name;
            this.OptionState = optionState;
        }
    }

}
