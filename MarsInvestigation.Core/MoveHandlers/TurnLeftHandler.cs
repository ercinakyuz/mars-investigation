using System;
using System.Collections.Generic;
using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core.MoveHandlers
{
    public class TurnLeftHandler : IMoveHandler
    {
        private static readonly Dictionary<DirectionType, DirectionType> TurnLeftDirectionDictionary = new Dictionary<DirectionType, DirectionType>
        {
            { DirectionType.North, DirectionType.West },
            { DirectionType.West, DirectionType.South },
            { DirectionType.South, DirectionType.East },
            { DirectionType.East, DirectionType.North }
        };
        public void Handle(Rover rover)
        {
            if (TurnLeftDirectionDictionary.ContainsKey(rover.Direction))
            {
                rover.Direction = TurnLeftDirectionDictionary[rover.Direction];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(rover.Direction), rover.Direction.ToString(), $"{rover.Direction} is not a valid command for TurnLeftHandler.");
            }
        }
    }
}