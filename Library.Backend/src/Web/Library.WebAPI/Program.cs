using Library.Core;
using Library.Data.PostgreSql;
using Library.WebAPI.Authentication;
using Library.WebAPI.Swagger;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services
    .AddCore()
    .AddCustomHeaderAuthentication(services)
    .AddUserContext()
    .AddSwagger()
    .AddPostgreSql(x => x.ConnectionString = configuration.GetConnectionString("DbConnectionString"))
    .AddCors(options => options.AddPolicy(
        "AllowOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()));

services.AddControllers();

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();
        await migrator.MigrateAsync();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowOrigin");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
