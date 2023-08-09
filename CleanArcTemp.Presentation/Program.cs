using CleanArcTemp.Application;
using CleanArcTemp.Infrastructure;
using CleanArcTemp.Presentation;

var builder = WebApplication.CreateBuilder(args);


var authenticationSchemesBuilder = new ConfigurationBuilder()
    .AddJsonFile("authenticationSchemes.json", optional: true,
        reloadOnChange: true).Build();


// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration, authenticationSchemesBuilder);
builder.Services.PresentationDependencyInjection();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();

