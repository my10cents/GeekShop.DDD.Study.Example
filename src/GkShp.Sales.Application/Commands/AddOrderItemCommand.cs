using FluentValidation;
using GkShp.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = String.Empty;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public AddOrderItemCommand(Guid clientId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ClientId = clientId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Client ID");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Client ID");

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("The name of produto was not provided");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("The minimum quantity of an item is 1");

            RuleFor(x => x.Quantity)
                .LessThan(15)
                .WithMessage("The miximum quantity of an item is 15");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("The price of an item must be greater than 0");

        }
    }
}
