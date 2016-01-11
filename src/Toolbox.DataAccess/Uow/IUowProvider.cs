namespace Toolbox.DataAccess
{
    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork(bool autoDetectChanges = true, bool enableLogging = false);
    }
}
