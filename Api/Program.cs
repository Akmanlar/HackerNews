using HackerNews.Data;
using HackerNews.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();
builder.Services.AddHttpClient<IHackerNewsClient, HackerNewsClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("HackerNewsClient:BaseUrl"));
    c.Timeout = TimeSpan.FromSeconds(builder.Configuration.GetValue<int>("HackerNewsClient:Timeout"));
});
builder.Services.Decorate<IHackerNewsClient, CachedHackerNewsClient>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();