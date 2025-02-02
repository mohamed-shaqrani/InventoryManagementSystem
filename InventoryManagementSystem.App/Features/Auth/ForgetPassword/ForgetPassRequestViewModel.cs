using FluentValidation;

namespace InventoryManagementSystem.App.Features.Auth.ForgetPassword
{
    public record ForgetPassRequestViewModel(string Email);
    public class ForgetPassRequestViewModelValidator : AbstractValidator<ForgetPassRequestViewModel>
    {
        public ForgetPassRequestViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}