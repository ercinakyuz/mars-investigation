using System;
using System.Collections.Generic;
using System.Linq;
using MarsInvestigation.Core.Commands;
using MarsInvestigation.Core.Helpers;
using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core
{
    public class CommandCenter
    {
        private static Dictionary<Block, Rover> _plateau;

        public void ExecuteCommands(string[] commandTexts)
        {
            if (commandTexts.Length >= 3 && commandTexts.Length % 2 == 1)
            {
                _plateau = new CreatePlateauCommand().Execute(commandTexts[0]);
                
                Rover deployedRover = null;
                for (int i = 1; i < commandTexts.Length; i++)
                {
                    var modeOfCommand = (i - 1) % 2;
                    if (modeOfCommand == 0)
                    {
                        deployedRover = new RoverDeployCommand(_plateau).Execute(commandTexts[i]);
                    }
                    else
                    {
                        new RoverMoveCommand(deployedRover, _plateau).Execute(commandTexts[i]);
                    }
                }
            }
            else
            {
                throw new ArgumentException("It is not a valid command for execution.");
            }

        }

        public List<string> GetFinalRoverStates()
        {
            var finalRoverStates = new List<string>();
            var rovers = _plateau.Values.Where(w => w != null).OrderBy(o => o.Order);
            foreach (var rover in rovers)
            {
                var location = _plateau.GetRoverLocation(rover);
                finalRoverStates.Add($"{location.X} {location.Y} {EnumHelper.GetDescription(rover.Direction)}");
            }
            return finalRoverStates;
        }
    }
}
