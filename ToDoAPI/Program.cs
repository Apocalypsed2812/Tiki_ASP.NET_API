using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoAPI.Models;
using ToDoAPI.Repositories;
using ToDoAPI.Validator;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoAPI.Repositories.ProductRepository;
using Microsoft.OpenApi.Models;
using ToDoAPI.Repositories.OrderRepository;
using ToDoAPI.Repositories.AccountRepository;
using ToDoAPI.Repositories.CategoryRepository;
using ToDoAPI.Repositories.TestBaseRepo;
using ToDoAPI.Repositories.HobbyRepository;
using ToDoAPI.Services.Hobby;
using ToDoAPI.Services.NewFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(x => { x.ImplicitlyValidateChildProperties = true; x.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()); });
//builder.Services.AddTransient<IValidator<HobbyModel>, HobbyValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("MyCors", policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<HobbyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Hobby"));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IHobbyService, HobbyService>();
builder.Services.AddTransient<IHobbyRepository, HobbyRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//builder.Services.AddTransient(IBaseRepository<Category>, BaseRepository<Category>);
builder.Services.AddTransient<ITestRepository, TestRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IPasswordHasher<AccountModel>, PasswordHasher<AccountModel>>();
builder.Services.AddTransient<ITestServices, TestServices>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "localhost:5238",
            ValidAudience = "localhost:5328",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ToDo"))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("MyCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
