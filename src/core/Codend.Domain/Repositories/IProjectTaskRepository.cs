using Codend.Domain.Entities;

namespace Codend.Domain.Repositories;

public interface IProjectTaskRepository
{
    void Add(BaseProjectTask task);

    Task<BaseProjectTask?> GetByIdAsync(ProjectTaskId taskId);

    void Remove(BaseProjectTask task);

    void Update(BaseProjectTask task);

    /// <summary>
    /// Updates range.
    /// </summary>
    /// <param name="tasks">Tasks to update.</param>
    void UpdateRange(IEnumerable<BaseProjectTask> tasks);

    /// <summary>
    /// Checks whether project exists and checks whether status is contained by given project.
    /// </summary>
    /// <returns>true id status is valid for this project, otherwise false</returns>
    bool ProjectTaskStatusIsValid(ProjectId projectId, ProjectTaskStatusId statusId);

    /// <summary>
    /// Collects tasks which belongs to a given story.
    /// </summary>
    /// <param name="storyId">StoryId.</param>
    /// <returns>List of tasks which belongs to the <see cref="Story"/>.</returns>
    Task<List<BaseProjectTask>> GetStoryTasks(StoryId storyId);

    /// <summary>
    /// Collects tasks which has given status.
    /// </summary>
    /// <param name="statusId">StatusId.</param>
    /// <returns>List of tasks which has given status.</returns>
    Task<List<BaseProjectTask>> GetTasksWithStatusId(ProjectTaskStatusId statusId);
}