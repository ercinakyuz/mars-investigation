using System.Collections.Generic;
using MarsInvestigation.Core.Commands;
using MarsInvestigation.Core.Models;
using NUnit.Framework;

namespace MarsInvestigation.Test
{
    [SetUpFixture]
    public abstract class PlateauSetupFixture
    {
        protected Dictionary<Block, Rover> Plateau;

        [OneTimeSetUp]
        public void Setup()
        {
            ICommand<Dictionary<Block, Rover>> createPlateauCommand = new CreatePlateauCommand();
            Plateau = createPlateauCommand.Execute("7 7");
        }
    }
}
