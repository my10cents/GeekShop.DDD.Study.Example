using GkShp.Core.DomainTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Domain.Events
{
    public class ProductLowStockEvent : DomainEvent
    {
        public int RemainingQuantity { get; private set; }

        public ProductLowStockEvent(Guid aggregateId, int remainingQuantity) : base(aggregateId)
        {
            RemainingQuantity = remainingQuantity;
        }
    }
}
