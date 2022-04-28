using GkShp.Core.DomainTypes;

namespace GkShp.Sales.Domain
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public DiscountTypeVoucher discountTypeVoucher { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UsedAt { get; private set; }
        public DateTime ValidUntil { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        //EF. Rel.
        public ICollection<Order> Order { get; private set; }

    }
}