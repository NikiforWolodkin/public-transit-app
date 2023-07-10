using FluentValidation;
using Services.Abstractions.Dtos;

namespace Presentation.Validators
{
    public class BusStopAddDtoValidator : AbstractValidator<BusStopAddDto>
    {
        public BusStopAddDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Type must be a valid BusStopType");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90");
        }
    }
}
