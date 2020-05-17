using FluentValidation;
using PPop.Domain.Tiles;

namespace PPop.Infrastructure.Validators.Validators
{
    public class TileValidator : AbstractValidator<TileNode>
    {
        public TileValidator()
        {
            RuleFor(t => t.Cost)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .Must(BeAnIntValueInRange).WithMessage("{PropertyName} is not a valid value");

            RuleFor(t => t.Representation)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} shouldn't be null")
                .NotEmpty().WithMessage("{PropertyName} shouldn't be null");
        }

        private bool BeAnIntValueInRange(int intValue)
        {
            return intValue >= -1;
        }
    }
}
