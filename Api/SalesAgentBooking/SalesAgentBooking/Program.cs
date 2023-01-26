using SalesAgentBusiness.Classes;
using SalesAgentBusiness.Interfaces;
using SalesAgentDataAccess.Classes;
using SalesAgentDataAccess.DapperWrap;
using SalesAgentDataAccess.DbContext;
using SalesAgentDataAccess.Interfaces;

namespace SalesAgentBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<ISalesAgentDbContext, SalesAgentDbContext>();
            builder.Services.AddSingleton<IDapperWrapper, DapperWrapper>();
            builder.Services.AddScoped<IBookingSalesAgentBusiness, BookingSalesAgentBusiness>();
            builder.Services.AddScoped<IBookingSalesAgentRepository, BookingSalesAgentRepository>();
            builder.Services.AddControllers();
            builder.Services.AddDateOnlyTimeOnlyStringConverters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());
            
            var MyAllowedOrigins = "myAllowedOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowedOrigins,
                                      builder =>
                                      {
                                          builder.WithOrigins("http://localhost:4200")
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod();
                                      });
            });
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

            app.UseCors(MyAllowedOrigins);

            app.Run();
        }
    }
}