using AffiliateService.Api.Extensions;
using AffiliateService.Api.Filters;
using AffiliateService.Application.Extensions;
using AffiliateService.Infrastructure.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddPresentationLayer()
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration);

builder.Services
    .AddControllers(o =>
    {
        //Note: we can use filters for unhandled exception handling but we can also use middlewares, wich seems like a prefered way
        //o.Filters.Add<ErrorHandlingFilter>();
        o.Filters.Add<ModelValidationFilter>();
    })
    .ConfigureApiBehaviorOptions(o =>
    {
        o.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => {
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigurePresentationMiddlewares();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
