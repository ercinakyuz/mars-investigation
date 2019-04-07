using System.ComponentModel;

namespace MarsInvestigation.Core.Models
{
    public enum DirectionType
    {
        None = 0,
        [Description("N")]
        North = 1,
        [Description("W")]
        West = 2,
        [Description("S")]
        South = 3,
        [Description("E")]
        East = 4
    }
}
