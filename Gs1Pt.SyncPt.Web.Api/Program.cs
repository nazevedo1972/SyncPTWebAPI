using Gs1Pt.SyncPt.Component.Auth;
using Gs1Pt.SyncPt.Web.Api.HttpClients;
using Gs1Pt.SyncPt.Web.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

#region Serilog
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
#endregion

#region Services

#region Auth
var jwtSettings = new Gs1Pt.SyncPt.Component.Auth.Models.JwtSettings
{
    SecurityKey = settings?.JWTSettings?.SecurityKey,
    ValidIssuer = settings?.JWTSettings?.ValidIssuer,
    ValidAudience = settings?.JWTSettings?.ValidAudience,
    ExpiryInMinutes = settings?.JWTSettings?.ExpiryInMinutes ?? 5
};
builder.Services.UseAuth(connectionStrings?.SyncPtDb, jwtSettings);

#region Authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.ValidIssuer,
        ValidAudience = jwtSettings.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
    };
});
#endregion
#endregion

#region HttpClients
builder.Services.AddHttpClient<ISyncPtInternalApi, SyncPtInternalApi>("SyncPtInternalApi", client =>
{
    //client.BaseAddress = new Uri("https://localhost:44336");
    client.BaseAddress = new Uri("https://syncpt-api-prd.azurewebsites.net");
    //client.DefaultRequestHeaders.Add("APIKey", Program.Settings.Gs1GlobalRegistryApiKey);
    client.Timeout = TimeSpan.FromSeconds(5 * 60);
});
#endregion

#endregion

#region Json settings
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
#endregion

builder.Services.AddControllers();

#region Swagger/OpenAPI

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,        
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    //Swagger Documentation Section
    var info = new OpenApiInfo()
    {
        Title = "SyncPT REST API",
        Version = "v3",
        Description = "SyncPT REST API",
        Contact = new OpenApiContact()
        {
            Name = "SyncPT suporte",
            Email = "ingo@gs1pt.org",
        }

    };
    setup.SwaggerDoc("v3", info);

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setup.IncludeXmlComments(xmlPath);
});


#endregion


var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v3/swagger.json", "v3");
    c.RoutePrefix = string.Empty;// Set Swagger UI at apps root
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
