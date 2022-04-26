using AutoMapper;
using GkShp.Catalog.Application.ViewModels;
using GkShp.Catalog.Domain;
using GkShp.Core.DomainTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Application.Services
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductApplicationService(IProductRepository productRepository, IStockService stockService, IMapper mapper)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductModel>> GetProductByCategory(int code)
        {
            var products = await _productRepository.GetByCategoryAsync(code);
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel> GetProductById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductModel>(product);
        }


        public async Task AddProduct(ProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            _productRepository.Add(product);
            await _productRepository.UnitOfWork.Commit();

        }


        public async Task UpdateProduct(ProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            _productRepository.Update(product);
            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var models = _mapper.Map<IEnumerable<ProductModel>>(await _productRepository.GetAllAsync());
            return models;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            var models = _mapper.Map<IEnumerable<CategoryModel>>(await _productRepository.GetAllCategoriesAsync());
            return models;
        }

        public async Task<ProductModel> DebitStock(Guid id, int Quantity)
        {
            if(!_stockService.DebitStock(id, Quantity).Result)
            {
                throw new DomainException("Debit fail");
            }

            return _mapper.Map<ProductModel>(await _productRepository.GetByIdAsync(id));
        }

        public async Task<ProductModel> ReplaceStock(Guid id, int Quantity)
        {
            if (!_stockService.ReplaceStock(id, Quantity).Result) 
            {
                throw new DomainException("Raplace Stock Fail!");
            }

            return _mapper.Map<ProductModel>(await _productRepository.GetByIdAsync(id));
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose(); ;
        }
    }
}
