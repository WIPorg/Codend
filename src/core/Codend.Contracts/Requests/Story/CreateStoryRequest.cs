﻿namespace Codend.Contracts.Requests.Story;

/// <summary>
/// Represents create story request.
/// </summary>
/// <param name="Name">Name of the story.</param>
/// <param name="Description">Description of the story.</param>
public record CreateStoryRequest(
    string Name,
    string Description,
    Guid? EpicId
);