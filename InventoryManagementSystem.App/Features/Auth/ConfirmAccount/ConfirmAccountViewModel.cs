using FluentValidation;


namespace InventoryManagementSystem.App.Features.Auth.ConfirmAccount;

public record ConfirmAccountViewModel(string code);

public class RegisterViewModelValidator : AbstractValidator<ConfirmAccountViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.code).NotEmpty();
    }
}