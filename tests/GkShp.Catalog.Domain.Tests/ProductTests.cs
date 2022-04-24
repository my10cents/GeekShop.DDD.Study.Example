using GkShp.Core.DomainTypes;
using System;
using Xunit;

namespace GkShp.Catalog.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Validate_ValidationsCanReturnExceptions()
        {
            // Arrange & Act & Assert

            var ex = Assert.Throws<DomainException>(() =>
                new Product(string.Empty, "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimension(1, 1, 1))
            );

            Assert.Equal("The field Name of product can't be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimension(1, 1, 1))
            );

            Assert.Equal("The field Description of product can't be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 0, Guid.NewGuid(), DateTime.Now, "Image", new Dimension(1, 1, 1))
            );

            Assert.Equal("The field Value of product can't be less or equal than 0", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.Empty, DateTime.Now, "Image", new Dimension(1, 1, 1))
            );

            Assert.Equal("The field CategoryId of product can't be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimension(1, 1, 1))
            );

            Assert.Equal("The field Image of product can't be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimension(0, 1, 1))
            );

            Assert.Equal("The field Height can't be less or equal than 0", ex.Message);
        }
    }
}