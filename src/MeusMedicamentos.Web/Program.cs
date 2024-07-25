using MeusMedicamentos.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddControllersWithViews();

builder.Services.ResolverDependencias(builder.Configuration);

var app = builder.Build();

// Configurar o pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
