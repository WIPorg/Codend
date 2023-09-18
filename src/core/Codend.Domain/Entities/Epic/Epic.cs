using Codend.Domain.Core.Abstractions;
using Codend.Domain.Core.Primitives;
using Codend.Domain.ValueObjects;
using FluentResults;

namespace Codend.Domain.Entities;

public class Epic : Entity<EpicId>, ISoftDeletableEntity
{
    private Epic() : base(new EpicId(Guid.NewGuid()))
    {
    }

    #region ISoftDeletableEntityProperties

    public DateTime DeletedOnUtc { get; }
    public bool Deleted { get; }

    #endregion

    #region Epic properties

    /// <summary>
    /// ProjectId which story belongs to.
    /// </summary>
    public ProjectId ProjectId { get; private set; }

    /// <summary>
    /// Epic name.
    /// </summary>
    public EpicName Name { get; private set; }

    /// <summary>
    /// Epic description.
    /// </summary>
    public EpicDescription Description { get; private set; }

    #endregion

    #region Domain methods

    /// <summary>
    /// Epic creator.
    /// </summary>
    /// <param name="name">Epic name.</param>
    /// <param name="description">Epic description.</param>
    /// <param name="projectId">Epic project id.</param>
    /// <returns>Created <see cref="Epic"/> or <see cref="Result"/> with errors.</returns>
    public static Result<Epic> Create(string name, string description, ProjectId projectId)
    {
        var resultName = EpicName.Create(name);
        var resultDescription = EpicDescription.Create(description);

        var result = Result.Merge(resultName, resultDescription);
        if (result.IsFailed)
        {
            return result;
        }

        var epic = new Epic()
        {
            ProjectId = projectId,
            Name = resultName.Value,
            Description = resultDescription.Value
        };

        return Result.Ok(epic);
    }

    /// <summary>
    /// Edits epic name.
    /// </summary>
    /// <param name="name">New name.</param>
    /// <returns>Ok <see cref="Result"/> with new epic name or failure with errors.</returns>
    public virtual Result<EpicName> EditName(string name)
    {
        var resultName = EpicName.Create(name);
        if (resultName.IsFailed)
        {
            return resultName;
        }

        Name = resultName.Value;
        return resultName;
    }

    /// <summary>
    /// Edit epic description.
    /// </summary>
    /// <param name="description">New description.</param>
    /// <returns>Ok <see cref="Result"/> with new epic description or failure with errors.</returns>
    public virtual Result<EpicDescription> EditDescription(string description)
    {
        var resultDescription = EpicDescription.Create(description);
        if (resultDescription.IsFailed)
        {
            return resultDescription;
        }

        Description = resultDescription.Value;
        return resultDescription;
    }

    #endregion
}