using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using WebApplicationBlog.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Servicios - base de datos en memoria
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("BlogDb");
    Console.WriteLine("Base de datos en memoria configurada: BlogDb");
});

// Servicios de la colección de dependencias
builder.Services.AddScoped<IBlogPostRepository, InMemoryBlogPostRepository>();
builder.Services.AddScoped<BlogPostService>();
builder.Services.AddScoped<IPostCommentRepository, InMemoryPostCommentRepository>();
builder.Services.AddScoped<PostCommentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Blog API", Version = "v1" });
});

var app = builder.Build();

// Configuración de datos iniciales
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await DataInitializer.InitializeAsync(context);
}

// Configuración de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();