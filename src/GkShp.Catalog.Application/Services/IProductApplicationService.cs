using GkShp.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Application.Services
{
    public interface IProductApplicationService : IDisposable
    {
        Task<IEnumerable<ProductModel>> GetProductByCategory(int code);
        Task<ProductModel> GetProductById(Guid id);
        Task<IEnumerable<ProductModel>> GetAllAsync();
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task AddProduct(ProductModel product);
        Task UpdateProduct(ProductModel product);

        Task<ProductModel> DebitStock(Guid id, int Quantity);
        Task<ProductModel> ReplaceStock(Guid id, int Quantity);
    }
}
