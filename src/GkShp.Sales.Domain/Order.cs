using GkShp.Core.DomainTypes;

namespace GkShp.Sales.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool UsedVoucher { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        //EF Relationship
        public virtual Voucher Voucher { get; private set; }

        public Order(Guid clientId, bool usedVoucher, decimal discount, decimal totalValue)
        {
            ClientId = clientId;
            UsedVoucher = usedVoucher;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            UsedVoucher = true;
            CalculateOrderValue();
        }

        public void CalculateOrderValue()
        {
            TotalValue = OrderItems.Sum(i => i.CalculateValue());
            CalculateDiscountTotalValue();
        }

        public void CalculateDiscountTotalValue()
        {
            if (!UsedVoucher) return;

            var discount = 0m;
            var value = TotalValue;

            if (Voucher.discountTypeVoucher == DiscountTypeVoucher.Percentage)
            {
                if (Voucher.Percentual.HasValue)
                {
                    discount = (value * Voucher.Percentual.Value) / 100;
                    value -= discount;
                }
            } 
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0m : value;
            Discount = discount;
        }

        public bool OrderHaveItem(OrderItem item)
        {
            return _orderItems.Any(i => i.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (OrderHaveItem(item))
            {
                OrderItem? existingItem = _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.AddUnits(item.Quantity);
                    item = existingItem;
                    _orderItems.Remove(existingItem);
                }
            }
            item.CalculateValue();

            _orderItems.Add(item);

            CalculateOrderValue();

        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            OrderItem? existingItem = OrderItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("The item does not belong to the order");
            
            _orderItems.Remove(existingItem);

            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            OrderItem? existingItem = _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("The item does not belong to the order");

            _orderItems.Remove(existingItem);

            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void UpdateUnits(OrderItem item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        //draft = rascunho
        public void SetStatusToDraft()
        {        
            OrderStatus = OrderStatus.Draft;
        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;
        }

        public void FinalizeOrder()
        {
            OrderStatus = OrderStatus.Paid;
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid clientId)
            {
                var order = new Order()
                {
                    ClientId = clientId
                };
                order.SetStatusToDraft();
                return order;
            }
        }
    }
}
//AbBend