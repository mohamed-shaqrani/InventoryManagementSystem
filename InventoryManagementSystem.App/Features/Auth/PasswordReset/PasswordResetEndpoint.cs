using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.App.Features.Authentication.PasswordReset;
using InventoryManagementSystem.App.Features.Authentication.PasswordReset.PasswordReset.Command;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Response.Endpint;

namespace InventoryManagementSystem.App.Features.Authentication.ResetPassword;

[Route("api/auth/reset-password")]
public class PasswordResetEndpoint : BaseEndpoint<PasswordResetViewModel, bool>
{
    public PasswordResetEndpoint(BaseEndpointParam<PasswordResetViewModel> param) : base(param)
    {
    }

    [HttpPost]
    public async Task<EndpointResponse<string>> PasswordReset([FromBody] PasswordResetViewModel param)
    {
        var validationResult = ValidateRequest(param);
        if (!validationResult.IsSuccess)
            return EndpointResponse<string>.Failure(validationResult.ErrorCode, validationResult.Message);

        var command = new PasswordResetCommand(param.Email, param.NewPassword, param.OTP);
        var result = await _mediator.Send(command);

        return result.IsSuccess ? EndpointResponse<string>.Success(string.Empty, result.Message)
                                : EndpointResponse<string>.Failure(result.ErrorCode, result.Message);
    }
}
