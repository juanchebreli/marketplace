using marketplace.Context;
using Microsoft.EntityFrameworkCore;
using marketplace.WebSocket;
using marketplace.Configurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors open for this moment
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("*");
        builder.WithHeaders("*");
        builder.WithMethods("*");
    });
});


// add jwt to service
var JWTSection = builder.Configuration.GetSection("Jwt");
builder.Services.AddJWT(JWTSection);

// connect to db
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("ConnectionString")));

// configure DI for application services
builder.Services.AddServices();

// configure DI for application repositories
builder.Services.AddRepositories();

// configure DI for application helpers
builder.Services.AddHelpers();

//seed
builder.Services.AddTransient<DataSeeder>();

//signal R for websocket
builder.Services.AddSignalR();


var app = builder.Build();

// configure Exception middleware
app.ConfigureExceptionHandler();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
	SeedData(app);

//Seed Data
void SeedData(IHost app)
{
	var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

	using (var scope = scopedFactory.CreateScope())
	{
		var service = scope.ServiceProvider.GetService<DataSeeder>();
		service.SeedStates();
	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
	endpoints.MapHub<NewOfferHub>("hub/newOffer");
});

app.Run();
