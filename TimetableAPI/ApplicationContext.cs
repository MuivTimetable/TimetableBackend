namespace TimetableAPI
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):
            base(options)
        {

        }

        public DbSet<SchedulerDate> SchedulerDates { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Scheduler_Group> Schedulers_Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
