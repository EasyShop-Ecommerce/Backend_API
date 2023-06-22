using EasyShop.Infrastructure.Data;

public partial class DatabaseSeeder
{
    public class InitializationService
    {
        private readonly StoreContext _dbContext;

        public InitializationService(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            // Instantiate DatabaseSeeder and call SeedTestData to populate test data
            var databaseSeeder = new DatabaseSeeder(_dbContext);
            databaseSeeder.SeedTestData();
        }
    }

}
