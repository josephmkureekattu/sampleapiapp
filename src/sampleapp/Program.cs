using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using sampleapp.DBContext;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.



builder.Services.AddDbContext<AppDbContext>(
                    opts => opts.UseSqlServer(
                        builder.Configuration["DBConnection"],
                        option =>
                        {
                            option.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                            option.CommandTimeout(180);
                            option.EnableRetryOnFailure(
                                     maxRetryCount: 2,
                                     maxRetryDelay: TimeSpan.FromSeconds(10),
                                     errorNumbersToAdd: null
                                     );
                        }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();


app.Map("/", () => { return Results.Ok(); });
app.Map("/test", () => { return Results.Ok("test"); });


app.MapControllers();



app.Run();
