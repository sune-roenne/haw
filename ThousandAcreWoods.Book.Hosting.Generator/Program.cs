// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThousandAcreWoods.Application.Book.Persistence;
using ThousandAcreWoods.Book.Hosting.Generator.Configuration;
using ThousandAcreWoods.LocalStorage.Configuration;
using ThousandAcreWoods.LocalStorage;
using ThousandAcreWoods.Book.Hosting.Generator;

var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var bookConf = new SiteGeneratorConfiguration();
var localStorageConf = new LocalStorageConfiguration();
conf.GetSection(SiteGeneratorConfiguration.SiteGeneratorConfigurationElementName).Bind(bookConf);
conf.GetSection(LocalStorageConfiguration.LocalStorageConfigurationElementName).Bind(localStorageConf);

var serviceCollection = new ServiceCollection();
serviceCollection.Configure<SiteGeneratorConfiguration>(conf.GetSection(SiteGeneratorConfiguration.SiteGeneratorConfigurationElementName));
serviceCollection.Configure<LocalStorageConfiguration>(conf.GetSection(LocalStorageConfiguration.LocalStorageConfigurationElementName));
serviceCollection.AddLocalStorage();
serviceCollection.AddLogging();
serviceCollection.AddTransient<IHostingModelGenerator, HostingModelGenerator>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var bookLoader = serviceProvider.GetRequiredService<IBookRepository>();
var generator = serviceProvider.GetRequiredService<IHostingModelGenerator>();
var book = await bookLoader.LoadBookFromInput();

await generator.GenerateSiteData(book);


