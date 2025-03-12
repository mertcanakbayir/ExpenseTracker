using Business.Abstract;
using Business.Concrete;
using DAL.Abstract;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSwaggerGen();
// DbContext kaydý (Connection string appsettings.json'dan okunur)
builder.Services.AddDbContext<ExpenseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
     .LogTo(Console.WriteLine, LogLevel.Information)
);


// Diðer servisler...
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
