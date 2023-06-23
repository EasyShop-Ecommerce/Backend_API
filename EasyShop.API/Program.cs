using EasyShop.Core.Interfaces;
using EasyShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IStoreProductRepository, StoreProductRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>) );

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