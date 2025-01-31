using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.App.Features.Authentication.ForgetPassword.Commands;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Response.Endpint;

namespace InventoryManagementSystem.App.Features.Authentication.ForgetPassword
{
    [Route("api/auth/forget-password/")]
    public class ForgetPasswordEndpoint : BaseEndpoint<ForgetPassRequestViewModel, string>
    {
        public ForgetPasswordEndpoint(BaseEndpointParam<ForgetPassRequestViewModel> param) : base(param)
        {
        }

        [HttpPost]
        public async Task<EndpointResponse<string>> ForgetPassword([FromBody] ForgetPassRequestViewModel param)
        {
            var validationResult = ValidateRequest(param);
            if (!validationResult.IsSuccess)
                return EndpointResponse<string>.Failure(validationResult.ErrorCode, validationResult.Message);

            var result = await _mediator.Send(new ForgetPasswordCommand(param.Email));

            return result.IsSuccess ? EndpointResponse<string>.Success(string.Empty, "Email Sent with OTP verification code")
                             : EndpointResponse<string>.Failure(result.ErrorCode, result.Message);
        }

    }
}
