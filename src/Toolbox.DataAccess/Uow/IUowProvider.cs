namespace Toolbox.DataAccess.Uow
{
    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork(bool autoDetectChanges = true, bool enableLogging = false);
    }
}
