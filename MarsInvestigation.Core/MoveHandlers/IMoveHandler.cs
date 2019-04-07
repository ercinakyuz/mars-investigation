using MarsInvestigation.Core.Models;

namespace MarsInvestigation.Core.MoveHandlers
{
    public interface IMoveHandler
    {
        void Handle(Rover rover);
    }
}