using AnketaAPI;
using AnketaAPI.Models;
using AnketaAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbConext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLDBConnection")));
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<CatalogSurvey>, CatalogSurveyRepository>();
builder.Services.AddScoped<IRepository<Question>, QuestionRepository>();
builder.Services.AddScoped<IRepository<CatalogSurveyQuestion>, CatalogSurveyQuestionRepository>();
builder.Services.AddScoped<IRepository<QuestionAnswer>, QuestionAnswerRepository>();
builder.Services.AddScoped<IRepository<UserCatalogSurvery>, UserCatalogSurveryRepository>();
builder.Services.AddScoped<IRepository<UserAnswer>, UserAnswerRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Enable CORS
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
