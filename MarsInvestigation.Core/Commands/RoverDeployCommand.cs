using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using MarsInvestigation.Core.Helpers;
using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core.Commands
{
    public class RoverDeployCommand : ICommand<Rover>
    {
        private readonly Dictionary<Block, Rover> _plateau;
        public RoverDeployCommand(Dictionary<Block, Rover> plateau)
        {
            _plateau = plateau;
        }
        public Rover Execute(string commandText)
        {
            Rover returnedRover = null;
            var splittedCommands = commandText.Split(' ');
            if (splittedCommands.Length == 3)
            {
                if (!int.TryParse(splittedCommands[0], out var roverXLocation))
                {
                    throw new ArgumentException("Invalid Rover X Location.");
                }
                if (!int.TryParse(splittedCommands[1], out var roverYLocation))
                {
                    throw new ArgumentException("Invalid Rover Y Location.");
                }
                var deployTo = new Block { X = roverXLocation, Y = roverYLocation };

                if (_plateau.ContainsKey(deployTo) && _plateau[deployTo] == null)
                {
                    var roverDirection = EnumHelper.GetValueFromDescription<DirectionType>(splittedCommands[2]);
                    if (roverDirection == DirectionType.None)
                    {
                        throw new InvalidEnumArgumentException(nameof(roverDirection), (int) DirectionType.None,typeof(DirectionType));
                    }
                    var roversOnPlateau = _plateau.Values.Where(w => w != null).ToList();
                    var maxRoverOrder = 0;
                    if (roversOnPlateau.Any())
                    {
                        maxRoverOrder = roversOnPlateau.Max(m => m.Order);
                    }

                    returnedRover = new Rover {Direction = roverDirection, Order = maxRoverOrder + 1};
                    _plateau[deployTo] = returnedRover;
                }
                else
                {
                    throw new ArgumentException("The location is not valid for deploying.");
                }
            }
            else
            {
                throw new TargetParameterCountException($"{commandText} is not a valid deploy command, please give the order in \"x(1) y(2) direction(N)\" format.");
            }

            return returnedRover;
        }
    }
}
