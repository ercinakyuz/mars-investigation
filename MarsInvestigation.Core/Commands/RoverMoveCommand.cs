using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MarsInvestigation.Core.Models;
using MarsInvestigation.Core.MoveHandlers;

namespace MarsInvestigation.Core.Commands
{
    public class RoverMoveCommand : ICommand<Rover>
    {
        private readonly Rover _rover;
        private static readonly Dictionary<char, IMoveHandler> MoveHandlerDictionary = new Dictionary<char, IMoveHandler>
        {
            {'L',new TurnLeftHandler() },
            {'R',new TurnRightHandler() }
        };

        public RoverMoveCommand(Rover rover, Dictionary<Block, Rover> plateau)
        {
            _rover = rover;
            MoveHandlerDictionary['M'] = new MoveForwardHandler(plateau);
        }

        public Rover Execute(string commandText)
        {
            var regexMoveCommandPattern = new Regex("^[LRM]+$");
            if (regexMoveCommandPattern.IsMatch(commandText))
            {
                foreach (var singleCommand in commandText)
                {
                    if (MoveHandlerDictionary.ContainsKey(singleCommand))
                    {
                        MoveHandlerDictionary[singleCommand].Handle(_rover);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(singleCommand), singleCommand.ToString(),
                            $"{singleCommand} is not a valid command for IMoveHandler.");
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid character(s) in \"{commandText}\" text for move command.");
            }

            return _rover;
        }
    }
}
