using System;
using System.Collections.Generic;
using System.Reflection;
using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core.Commands
{
    public class CreatePlateauCommand : ICommand<Dictionary<Block, Rover>>
    {
        public Dictionary<Block, Rover> Execute(string commandText)
        {
            Dictionary<Block, Rover> plateau = null;
            var splittedCommands = commandText.Split(' ');
            if (splittedCommands.Length == 2)
            {
                if (!int.TryParse(splittedCommands[0], out var upperX))
                {
                    throw new InvalidCastException("Invalid argument value for parsing upper value of x.");
                }
                ;
                if (!int.TryParse(splittedCommands[1], out var upperY))
                {
                    throw new InvalidCastException("Invalid argument value for parsing upper value of y.");
                }
                plateau = new Dictionary<Block, Rover>();
                for (int x = 0; x <= upperX; x++)
                {
                    for (int y = 0; y <= upperY; y++)
                    {
                        plateau.Add(new Block { X = x, Y = y }, null);
                    }
                }
            }
            else
            {
                throw new TargetParameterCountException("Invalid size for plateau. It needs two dimensions for creating, also the text must be seperated with space. ");
            }

            return plateau;
        }
    }
}
