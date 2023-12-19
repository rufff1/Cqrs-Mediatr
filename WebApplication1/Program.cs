using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.Cqrs.Handlers;
using Busines.Cqrs.Queries;
using Busines.DTOs.Product.Request;
using Busines.DTOs.Product.Response;
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
using Microsoft.Extensions.Options;
using Presentation.Middlewares;
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




#region Handler config
builder.Services.AddTransient<IRequestHandler<CreateCategoryCommand, Response<CategoryCreateDTO>>, CreateCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateCategoryCommand, Response<CategoryUpdateDTO>>, UpdateCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteCategoryCommand, Response>, DeleteCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<GetCategoryByIdQuery, Response<CategoryDTO>>, GetCategoryHandler>();
builder.Services.AddTransient<IRequestHandler<GetCategoryListQuery, Response<List<CategoryDTO>>>, GetCategorytListHandler>();

builder.Services.AddTransient<IRequestHandler<CreateProductCommand, Response<ProductCreateDTO>>, CreateProductHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateProductCommand, Response<ProductUpdateDTO>>, UpdateProductHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteProductCommand, Response>, DeleteProductHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>, GetProductHandler>();


#endregion


#region Repository config
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
#endregion



#region UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion


builder.Services.AddAutoMapper(x =>
{
    x.AddProfile(new CategoryMappingProfile());
    x.AddProfile(new ProductMappingProfile());
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
