using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MarsInvestigation.Core.Commands;
using MarsInvestigation.Core.Models;
using NUnit.Framework;

namespace MarsInvestigation.Test
{
    [TestFixture]
    public class PlateauTests
    {
        [TestCase("10 5")]
        [TestCase("10")]
        [TestCase("5 5 N")]
        [TestCase("N S")]
        [TestCase("10 S")]
        [TestCase("N 5")]
        [TestCase("7 5 8")]
        public void CreatePlateauMustHaveExpectedSizeOrThrows(string upperDimensions)
        {
            var splittedDimensions = upperDimensions.Split(' ');
            ICommand<Dictionary<Block, Rover>> command = new CreatePlateauCommand();

            if (splittedDimensions.Length == 2)
            {
                var isUpperXParsedSuccessfully = int.TryParse(splittedDimensions[0], out var upperX);
                var isUpperYParsedSuccessfully = int.TryParse(splittedDimensions[1], out var upperY);
                if (!isUpperXParsedSuccessfully)
                {
                    var ex = Assert.Throws<InvalidCastException>(() => command.Execute(upperDimensions));
                    Assert.That(ex.Message, Is.EqualTo("Invalid argument value for parsing upper value of x."));
                }
                else if (!isUpperYParsedSuccessfully)
                {
                    var ex = Assert.Throws<InvalidCastException>(() => command.Execute(upperDimensions));
                    Assert.That(ex.Message, Is.EqualTo("Invalid argument value for parsing upper value of y."));
                }
                else
                {
                    var plateau = command.Execute(upperDimensions);
                    Assert.AreEqual(plateau.Count, (upperX + 1) * (upperY + 1));
                    foreach (var section in plateau)
                    {
                        Assert.NotNull(section.Key);
                        Assert.Null(section.Value);
                    }

                    Assert.AreEqual(plateau.Keys.Max(m => m.X), upperX);
                    Assert.AreEqual(plateau.Keys.Max(m => m.Y), upperY);
                }
            }
            else
            {
                var ex = Assert.Throws<TargetParameterCountException>(() => command.Execute(upperDimensions));
                Assert.That(ex.Message, Is.EqualTo("Invalid size for plateau. It needs two dimensions for creating, also the text must be seperated with space. "));
            }

        }
    }
}
