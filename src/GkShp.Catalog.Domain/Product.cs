using GkShp.Core.DomainTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime RecordDate { get; private set; }
        public string? Image { get; private set; }
        public int StockQuantity { get; private set; }

        public Category? Category { get; private set; }

        protected Product() { }

        public Product(Guid categoryId, string name, string description, bool active, decimal value, DateTime recordDate, string image, int stockQuantity)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            RecordDate = recordDate;
            Image = image;
            StockQuantity = stockQuantity;
        }

        //ad-hoc setters
        public void Ativate() => Active = true;

        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }


        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            StockQuantity -= quantity;
        }

        public void RefillStock(int quantity)
        {
            StockQuantity += quantity;
        }

        public bool HaveStock(int quantity)
        {
            return StockQuantity >= quantity;
        }

        public void Validate()
        {

        }

    }

    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
        }
    }
}
