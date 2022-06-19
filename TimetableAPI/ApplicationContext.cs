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
        public DbSet<Session> Session { get; set; }
        public DbSet<Scheduler_User_Totalizer> Scheduler_User_Totalizers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scheduler_Group>().HasKey(u => new { u.Scheduler_id, u.Group_id });

            modelBuilder.Entity<Session>().HasKey(u => new { u.User_id, u.SessionIdentificator });

            modelBuilder.Entity<Scheduler_User_Totalizer>().HasKey(u => new { u.Scheduler_id, u.User_id });

            modelBuilder.Entity<Permission>().HasData(
                new Permission {
                    Permission_id = 1,
                    Permission_name = "Студент"
                },
                new Permission {
                    Permission_id = 2,
                    Permission_name = "Староста или помощник"
                },
                new Permission
                {
                    Permission_id = 3,
                    Permission_name = "Преподаватель"
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    User_id = 1,
                    Name = "Роберт Полсон",
                    Login = "robpol",
                    Password = "qwerty",
                    Email = "70134928@online.muiv.ru",
                    Group_id = 1000017945,
                    Permission_id = 2
                },
                new User
                {
                    User_id = 2,
                    Name = "Артур Пендрагон",
                    Login = "70137919",
                    Password = "ZCj,frfNsCj,frf",
                    Email = "70137919@online.muiv.ru",
                    Group_id = 1000018364,
                    Permission_id = 1
                },
                new User
                {
                    User_id = 3,
                    Name = "Олегг",
                    Login = "1111",
                    Password = "qwert",
                    Email = "70139904@online.muiv.ru",
                    Group_id = 1000017945,
                    Permission_id = 2
                }
                ,
                new User
                {
                    User_id = 4,
                    Name = "Александр Лаптев",
                    Login = "70140101",
                    Password = "Qwe123asd",
                    Email = "70140101@online.muiv.ru",
                    Group_id = 1000018364,
                    Permission_id = 2
                });

            modelBuilder.Entity<Group>().HasData(
                new Group
                {
                    Group_id = 1111,
                    Group_name = "TEST"
                });
        }
    }
}
