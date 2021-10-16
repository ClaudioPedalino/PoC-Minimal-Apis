public class PeopleValidator : AbstractValidator<People>
{
    public PeopleValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.Age).NotEmpty().GreaterThanOrEqualTo(16);
    }
}