﻿using Codend.Domain.Core.Abstractions;
using Codend.Domain.Entities;

namespace Codend.Domain.Core.Events;

/// <summary>
/// Domain event raised after new sprint has been created.
/// </summary>
public class ProjectTaskRemovedFromSprintEvent : IDomainEvent
{
    public ProjectTaskRemovedFromSprintEvent(Sprint sprint, ProjectTask projectTask)
    {
        Sprint = sprint;
        ProjectTask = projectTask;
    }

    public Sprint Sprint { get; set; }
    public ProjectTask ProjectTask { get; set; }
}