using FluentValidation;

public class TileValidator : AbstractValidator<Tile>
{
    public TileValidator()
    {
        RuleFor(t => t.Cost)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} shouldn't be null")
            .Must(BeAPositiveIntValue).WithMessage("{PropertyName} is not a valid value");

        RuleFor(t => t.Position)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} shouldn't be null")
            .Must(BeAPositiveIntValue).WithMessage("{PropertyName} is not a valid value");
    }

    private bool BeAPositiveIntValue(int intValue)
    {
        return intValue >= 0;
    }
}
