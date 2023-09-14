using Codend.Application.Core.Abstractions.Authentication;
using Codend.Application.Core.Abstractions.Data;
using Codend.Application.Core.Abstractions.Messaging.Commands;
using Codend.Domain.Entities;
using Codend.Domain.Repositories;
using FluentResults;
using ProjectNotFound = Codend.Domain.Core.Errors.DomainErrors.ProjectErrors.ProjectNotFound;

namespace Codend.Application.Projects.Commands.DeleteProject;

/// <summary>
/// Command for deleting project with given id.
/// </summary>
/// <param name="ProjectId">Id of the project that will be deleted.</param>
public sealed record DeleteProjectCommand(
        Guid ProjectId)
    : ICommand;

/// <inheritdoc />
public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserIdentityProvider _identityProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectCommandHandler"/> class.
    /// </summary>
    public DeleteProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork,
        IUserIdentityProvider identityProvider)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _identityProvider = identityProvider;
        
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityProvider.UserId;
        var project = await _projectRepository.GetByIdAsync(new ProjectId(request.ProjectId));
        if (project is null)
        {
            return Result.Fail(new ProjectNotFound());
        }

        if (project.OwnerId != userId)
        {
            return Result.Fail(new ProjectNotFound());
        }

        _projectRepository.Remove(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}