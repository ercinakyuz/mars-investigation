using System.Collections.Generic;
using System.Linq;
using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core.Helpers
{
    public static class PlateauExtensions
    {
        public static Block GetRoverLocation(this Dictionary<Block,Rover> plateau, Rover rover)
        {
            return plateau.FirstOrDefault(x => x.Value != null && x.Value.Order.Equals(rover.Order)).Key;
        }
    }
}
