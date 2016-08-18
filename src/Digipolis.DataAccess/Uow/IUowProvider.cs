namespace Digipolis.DataAccess
{
    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false);
    }
}
