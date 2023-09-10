﻿using Codend.Domain.Core.Abstractions;
using Codend.Domain.Entities;

namespace Codend.Domain.Core.Events;

/// <summary>
/// Domain event raised after deleting task from project.
/// </summary>
public class ProjectTaskDeletedFromProjectEvent : IDomainEvent
{
    public ProjectTaskDeletedFromProjectEvent(AbstractProjectTask projectTask, ProjectId projectId)
    {
        ProjectTask = projectTask;
        ProjectId = projectId;
    }

    public AbstractProjectTask ProjectTask { get; set; }
    public ProjectId ProjectId { get; set; }
}