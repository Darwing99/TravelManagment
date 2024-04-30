using BlazorToastify;
using BLL.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using DAL.DataAccess;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddToasts();
builder.Services.AddSweetAlert2();
builder.Services.AddSweetAlert2(options => {
    options.Theme = SweetAlertTheme.Dark;
});
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserDao>(); 
builder.Services.AddScoped<EmployeeDao>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeBranchAssignmenService>();
builder.Services.AddScoped<EmployeeBranchAssignmenDao>();
builder.Services.AddScoped<CarrierService>();
builder.Services.AddScoped<CarrierDao>();
builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<TripDao>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<AlertServices>();
builder.Services.AddScoped<BranchService>();
builder.Services.AddScoped<BranchDao>();
builder.Services.AddScoped<UserStateService>();
builder.Services.AddScoped<TravelDetailService>();
builder.Services.AddScoped<TravelDetailDao>();
builder.Services.AddMemoryCache();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
