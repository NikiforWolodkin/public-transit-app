using FluentValidation;
using Services.Dtos;

namespace Web.Validators
{
    public class DepartureTimetableAddDtoValidator : AbstractValidator<DepartureTimetableAddDto>
    {
        public DepartureTimetableAddDtoValidator()
        {
            RuleFor(timetable => timetable.DepartureTimes)
                .NotNull()
                .WithMessage("Departure times must not be null.")
                .NotEmpty()
                .WithMessage("Departure times must not be empty.")
                .Must(BeInAscendingOrder)
                .WithMessage("Departure times must be in ascending order.");
        }

        private bool BeInAscendingOrder(List<DateTime> departureTimes)
        {
            for (int i = 1; i < departureTimes.Count; i++)
            {
                if (departureTimes[i] <= departureTimes[i - 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
