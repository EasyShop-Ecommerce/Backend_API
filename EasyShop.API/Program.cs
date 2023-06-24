using EasyShop.Core.Entities;
using EasyShop.Core.Identity;
using EasyShop.Core.Interfaces;
using EasyShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EasyShop.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<DBContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			// Create User
			builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DBContext>();

			// [Autthorize] Used JWT Token In Check Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;	
				options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer =builder.Configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IStoreProductRepository, StoreProductRepository>();
			builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
			
            builder.Services.AddScoped(typeof (IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();
			app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthorization();


			app.MapControllers();

			app.Run();


			//using (var scope = app.Services.CreateScope())
			//{
			//	var serviceProvider = scope.ServiceProvider;
			//	var dbContext = serviceProvider.GetRequiredService<StoreContext>();

			//	// Instantiate DatabaseSeeder and call SeedTestData to populate test data
			//	var databaseSeeder = new DatabaseSeeder(dbContext);
			//	databaseSeeder.SeedTestData();
			//}

		}
    }
}