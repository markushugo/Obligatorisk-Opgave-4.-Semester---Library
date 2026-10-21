var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Register product service and repo from class library. Configure connection string in appsettings.json
builder.Services.AddScoped<Obligatorisk_Opgave_4._Semester.IRepos.IProductRepo>(sp =>
    new Obligatorisk_Opgave_4._Semester.Repos.ProductRepo(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));
builder.Services.AddScoped<Obligatorisk_Opgave_4._Semester.Abstractions.IProductService, Obligatorisk_Opgave_4._Semester.Services.ProductService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Obligatorisk_Opgave_4._Semester.Services.ICartService, Obligatorisk_Opgave_4._Semester.Services.SessionCartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
