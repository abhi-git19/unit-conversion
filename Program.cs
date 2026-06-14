using UnitConverterApi.Infrastructure.JsonConverters;
using UnitConverterApi.Middleware;
using UnitConverterApi.Models;
using UnitConverterApi.Services;
using UnitConverterApi.Services.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CaseInsensitiveEnumConverter<ConversionCategory>());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Unit Converter API",
        Version = "v1",
        Description = "A .NET Core API for converting values between units (length, weight, temperature, volume, area)."
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Register each strategy keyed by its ConversionCategory. The strategy is NOT
// instantiated here — it is created by the DI container each time a
// request asks for that category. Adding a new category = one new line below
// plus a new IConversionStrategy implementation; nothing else changes.
builder.Services.AddKeyedScoped<IConversionStrategy, LengthConversionStrategy>(ConversionCategory.Length);
builder.Services.AddKeyedScoped<IConversionStrategy, WeightConversionStrategy>(ConversionCategory.Weight);
builder.Services.AddKeyedScoped<IConversionStrategy, TemperatureConversionStrategy>(ConversionCategory.Temperature);
builder.Services.AddKeyedScoped<IConversionStrategy, VolumeConversionStrategy>(ConversionCategory.Volume);
builder.Services.AddKeyedScoped<IConversionStrategy, AreaConversionStrategy>(ConversionCategory.Area);

// The factory receives the full list of registered keys so it can enumerate
// all strategies on demand without resolving every one upfront.
var registeredCategories = new[]
{
    ConversionCategory.Length,
    ConversionCategory.Weight,
    ConversionCategory.Temperature,
    ConversionCategory.Volume,
    ConversionCategory.Area
};

builder.Services.AddScoped<IConversionStrategyFactory>(sp =>
    new ConversionStrategyFactory(sp, registeredCategories));

// Application service
builder.Services.AddScoped<IConversionService, ConversionService>();

// CORS - allow any origin for demo purposes; tighten for production
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();