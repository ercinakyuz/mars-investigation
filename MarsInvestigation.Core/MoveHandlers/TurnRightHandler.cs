using System;
using System.Collections.Generic;
using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core.MoveHandlers
{
    public class TurnRightHandler : IMoveHandler
    {
        private static readonly Dictionary<DirectionType, DirectionType> TurnRightDirectionDictionary = new Dictionary<DirectionType, DirectionType>
        {
            { DirectionType.North, DirectionType.East },
            { DirectionType.West, DirectionType.North },
            { DirectionType.South, DirectionType.West },
            { DirectionType.East, DirectionType.South }
        };
        public void Handle(Rover rover)
        {
            if (TurnRightDirectionDictionary.ContainsKey(rover.Direction))
            {
                rover.Direction = TurnRightDirectionDictionary[rover.Direction];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(rover.Direction), rover.Direction.ToString(), $"{rover.Direction} is not a valid command for TurnRightHandler.");
            }
        }
    }
}