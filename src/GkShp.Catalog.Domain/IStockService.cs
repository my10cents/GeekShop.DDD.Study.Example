namespace GkShp.Catalog.Domain
{
    public interface IStockService : IDisposable
    {
        Task<bool> DebitStock(Guid productId, int quantity);
        Task<bool> ReplaceStock(Guid productId, int quantity);
    }
}
