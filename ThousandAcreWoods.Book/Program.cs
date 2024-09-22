
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThousandAcreWoods.Application.Book.Persistence;
using ThousandAcreWoods.Book.Configuration;
using ThousandAcreWoods.Book.Generator;
using ThousandAcreWoods.LocalStorage;
using ThousandAcreWoods.LocalStorage.Configuration;

var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var bookConf = new BookConfiguration();
var localStorageConf = new LocalStorageConfiguration();
conf.GetSection(BookConfiguration.BookConfigurationElementName).Bind(bookConf);
conf.GetSection(LocalStorageConfiguration.LocalStorageConfigurationElementName).Bind(localStorageConf);

var serviceCollection = new ServiceCollection();
serviceCollection.Configure<BookConfiguration>(conf.GetSection(BookConfiguration.BookConfigurationElementName));
serviceCollection.Configure<LocalStorageConfiguration>(conf.GetSection(LocalStorageConfiguration.LocalStorageConfigurationElementName));
serviceCollection.AddLocalStorage();
serviceCollection.AddLogging();

var serviceProvider = serviceCollection.BuildServiceProvider();

var bookLoader = serviceProvider.GetRequiredService<IBookRepository>();

var book = await bookLoader.LoadBookFromInput();


foreach(var withTheLir in new List<bool> { true, false })
{
    var texFileName = await LatexBookGenerator.GenerateLatexBook(book, bookConf, withTheLir: withTheLir);
    var pdfFileName = await LatexBookExecutor.GeneratePdfFile(bookConf, texFileName, suffix: withTheLir ? null : "publish");
}
