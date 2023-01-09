using ChapterAPI.Contexts;
using ChapterAPI.Interfaces;
using ChapterAPI.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ChapterContext,ChapterContext>(); // config servi�o contexto
builder.Services.AddTransient<LivroRepository, LivroRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();

//Adicionado servi�o de cros 

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7130")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


// Add sevi�o de JwtBearer : forma de autentica��o 

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})


//define as par�metros de valida��o do token

.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        // valida quem esta solicitando o acesso
        ValidateIssuer = true,
        // valida quem esta recebendo
        ValidateAudience = true,
        // valida se o tempo de expira��o ser� validado
        ValidateLifetime = true,
        // forma de criptografia e ainda valida a chave de autentica��o
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao")),
        // valida o tempo de expira��o do token
        ClockSkew = TimeSpan.FromMinutes(30),
        // nome do Issuer de onde est� vindo.
        ValidIssuer = "ChapterAPI",
        // nome do lugar para onde est� indo
        ValidAudience = "ChapterAPI"
    };
});


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

app.UseCors("corsPolicy");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
