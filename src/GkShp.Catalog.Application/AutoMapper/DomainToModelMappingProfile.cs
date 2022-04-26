using AutoMapper;
using GkShp.Catalog.Application.ViewModels;
using GkShp.Catalog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GkShp.Catalog.Application.AutoMapper
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimension.Width))
                .ForMember(d => d.Height, o => o.MapFrom(d => d.Dimension.Height))
                .ForMember(d => d.Depth, o => o.MapFrom(d => d.Dimension.Depth));
            CreateMap<Category, CategoryModel>();
        }
    }

    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<ProductModel, Product>()
                .ConstructUsing(p => new Product( p.Name, p.Description, p.Active, p.Value, p.CategoryId, p.RecordDate, p.Image
                                                , new Dimension(p.Height, p.Width, p.Depth)));

            CreateMap<CategoryModel, Category>()
                .ConstructUsing(p => new Category(p.Name, p.Code));
        }
            
            
    }
}
