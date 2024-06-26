using LSEG.StockMarketAnalyst.OutlierDetector.Helpers;
using LSEG.StockMarketAnalyst.OutlierDetector.Kafka;
using LSEG.StockMarketAnalyst.OutlierDetector.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddSingleton<IFileHelper, FileHelper>();
builder.Services.AddSingleton<IStatisticsHelper, StatisticsHelper>();
builder.Services.AddTransient<IOutlierManager, OutlierManager>();
builder.Services.AddTransient<IStocksPriceSamplesConsumer, StocksPriceSamplesConsumer>();
builder.Services.AddControllers();
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
