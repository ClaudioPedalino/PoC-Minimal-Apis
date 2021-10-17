namespace net6.core.Validators
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Titulo)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Autor)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Genero)
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Precio)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
