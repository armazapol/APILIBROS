using APILIBROS.Data;
using APILIBROS.Helpers;
using APILIBROS.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IValidator<Libro>, LibroValidator>();

var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
builder.Services.AddDbContext<ExamenTadconDB>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/libros", async (Libro e, IValidator<Libro> validator, ExamenTadconDB db) =>
{

    var validationResult = await validator.ValidateAsync(e);

    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors
            .Select(e => e.ErrorMessage)
            .ToList();

        var errorResponse = new
        {
            Message = "Uno o más campos de entrada son inválidos.",
            Errors = errors
        };

        return Results.Json(errorResponse, statusCode: StatusCodes.Status400BadRequest);
    }

    var libro = await db.Libros.FindAsync(e.Id);
    if (libro is not null)
    {
        return Results.Json(new
        {
            Message = "El libro con el Id proporcionado ya existe."
        }, statusCode: StatusCodes.Status400BadRequest);
    }

    db.Libros.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/libro/{e.Id}", e);

});

app.MapGet("/libros/{id:int}", async (int id, ExamenTadconDB db) => {
    return await db.Libros.FindAsync(id)
        is Libro e
            ? Results.Ok(e)
            : Results.NotFound();
}
);

app.MapGet("/libros", async (ExamenTadconDB db) =>
{
    return await db.Libros.ToListAsync();
});

app.MapPut("/libros/{id:int}", async (int id, Libro e, IValidator<Libro> validator, ExamenTadconDB db) =>
{

    var validationResult = await validator.ValidateAsync(e);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors
            .Select(x => x.ErrorMessage)
            .ToList();

        var errorResponse = new
        {
            Message = "Uno o más campos de entrada son inválidos.",
            Errors = errors
        };

        return Results.Json(errorResponse, statusCode: StatusCodes.Status400BadRequest);
    }

    var libro = await db.Libros.FindAsync(id);
    if (libro is null) return Results.Json(new
    {
        Message = "El libro con el Id proporcionado no existe."
    }, statusCode: StatusCodes.Status400BadRequest);

    libro.Titulo = e.Titulo;
    libro.Autor = e.Autor;
    libro.AnioPublicacion = e.AnioPublicacion;
    libro.Editorial = e.Editorial;
    libro.CantidadPaginas = e.CantidadPaginas;
    libro.Categoria = e.Categoria;

    await db.SaveChangesAsync();

    return Results.Ok(libro);

});

app.MapDelete("/libros/{id:int}", async (int id, ExamenTadconDB db) => {
    var libro = await db.Libros.FindAsync(id);

    if (libro is not null)
    {
        db.Libros.Remove(libro);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    else
    {
        return Results.Json(new
        {
            Message = "El libro con el Id proporcionado no existe."
        }, statusCode: StatusCodes.Status400BadRequest);
    }
   
});

app.Run();
