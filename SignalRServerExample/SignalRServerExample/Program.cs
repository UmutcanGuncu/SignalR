using SignalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("policy",builder =>
    builder.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1").AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(origin => true)
    );
});
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("policy");
app.UseAuthorization();
app.UseEndpoints(obj =>
{
    obj.MapHub<MyHub>("myhub");
});
app.MapRazorPages();

app.Run();

