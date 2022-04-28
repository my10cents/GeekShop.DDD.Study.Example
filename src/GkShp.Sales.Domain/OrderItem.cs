using GkShp.Core.DomainTypes;

namespace GkShp.Sales.Domain
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; } 
        public Guid ProductId { get; private set; } 
        public string ProductName { get; private set; } = String.Empty;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; } 

        //EF Rel.
        public Order Order { get; private set; } = null!;

        protected OrderItem() { }

        public OrderItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            OrderId = Guid.Empty;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateValue()
        {
            return Quantity * UnitPrice;
        }

        internal void AddUnits(int units)
        {
            Quantity += units;
        }

        internal void UpdateUnits(int units)
        {
            Quantity = units;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}