using System;
using System.Text.RegularExpressions;
using MarsInvestigation.Core.Commands;
using MarsInvestigation.Core.Helpers;
using MarsInvestigation.Core.Models;
using NUnit.Framework;

namespace MarsInvestigation.Test
{
    [TestFixture]
    public class RoverMoveTests: PlateauSetupFixture
    {
        [TestCase("1 2 N", "LMLMLMLMM", "1 3 N")]
        [TestCase("1 2 N", "LMLMLMLMM", "1 3 N")]
        [TestCase("2 2 N", "LMLMLPMLMM", "1 3 N")]
        [TestCase("3 3 E", "MMRMMRMRRM", "5 1 E")]
        public void MoveRoverExpectValidPositionOrThrows(string deployRoverCommandText, string moveRoverCommandText, string expectedPositionText)
        {
            ICommand<Rover> roverDeployCommand = new RoverDeployCommand(Plateau);
            var deployedRover = roverDeployCommand.Execute(deployRoverCommandText);
            ICommand<Rover> roverMoveCommand = new RoverMoveCommand(deployedRover, Plateau);

            var regexMoveCommandPattern = new Regex("^[LRM]+$");
            if (regexMoveCommandPattern.IsMatch(moveRoverCommandText))
            {
                var splittedPosition = expectedPositionText.Split(' ');
                int.TryParse(splittedPosition[0], out var expectedRoverXLocation);
                int.TryParse(splittedPosition[1], out var expectedRoverYLocation);
                var expectedRoverDirection = EnumHelper.GetValueFromDescription<DirectionType>(splittedPosition[2]);
                var expectedPosition = new Block { X = expectedRoverXLocation, Y = expectedRoverYLocation };
                if (Plateau.ContainsKey(expectedPosition) && Plateau[expectedPosition] == null)
                {
                    var movedRover = roverMoveCommand.Execute(moveRoverCommandText);
                    Assert.AreSame(deployedRover, movedRover);
                    var actualPosition = Plateau.GetRoverLocation(movedRover);

                    Assert.AreEqual(expectedRoverXLocation, actualPosition.X);
                    Assert.AreEqual(expectedRoverYLocation, actualPosition.Y);
                    Assert.AreEqual(expectedRoverDirection, movedRover.Direction);
                }
                else
                {
                    var ex = Assert.Throws<ArgumentException>(() => roverMoveCommand.Execute(moveRoverCommandText));
                    Assert.AreEqual($"Direction of {expectedRoverDirection} at order {deployedRover.Order} rover's move command is not valid!", ex.Message);
                }
            }
            else
            {
                var ex = Assert.Throws<ArgumentException>(() => roverMoveCommand.Execute(moveRoverCommandText));
                Assert.AreEqual($"Invalid character(s) in \"{moveRoverCommandText}\" text for move command.", ex.Message);
            }
        }
    }
}
