using AppManagerServer;
using AppManagerServer.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var configData = new ConfigReader();
var dataStore = new DataStore();
dataStore.LoadApps(configData.Apps);
builder.Services.AddSingleton(dataStore);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
