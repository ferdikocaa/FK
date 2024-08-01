using Business.Constants;
using Entities;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class ActivityTypeValidator : AbstractValidator<ActivityType>
    {
        public ActivityTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.ActivityTypeName);
            RuleFor(x => x.Code).NotEmpty().WithMessage(Messages.ActivityTypeCode);
        }
    }
}
