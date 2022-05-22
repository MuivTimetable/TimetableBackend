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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scheduler_Group>().HasKey(u => new { u.Scheduler_id, u.Group_id });

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

            //TODO: Удалить нижнее действия после создания обработчика групп

            modelBuilder.Entity<Group>().HasData(
                new Group
                {
                    Group_id = 1000017945,
                    Group_name = "о.ЭЗДт 32.2/Б-20"
                },
                new Group
                {
                    Group_id = 1000018011,
                    Group_name = "о.ИЗДт 30.2/Б1-20"
                },
                new Group
                {
                    Group_id = 1000018210,
                    Group_name = "л.ЭЗДт 32.1/Б1-20"
                },
                new Group
                { 
                    Group_id = 1000018364,
                    Group_name = "л.ЮВДтс 22.1/Б2-20"
                },
                new Group
                {
                    Group_id = 1000019061,
                    Group_name = "з.ЮЗДт 82.3/М2-20"
                },
                new Group
                {
                    Group_id = 1000019464,
                    Group_name = "РЮД 13.1-21"
                },
                new Group
                {
                    Group_id = 1000019466,
                    Group_name = "РЭД 21.1-21"
                },
                new Group
                {
                    Group_id = 1000019467,
                    Group_name = "РЭД 20.1-21"
                },
                new Group
                {
                    Group_id = 1000019558,
                    Group_name = "о.УЗДт 21.2/Б6-20"
                },
                new Group
                {
                    Group_id = 1000020418,
                    Group_name = "о.УЗДт 21.2/Б7-20"
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
                });
        }
    }
}
