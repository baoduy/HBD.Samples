var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapFallbackToFile("index.html");
app.Run();