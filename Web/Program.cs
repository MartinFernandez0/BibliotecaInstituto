using CurrieTechnologies.Razor.SweetAlert2;
using Service.Interfaces;
using Service.Services;
using Web.Components;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// caching memory   
builder.Services.AddMemoryCache();
// Auth service que usa el provider
builder.Services.AddScoped<FirebaseAuthService>();

builder.Services.AddScoped(typeof(IGenericService<object>),
    typeof(GenericService<object>));
//libroService
builder.Services.AddScoped<ILibroService, LibroService>();
//prestamoService
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
//usuarioService
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddSweetAlert2();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
