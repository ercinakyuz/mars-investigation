using System;
using System.Collections.Generic;
using MarsInvestigation.Core.Helpers;
using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core.MoveHandlers
{
    public class MoveForwardHandler : IMoveHandler
    {
        private readonly Dictionary<Block, Rover> _plateau;
        public MoveForwardHandler(Dictionary<Block, Rover> plateau)
        {
            _plateau = plateau;
        }
        public void Handle(Rover rover)
        {
            var currentRoverLocation = _plateau.GetRoverLocation(rover);
            var nextRoverLocation = currentRoverLocation;
            switch (rover.Direction)
            {
                case DirectionType.North:
                    nextRoverLocation.Y++;
                    break;
                case DirectionType.West:
                    nextRoverLocation.X--;
                    break;
                case DirectionType.South:
                    nextRoverLocation.Y--;
                    break;
                case DirectionType.East:
                    nextRoverLocation.X++;
                    break;
            }

            if (rover.Direction != DirectionType.None && IsValidMove(nextRoverLocation, _plateau))
            {
                _plateau[currentRoverLocation] = null;
                _plateau[nextRoverLocation] = rover;
            }
            else
            {
                throw new ArgumentException($"Direction of {rover.Direction} at order {rover.Order} rover's move command is not valid!");
            }
        }
        private bool IsValidMove(Block to, Dictionary<Block, Rover> plateau)
        {
            return plateau.ContainsKey(to) && plateau[to] == null;
        }

    }
}