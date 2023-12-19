using AutoMapper;
using Busines.Cqrs.Commands;
using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Bussines.Cqrs.Queries;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.MappingProfiles
{
    public class CategoryMappingProfile :Profile
    {
        public CategoryMappingProfile() : base()
        {

            CreateMap<CategoryCreateDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryUpdateDTO, Category>().ReverseMap();





            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();


        }
    }
}
