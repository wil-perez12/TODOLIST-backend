using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TODO.ContextDB;
using TODO.Helpers;
using TODO.Interfaces;
using TODO.Services;
using TODOLIST_Infraestructure.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

//habilitando la notación para los endpoint
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); 
});

//scope para la interfaz de acceso al login
builder.Services.AddScoped<IAccesible, AccesoService>();
builder.Services.AddScoped<EncriptarHelper>();
builder.Services.AddScoped<TokenHelper>();
builder.Services.AddScoped<ValidarTokenHelper>();

//socpe para la interfaz de tareas
builder.Services.AddScoped<ITareas, TareaService>();

//conexion a la base de datos 
builder.Services.AddSqlServer<TodoContext>(builder.Configuration.GetConnectionString("Conection"));

//configuracion para el JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]!))
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
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
app.UseAuthentication();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
