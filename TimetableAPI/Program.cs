using TimetableAPI.Repos;
using TimetableAPI.Deserializator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IClientResponceRepo, ClientResponceRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//TODO: �������� ��������� �������

builder.Services.AddOptions();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//app.Run();

Deserializator deserializator = new Deserializator();

deserializator.ShedulerDeserializator();
