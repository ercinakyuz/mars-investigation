using System;
using System.ComponentModel;
using System.Reflection;
using MarsInvestigation.Core.Commands;
using MarsInvestigation.Core.Helpers;
using MarsInvestigation.Core.Models;
using NUnit.Framework;

namespace MarsInvestigation.Test
{
    [TestFixture]
    public class RoverDeployTests: PlateauSetupFixture
    {
        [TestCase("10 5")]
        [TestCase("1 2 N")]
        [TestCase("10")]
        [TestCase("5 5 N")]
        [TestCase("5 5 N")]
        [TestCase("10 10 N")]
        [TestCase("N S")]
        [TestCase("10 S")]
        [TestCase("N 5")]
        [TestCase("7 5 8")]
        public void DeployRoverExpectValidLocationOrThrows(string deployRoverCommandText)
        {
            var splittedRoverInfo = deployRoverCommandText.Split(' ');

            ICommand<Rover> roverDeployCommand = new RoverDeployCommand(Plateau);

            if (splittedRoverInfo.Length == 3)
            {
                var isRoverLocationXParsedSuccessfully = int.TryParse(splittedRoverInfo[0], out var roverX);
                var isRoverLocationYParsedSuccessfully = int.TryParse(splittedRoverInfo[1], out var roverY);
                if (!isRoverLocationXParsedSuccessfully)
                {
                    var ex = Assert.Throws<InvalidCastException>(() =>
                        roverDeployCommand.Execute(deployRoverCommandText));
                    Assert.AreEqual("Invalid argument value for parsing upper value of x.", ex.Message);
                }
                else if (!isRoverLocationYParsedSuccessfully)
                {
                    var ex = Assert.Throws<InvalidCastException>(() =>
                        roverDeployCommand.Execute(deployRoverCommandText));
                    Assert.That(ex.Message, Is.EqualTo("Invalid argument value for parsing upper value of y."));
                }
                else if (!Plateau.ContainsKey(new Block { X = roverX, Y = roverY }) || Plateau[new Block { X = roverX, Y = roverY }] != null)
                {
                    var ex = Assert.Throws<ArgumentException>(() => roverDeployCommand.Execute(deployRoverCommandText));
                    Assert.AreEqual("The location is not valid for deploying.", ex.Message);
                }
                else
                {
                    var roverDirection = EnumHelper.GetValueFromDescription<DirectionType>(splittedRoverInfo[2]);
                    if (roverDirection == DirectionType.None)
                    {
                        var ex = Assert.Throws<InvalidEnumArgumentException>(() => roverDeployCommand.Execute(deployRoverCommandText));
                        Assert.AreEqual("roverDirection", ex.ParamName);
                    }
                    else
                    {
                        var rover = roverDeployCommand.Execute(deployRoverCommandText);
                        Assert.NotNull(rover);
                        Assert.AreEqual(roverDirection, rover.Direction);
                        var roverLocation = new Block { X = roverX, Y = roverY };
                        Assert.AreSame(Plateau[roverLocation], rover);
                    }
                }
            }
            else
            {
                var ex = Assert.Throws<TargetParameterCountException>(() => roverDeployCommand.Execute(deployRoverCommandText));
                Assert.That(ex.Message, Is.EqualTo($"{deployRoverCommandText} is not a valid deploy command, please give the order in \"x(1) y(2) direction(N)\" format."));
            }
        }

    }
}
