using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            
            if (product == null) return false;

            if (!product.HaveStock(quantity)) return false;

            product.DebitStock(quantity);

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplaceStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (!product.HaveStock(quantity)) return false;

            product.DebitStock(quantity);

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
