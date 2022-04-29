namespace TimetableAPI
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):
            base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        //TODO: Добавить остальные сэты
    }
}
