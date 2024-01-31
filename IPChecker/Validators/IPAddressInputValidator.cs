using FluentValidation;
using IPChecker.DTOS.SearchIPAddressDTOS;

namespace IPChecker.Validators
{
    public class IPAddressInputValidator : AbstractValidator<InputIPAddressDTO>
    {

        public IPAddressInputValidator() 
        {
            RuleFor(x => x.IPAddress).NotEmpty().MaximumLength(15).MinimumLength(15).WithMessage("'{PropertyName}' musst be 15 characters long");
        }
    }
}
