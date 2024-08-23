

using AutoMapper;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Repository.IRepository;
using Mango.Services.CouponAPI.Repository;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

IMapper mapper = MappingConfigs.RegisterMaps().CreateMapper();

// Add services to the container.

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();


void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}


//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);


//        builder.Services.AddDbContext<AppDbContext>(option =>
//        {
//            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//        });

//        IMapper mapper = MappingConfigs.RegisterMaps().CreateMapper();

//        // Add services to the container.

//        builder.Services.AddSingleton(mapper);
//        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//        builder.Services.AddControllers();
//        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//        builder.Services.AddEndpointsApiExplorer();
//        builder.Services.AddSwaggerGen();


//        var app = builder.Build();

//        // Configure the HTTP request pipeline.
//        if (app.Environment.IsDevelopment())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI();
//        }

//        app.UseHttpsRedirection();

//        app.UseAuthorization();

//        app.MapControllers();

//        ApplyMigration();

//        app.Run();


//        void ApplyMigration()
//        {
//            using (var scope = app.Services.CreateScope())
//            {
//                var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//                if (_db.Database.GetPendingMigrations().Count() > 0)
//                {
//                    _db.Database.Migrate();
//                }
//            }
//        }
//    }
//}