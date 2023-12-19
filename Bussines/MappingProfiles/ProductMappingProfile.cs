using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.DTOs.Product.Request;
using Busines.DTOs.Product.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() :base()
        {
            CreateMap<ProductCreateDTO, Product>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();



            CreateMap<CreateProductCommand , Product>().ReverseMap();
            CreateMap<UpdateProductCommand , Product>().ReverseMap();   
        }
    }
}
