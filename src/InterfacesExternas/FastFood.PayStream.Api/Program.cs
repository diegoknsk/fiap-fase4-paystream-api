using Microsoft.EntityFrameworkCore;
using FastFood.PayStream.Infra.Persistence;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Infra.Persistence.Repositories;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Infra.Services;
using System.Reflection;
using FastFood.PayStream.Infra.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using FastFood.PayStream.Api.Config.Auth;

// Configurar JWT Security Token Handler
JwtAuthenticationConfig.ConfigureJwtSecurityTokenHandler();

var builder = WebApplication.CreateBuilder(args);

// Obter connection string do appsettings.json
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(dbConnectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' não foi encontrada no appsettings.json.");
}

// Registrar DbContext com PostgreSQL
builder.Services.AddDbContext<PayStreamDbContext>(options =>
    options.UseNpgsql(dbConnectionString));

// Registrar repositórios
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Registrar Presenters
builder.Services.AddScoped<CreatePaymentPresenter>();
builder.Services.AddScoped<GenerateQrCodePresenter>();
builder.Services.AddScoped<GetReceiptPresenter>();
builder.Services.AddScoped<PaymentNotificationPresenter>();

// Registrar Gateways de Pagamento
builder.Services.AddScoped<PaymentFakeGateway>();
builder.Services.AddScoped<PaymentMercadoPagoGateway>();

// Registrar HttpClient para serviços externos
builder.Services.AddHttpClient();

// Registrar serviços externos
builder.Services.AddScoped<IKitchenService, KitchenService>();

// Registrar UseCases
builder.Services.AddScoped<CreatePaymentUseCase>();
builder.Services.AddScoped<GenerateQrCodeUseCase>(sp =>
{
    var paymentRepository = sp.GetRequiredService<IPaymentRepository>();
    var realGateway = sp.GetRequiredService<PaymentMercadoPagoGateway>();
    var fakeGateway = sp.GetRequiredService<PaymentFakeGateway>();
    var presenter = sp.GetRequiredService<GenerateQrCodePresenter>();
    return new GenerateQrCodeUseCase(paymentRepository, realGateway, fakeGateway, presenter);
});
builder.Services.AddScoped<GetReceiptUseCase>(sp =>
{
    var paymentRepository = sp.GetRequiredService<IPaymentRepository>();
    var realGateway = sp.GetRequiredService<PaymentMercadoPagoGateway>();
    var fakeGateway = sp.GetRequiredService<PaymentFakeGateway>();
    var kitchenService = sp.GetRequiredService<IKitchenService>();
    var presenter = sp.GetRequiredService<GetReceiptPresenter>();
    return new GetReceiptUseCase(paymentRepository, realGateway, fakeGateway, kitchenService, presenter);
});
builder.Services.AddScoped<PaymentNotificationUseCase>(sp =>
{
    var paymentRepository = sp.GetRequiredService<IPaymentRepository>();
    var realGateway = sp.GetRequiredService<PaymentMercadoPagoGateway>();
    var fakeGateway = sp.GetRequiredService<PaymentFakeGateway>();
    var presenter = sp.GetRequiredService<PaymentNotificationPresenter>();
    return new PaymentNotificationUseCase(paymentRepository, realGateway, fakeGateway, presenter);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // CustomerBearer scheme
    c.AddSecurityDefinition("CustomerBearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Customer token (Bearer {token})"
    });

    // Cognito scheme
    c.AddSecurityDefinition("Cognito", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Bearer do Cognito. Ex: 'Bearer {token}'"
    });

    c.OperationFilter<AuthorizeBySchemeOperationFilter>();
});

// Configure authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCustomerJwtBearer(builder.Configuration)
    .AddCognitoJwtBearer(builder.Configuration);

// Configure authorization policies
builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
