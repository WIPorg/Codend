using Codend.Application.Core.Abstractions.Data;
using Codend.Contracts.Responses.Board;
using Codend.Domain.Core.Abstractions;
using Codend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Codend.Application.Extensions;

internal static class QueryableSetsExtensions
{
    internal static async Task<IEnumerable<BoardTaskResponse>> GetBoardTasksByProjectIdAsync(
        this IQueryableSets sets,
        ProjectId projectId,
        CancellationToken cancellationToken
    )
    {
        var sprints = sets
            .Queryable<Sprint>()
            .Where(sprint => sprint.ProjectId == projectId);

        var sprintTasks = await
            sets
            .Queryable<SprintProjectTask>()
            .Where(task => sprints.Any(s => s.Id == task.SprintId))
            .ToListAsync(cancellationToken);

        return await sets.GetBoardTasksBySprintTasksAsync(projectId, sprintTasks, cancellationToken);
    }

    private static async Task<IEnumerable<BoardTaskResponse>> GetBoardTasksBySprintTasksAsync(
        this IQueryableSets sets,
        ProjectId projectId,
        IEnumerable<SprintProjectTask> sprintTasks,
        CancellationToken cancellationToken
    )
    {
        var boardProjectTasks =
            await sets.GetBoardTasksBySprintTasksAsync<BaseProjectTask>(projectId, sprintTasks, cancellationToken);
        var boardStories =
            await sets.GetBoardTasksBySprintTasksAsync<Story>(projectId, sprintTasks, cancellationToken);
        var boardEpics = await sets.GetBoardTasksBySprintTasksAsync<Epic>(projectId, sprintTasks, cancellationToken);

        return boardProjectTasks.Union(boardStories).Union(boardEpics);
    }

    internal static async Task<IEnumerable<BoardTaskResponse>> GetBoardTasksBySprintTasksAsync<T>(
        this IQueryableSets sets,
        ProjectId projectId,
        IEnumerable<SprintProjectTask> sprintTasks,
        CancellationToken cancellationToken
    ) where T : class, ISprintTask, IEntity, IProjectOwnedEntity
    {
        // Fetches all entities before joining 💀💀💀
        var set = await sets
            .Queryable<T>()
            .Where(entity => entity.ProjectId == projectId)
            .ToListAsync(cancellationToken);

        return set
            .Join(
                sprintTasks,
                task => task.SprintTaskId,
                sprintTask => sprintTask.SprintTaskId,
                (task, sprintTask) => task.ToBoardTaskResponse(sprintTask)
            );
    }
}