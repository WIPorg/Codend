using Codend.Application.Core.Abstractions.Authentication;
using Codend.Application.Core.Abstractions.Data;
using Codend.Application.Core.Abstractions.Messaging.Commands;
using Codend.Application.ProjectTasks.Commands.UpdateProjectTask.Abstractions;
using Codend.Contracts.Requests;
using Codend.Domain.Entities;
using Codend.Domain.Repositories;

namespace Codend.Application.ProjectTasks.Commands.UpdateProjectTask;

/// <summary>
/// Record used for updating <see cref="BaseProjectTask"/> properties.
/// </summary>
public sealed record UpdateBaseProjectTaskCommand
(
    ShouldUpdateBinder<string> Name,
    ShouldUpdateBinder<string> Priority,
    ShouldUpdateBinder<ProjectTaskStatusId> StatusId,
    ShouldUpdateBinder<string?> Description,
    ShouldUpdateBinder<TimeSpan?> EstimatedTime,
    ShouldUpdateBinder<DateTime?> DueDate,
    ShouldUpdateBinder<uint?> StoryPoints,
    ShouldUpdateBinder<UserId?> AssigneeId,
    ShouldUpdateBinder<StoryId?> StoryId
) : ICommand, IUpdateProjectTaskCommand
{
    /// <summary>
    /// Id of the project task that will be updated.
    /// </summary>
    public ProjectTaskId TaskId { get; init; }
}

/// <summary>
/// Command handler for <see cref="UpdateBaseProjectTaskCommand"/>.
/// </summary>
public class UpdateAbstractProjectTaskCommandHandler :
    UpdateProjectTaskCommandAbstractHandler<UpdateBaseProjectTaskCommand, BaseProjectTask>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAbstractProjectTaskCommandHandler"/> class.
    /// </summary>
    public UpdateAbstractProjectTaskCommandHandler(
        IProjectTaskRepository taskRepository,
        IUnitOfWork unitOfWork,
        IProjectMemberRepository memberRepository,
        IProjectTaskStatusRepository projectTaskStatusRepository,
        IStoryRepository storyRepository,
        IUserIdentityProvider identityProvider)
        : base(taskRepository,
            unitOfWork,
            memberRepository,
            projectTaskStatusRepository,
            storyRepository,
            identityProvider)
    {
    }
}