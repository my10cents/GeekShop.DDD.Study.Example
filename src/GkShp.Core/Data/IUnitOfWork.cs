namespace GkShp.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
