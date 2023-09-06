﻿using Codend.Application.Core.Abstractions.Authentication;
using FluentResults;
using io.fusionauth;
using io.fusionauth.domain;
using io.fusionauth.domain.api;
using io.fusionauth.domain.api.user;
using Microsoft.Extensions.Options;

namespace Codend.Infrastructure.Authentication;

/// <summary>
/// Fusionauth implementation of <see cref="IAuthService"/>.
/// </summary>
public sealed class AuthService : IAuthService
{
    private readonly IFusionAuthAsyncClient _fusionAuthClient;
    private readonly Guid _appId;

    public AuthService(IOptions<FusionauthConfiguration> configuration)
    {
        var fusionauthConfiguration = configuration.Value;
        _fusionAuthClient = new FusionAuthClient(
            fusionauthConfiguration.ApiKey,
            fusionauthConfiguration.ApiUrl,
            fusionauthConfiguration.TenantId);
        _appId = new Guid(fusionauthConfiguration.ApplicationId);
    }

    /// <inheritdoc />
    public async Task<Result<string>> LoginAsync(string email, string password)
    {
        var loginRequest = new LoginRequest()
        {
            applicationId = _appId,
            loginId = email,
            password = password,
        };
        var response = await _fusionAuthClient.LoginAsync(loginRequest);

        if (response.WasSuccessful())
        {
            return Result.Ok(response.successResponse.token);
        }

        return response.statusCode switch
        {
            401 => Result.Fail(new AuthErrors.General.InternalAuthError()),
            404 => Result.Fail(new AuthErrors.Login.InvalidEmailOrPassword()),
            _ => Result.Fail(new AuthErrors.General.UnspecifiedAuthError())
        };
    }

    /// <inheritdoc />
    public async Task<Result<string>> RegisterAsync(string email, string password, string firstName, string lastName)
    {
        var newUser = new User()
        {
            active = true,
            email = email,
            password = password,
            firstName = firstName,
            lastName = lastName
        };
        var userRegistration = new UserRegistration()
        {
            applicationId = _appId,
            verified = true
        };
        var registration = new RegistrationRequest()
        {
            registration = userRegistration,
            skipVerification = true,
            user = newUser
        };

        var response = await _fusionAuthClient.RegisterAsync(null, registration);

        if (response.WasSuccessful())
        {
            return Result.Ok(response.successResponse.token);
        }

        if (response.statusCode != 400) return Result.Fail(new AuthErrors.General.UnspecifiedAuthError());
        return response.errorResponse.fieldErrors.Any(err => err.Value.Any(e => e.code.Contains("email")))
            ? Result.Fail(new AuthErrors.Register.EmailAlreadyExists())
            : Result.Fail(new AuthErrors.General.UnspecifiedAuthError());
    }
}