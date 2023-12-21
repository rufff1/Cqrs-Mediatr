using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.Cqrs.Handlers;
using Busines.Cqrs.Queries;
using Busines.DTOs.Blog.Request;
using Busines.DTOs.Blog.Response;
using Busines.DTOs.Product.Request;
using Busines.DTOs.Product.Response;
using Busines.DTOs.Tag.Request;
using Busines.DTOs.Tag.Response;
using Busines.MappingProfiles;
using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Bussines.Cqrs.Handlers;
using Bussines.Cqrs.Queries;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Presentation.Middlewares;
using Serilog;
using Serilog.Core;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Summarylerin dusmesi ucun swaggere
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Presentation.xml"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));

Logger log = new LoggerConfiguration()
      .WriteTo.Seq("http://localhost:5341")
      .Enrich.FromLogContext()
      .MinimumLevel.Information()
      .CreateLogger();

builder.Host.UseSerilog(log);

#region Handler config

//CATEGORY
builder.Services.AddTransient<IRequestHandler<CreateCategoryCommand, Response<CategoryCreateDTO>>, CreateCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateCategoryCommand, Response<CategoryUpdateDTO>>, UpdateCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteCategoryCommand, Response>, DeleteCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<GetCategoryByIdQuery, Response<CategoryDTO>>, GetCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<GetCategoryListQuery, Response<List<CategoryDTO>>>, GetCategorytListHandler>();

//PRODUCT
builder.Services.AddTransient<IRequestHandler<CreateProductCommand, Response<ProductCreateDTO>>, CreateProductHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateProductCommand, Response<ProductUpdateDTO>>, UpdateProductHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteProductCommand, Response>, DeleteProductHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>, GetProductHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductListQuery, Response<List<ProductDTO>>>, GetProductListHandler>();

//BLOG
builder.Services.AddTransient<IRequestHandler<CreateBlogCommand, Response<BlogCreateDTO>>, CreateBlogHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateBlogCommand, Response<BlogUpdateDTO>>, UpdateBlogHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteBlogCommand, Response>, DeleteBlogHandler>();
builder.Services.AddTransient<IRequestHandler<GetBlogByIdQuery, Response<BlogDTO>>, GetBlogHandler>();
builder.Services.AddTransient<IRequestHandler<GetBlogListQuery, Response<List<BlogDTO>>>, GetBlogListHandler>();

//TAG
builder.Services.AddTransient<IRequestHandler<CreateTagCommand, Response<TagCreateDTO>>, CreateTagHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateTagCommand, Response<TagUpdateDTO>>, UpdateTagHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteTagCommand, Response>, DeleteTagHandler>();
builder.Services.AddTransient<IRequestHandler<GetTagByIdQuery, Response<TagDTO>>, GetTagHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllTagQuery, Response<List<TagDTO>>>, GetTagListHandler>();








#endregion


#region Repository config
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
#endregion



#region UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion


builder.Services.AddAutoMapper(x =>
{
    x.AddProfile(new CategoryMappingProfile());
    x.AddProfile(new ProductMappingProfile());
    x.AddProfile(new BlogMappingProfile());
    x.AddProfile(new TagMappingProfile());
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<CustomExceptionMiddleware>();


app.Run();
