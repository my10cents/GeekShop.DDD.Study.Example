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
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime RecordDate { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }

        public Dimension Dimension { get; private set; }

        public Category Category { get; private set; }

        protected Product() { }

        public Product(Guid categoryId, string name, string description, bool active, decimal value, DateTime recordDate, string image, int stockQuantity, Dimension dimension)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            RecordDate = recordDate;
            Image = image;
            StockQuantity = stockQuantity;
            Dimension = dimension;

            Validate();
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
            Validations.ValidateIfEmpty(Description, "The field Description of product can't be empty");
            Description = description;
        }

        public void DebitStock(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HaveStock(quantity)) throw new DomainException("Insufficient stock");
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
            Validations.ValidateIfEmpty(Name, "The field Name of product can't be empty");
            Validations.ValidateIfEmpty(Description, "The field Description of product can't be empty");
            Validations.ValidateIfEqual(CategoryId, Guid.Empty, "The field Value can't be empty");
            Validations.ValidateIfLessThan(Value, 1, "The field Value of the product can't be less or equal than 0");
            Validations.ValidateIfEmpty(Image, "The field image of product can't be empty");
        }
    }
}
