using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductLowStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductLowStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(notification.AggregateId);

            //Enviar um email para aquisicao de mais produtos.

        }
    }
}
