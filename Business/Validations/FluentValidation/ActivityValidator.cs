using Business.Constants;
using Entities;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            RuleFor(x => x.ActivityTypeId).NotEmpty().WithMessage(Messages.ActivityTypeId);
            RuleFor(x => x.Description).NotEmpty().WithMessage(Messages.ActivityDescription);
        }
    }
}
