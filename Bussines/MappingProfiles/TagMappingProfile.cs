using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.DTOs.Tag.Request;
using Busines.DTOs.Tag.Response;
using Common.Entities;

namespace Busines.MappingProfiles
{
    public class TagMappingProfile :Profile
    {
        public TagMappingProfile() :base() 
        {
            CreateMap<TagCreateDTO,Tag>().ReverseMap();
            CreateMap<TagUpdateDTO,Tag>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();


            CreateMap<CreateTagCommand, Tag>().ReverseMap();
            CreateMap<UpdateTagCommand, Tag>().ReverseMap(); 

        }
    }
}
