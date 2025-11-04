using APILIBROS.Models;
using FluentValidation;

namespace APILIBROS.Helpers
{
    public class LibroValidator : AbstractValidator<Libro>
    {
        public LibroValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio.") 
                .MaximumLength(150).WithMessage("El título no puede exceder los 150 caracteres.");
                

            RuleFor(x => x.Autor)
                .NotEmpty().WithMessage("El autor es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del autor no puede exceder los 100 caracteres.");

            RuleFor(x => x.Editorial)
                .NotEmpty().WithMessage("La editorial es obligatoria.")
                .MaximumLength(80).WithMessage("La editorial no puede exceder los 80 caracteres.");

            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("La categoría es obligatoria.");

            RuleFor(x => x.AnioPublicacion)
                .NotNull().WithMessage("El Año de Publicación es obligatorio") 
                .InclusiveBetween(1454, 2099).WithMessage("El año de publicación debe estar entre 1454 y 2099.");

            RuleFor(x => x.CantidadPaginas)
                .NotNull().WithMessage("La cantidad de páginas es obligatoria.")
                .GreaterThan(0).WithMessage("La cantidad de páginas debe ser mayor a cero.")
                .LessThanOrEqualTo(10000).WithMessage("La cantidad de páginas no puede exceder las 10,000.");
        }
    }
}
