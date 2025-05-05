using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Mapper;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Repositories.Concretes;
using ShopwareX.Services.Abstracts;
using ShopwareX.Services.Concretes;

namespace ShopwareX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("MySqlDbConnection");

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<AppDbContext>(options 
                => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IGenderRepository, GenderRepository>();
            
            builder.Services.AddScoped<IGenderService, GenderService>();

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

            app.Run();
        }
    }
}
