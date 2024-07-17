using RecruitmentTest.Application;
using RecruitmentTest.Common;
using RecruitmentTest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.Configure<ApiOptions>(
    builder.Configuration.GetSection(nameof(ApiOptions)));

builder.Services.AddHttpClient();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.MapControllers();

//var dbContext = app.Services.GetRequiredService<AppDbContext>();


app.Run();

// private void UpdateDbTables(AppDbContext dbContext)
// {
//     
// }