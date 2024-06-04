
using Donor.Repositories;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Donor.DbContexts;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
     var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }

    builder.Services.AddDbContext<DonorContext>(options =>
        options.UseSqlite(connectionString)); 
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


    // Add services to the container.
    builder.Services.AddScoped<IDonorRepository, DonorRepository>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder =>
        {
            builder
                .WithOrigins("http://localhost:4300")
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

    app.UseRouting();
    app.UseCors("AllowSpecificOrigin");
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSerilogRequestLogging();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

