﻿using Codend.Domain.Core.Errors;
using Codend.Domain.Core.Extensions;
using Codend.Domain.ValueObjects.Primitives;
using FluentResults;
using DescriptionTooLong = Codend.Domain.Core.Errors.DomainErrors.ProjectTaskDescription.DescriptionTooLong;

namespace Codend.Domain.ValueObjects;

/// <summary>
/// [Optional] ProjectTask description value object.
/// </summary>
public sealed class ProjectTaskDescription : NullableStringValueObject
{
    /// <summary>
    /// Maximum description length.
    /// </summary>
    public const int MaxLength = 2000;

    private ProjectTaskDescription(string? value) : base(value)
    {
    }

    /// <summary>
    /// Creates new <see cref="ProjectTaskDescription"/> value object with given <paramref name="value"/> string.
    /// Additionally checks whether the maximum length is exceeded.
    /// </summary>
    /// <param name="value">Value for the new <see cref="ProjectTaskDescription"/>.</param>
    /// <returns>The <see cref="Result"/> of creation. Contains <see cref="ProjectTaskDescription"/> or an <see cref="DomainErrors.DomainError"/>.</returns>
    public static Result<ProjectTaskDescription> Create(string? value)
    {
        return Result
            .Ok(new ProjectTaskDescription(value))
            .Ensure<ProjectTaskDescription, DescriptionTooLong>(() => value is null || value.Length < MaxLength);
    }
}