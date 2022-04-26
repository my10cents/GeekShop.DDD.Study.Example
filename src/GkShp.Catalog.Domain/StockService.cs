using GkShp.Catalog.Domain.Events;
using GkShp.Core.Bus;
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
        private readonly IMediatorHandler _bus;

        public StockService(IProductRepository productRepository, IMediatorHandler bus)
        {
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            
            if (product == null) return false;

            if (!product.HaveStock(quantity)) return false;

            product.DebitStock(quantity);

            if (product.StockQuantity < 10)
            {
                await _bus.PublishEvent(new ProductLowStockEvent(product.Id, product.StockQuantity));
            }

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
