using System.Security.Claims;
using Data.Repositories;
using Infrastructure.Data.DbContext;
using Infrastructure.Entity.Role;
using Infrastructure.Entity.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service_Layer.Service.Jwt;

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
#region IjwtRepository
builder.Services.AddScoped<IJWTRepository, JWTRepository>();
#endregion
#region UserManager
builder.Services.AddTransient<UserManager<loginUser>>();
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
#region Jwt

builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
 {
     options.AccessDeniedPath = "/";
     options.LoginPath = "/";
 })
.AddJwtBearer(options =>
 {
     var secretkey = System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]);
     var encryptionkey = System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:EncryptKey"]);

     var validationParameters = new TokenValidationParameters
     {
         ClockSkew = TimeSpan.Zero, // default: 5 min
         RequireSignedTokens = true,

         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(secretkey),
         //Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])
         RequireExpirationTime = true,
         ValidateLifetime = true,

         ValidateAudience = true, //default : false
         ValidAudience = builder.Configuration["JWT:Audience"],

         ValidateIssuer = true, //default : false
         ValidIssuer = builder.Configuration["JWT:Issuer"],

         TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
         //Encoding.UTF8.GetBytes(builder.Configuration["JWT:EncryptKey"])
     };
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
     options.TokenValidationParameters = validationParameters;
     options.Events = new JwtBearerEvents
     {
         OnAuthenticationFailed = context =>
         {
             if (context.Exception != null)
                 throw new Exception("Authentication failed.");
             return Task.CompletedTask;
         },
         OnTokenValidated = async context =>
         {
             var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<loginUser>>();
             var userRepository = context.HttpContext.RequestServices.GetRequiredService<IRepository<loginUser>>();
             var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
             if (claimsIdentity.Claims?.Any() != true)
                 context.Fail("This token has no claims.");
             var securityStamp = claimsIdentity?.FindFirst(new ClaimsIdentityOptions().SecurityStampClaimType)?.Value;

             //Find user and token from database and perform your custom validation


             //if (user.SecurityStamp != Guid.Parse(securityStamp))
             //    context.Fail("Token secuirty stamp is not valid.");

             var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
             if (validatedUser == null)
                 context.Fail("Token secuirty stamp is not valid.");
         },
         OnChallenge = context =>
         {
             if (context.Request.Path.ToString().Contains("report"))
             {
                 context.Response.Redirect("/login");
                 context.HandleResponse();
                 return Task.CompletedTask;
             }
             if (context.AuthenticateFailure != null)
                 throw new Exception("Authenticate failure.");
             throw new Exception("Authenticate failure.");
             //return Task.CompletedTask;
         },
         OnMessageReceived = context =>
         {
             return Task.CompletedTask;
         }
     };
 });


/*builder.Services.Configure<DataProtectionTokenProviderOptions>(option => option.TokenLifespan = TimeSpan.FromHours(10));*/
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
