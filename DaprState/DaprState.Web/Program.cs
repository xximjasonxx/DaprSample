using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/time", async () =>
{
    string daprStoreName = "redis-component";
    using var client = new DaprClientBuilder().Build();
    var timeResult = await client.GetStateAsync<string>(daprStoreName, "time");
    if (string.IsNullOrEmpty(timeResult))
    {
        timeResult = DateTime.UtcNow.ToString("o");
        await client.SaveStateAsync(daprStoreName, "time", timeResult,
            metadata: new Dictionary<string, string>
            {
                { "ttlInSeconds", "20" }
            });
    }

    return Results.Ok(new { time = timeResult });
})
.WithName("GetTime")
.WithOpenApi();

app.Run();
