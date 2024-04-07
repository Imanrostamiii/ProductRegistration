using Data.Repositories;
using Infrastructure.Data.DbContext;
using Infrastructure.Entity.Role;
using Infrastructure.Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region DbContext
builder.Services.AddDbContext<AppDbcontext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("productSell"));
});
#endregion
#region Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
#endregion

#region Identity
builder.Services.AddIdentity<loginUser, Role>(identityOptions =>
    {
        identityOptions.Password.RequireDigit = false;
        identityOptions.Password.RequiredLength = 0;
        identityOptions.Password.RequireNonAlphanumeric = false;
        identityOptions.Password.RequireUppercase = false;
        identityOptions.Password.RequireLowercase = false;

        //UserName Settings
        identityOptions.User.RequireUniqueEmail = false;
    }).AddEntityFrameworkStores<AppDbcontext>()
    .AddDefaultTokenProviders();
#endregion
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

app.Run();
