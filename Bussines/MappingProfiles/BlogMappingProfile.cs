using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.DTOs.Blog.Request;
using Busines.DTOs.Blog.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.MappingProfiles
{
    public class BlogMappingProfile :Profile
    {
        public BlogMappingProfile() :base()
        {
            CreateMap<BlogCreateDTO, Blog>().ReverseMap();
            CreateMap<BlogUpdateDTO,Blog>().ReverseMap();
            CreateMap<Blog, BlogDTO>().ReverseMap();


            CreateMap<CreateBlogCommand , Blog>().ReverseMap();
            CreateMap<UpdateBlogCommand, Blog>().ReverseMap();
        }
    }
}
