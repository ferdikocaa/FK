using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(Messages.NotNull);
        }
    }
}
