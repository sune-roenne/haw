using ThousandAcreWoods.Application;
using ThousandAcreWoods.Infrastructure;
using ThousandAcreWoods.Persistence;
using ThousandAcreWoods.LocalStorage;
using ThousandAcreWoods.UI;
using ThousandAcreWoods.SVGTools.Parsing;
using ThousandAcreWoods.LocalStorage.Story.Conversion;
using ThousandAcreWoods.LocalStorage.Book;
using ThousandAcreWoods.Application.Book.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddConfigurationFiles()
    .AddConfiguration();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddLocalStorage();
builder.AddUi();
var app = builder.Build();

app.UseUi(builder.Configuration);

app.Run();
