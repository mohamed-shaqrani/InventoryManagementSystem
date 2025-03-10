﻿using InventoryManagementSystem.App.Features.Auth.ConfirmAccount.Command;
using InventoryManagementSystem.App.Features.Auth.Login;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Response.Endpint;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.App.Features.Auth.ConfirmAccount
{
    [Route("api/auth/confirm-account")]
    public class ConfirmAccountEndpoint : BaseEndpoint<ConfirmAccountViewModel, EndpointResponse<string>>
    {
        public ConfirmAccountEndpoint(BaseEndpointParam<ConfirmAccountViewModel> param) : base(param)
        {
        }

        [HttpPost]

        public async Task<EndpointResponse<AuthModel>> Register([FromBody] ConfirmAccountViewModel model)
        {
            var validationResult = ValidateRequest(model);
            if (!validationResult.IsSuccess)
                return EndpointResponse<AuthModel>.Failure(validationResult.ErrorCode, validationResult.Message);

            var register = new ConfirmAccountCommand(model.code);

            var res = await _mediator.Send(register);

            if (res.IsSuccess)
            {
                return EndpointResponse<AuthModel>.Success(res.Data, "Register Successfully");
            }

            return EndpointResponse<AuthModel>.Failure(res.ErrorCode, "Register Unsuccessfully");
        }
    }
}
