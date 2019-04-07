namespace MarsInvestigation.Core.Commands
{
    public interface ICommand<out TResult>
    {
        TResult Execute(string commandText);
    }
}
