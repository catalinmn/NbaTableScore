var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddHttpClient();

builder.Services.AddHttpClient("API", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://free-nba.p.rapidapi.com/games");
    httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "335aabf663msh43452f55e991660p143e3djsn1d663ce75b3c");
    httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "free-nba.p.rapidapi.com");
    httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "free-nba.p.rapidapi.com");
    httpClient.DefaultRequestHeaders.Add("access-control-allow-origin", "*");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

#region snippet
//trying to add headers to every http request

/*app.Use((context, next) =>
{
    context.Request.HttpContext.Request.Headers.Add("X-RapidAPI-Key", "335aabf663msh43452f55e991660p143e3djsn1d663ce75b3c");
    context.Request.Headers.Add("X-RapidAPI-Host", "free-nba.p.rapidapi.com");

    return next(context);
});*/
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
