using FluentValidation;

namespace InventoryManagementSystem.App.Features.Authentication.ConfirmAccount;

public record ConfirmAccountViewModel(string code);

public class RegisterViewModelValidator : AbstractValidator<ConfirmAccountViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.code).NotEmpty();
    }
}