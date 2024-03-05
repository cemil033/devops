
using Build.Common.MassTransit;
using Build.Common.MongoDB;
using Build.MicroService.Entities;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddMongo()
                .AddMongoRepository<Item>("items")
                .AddMassTransitWithRabbitMq();

builder.Services.AddControllers(option =>
    option.SuppressAsyncSuffixInActionNames = false
);
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

app.Run();
