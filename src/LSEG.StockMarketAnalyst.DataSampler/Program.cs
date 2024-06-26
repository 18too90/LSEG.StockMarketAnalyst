using LSEG.StockMarketAnalyst.DataSampler.Helpers;
using LSEG.StockMarketAnalyst.DataSampler.Kafka;
using LSEG.StockMarketAnalyst.DataSampler.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddSingleton<IFileHelper, FileHelper>();
builder.Services.AddSingleton<IDirectoryHelper, DirectoryHelper>();
builder.Services.AddTransient<ISampleManager, SampleManager>();
builder.Services.AddTransient<IStockPriceSamplesProducer, StockPriceSamplesProducer>();
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
