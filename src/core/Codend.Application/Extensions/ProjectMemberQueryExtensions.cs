﻿using Codend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Codend.Application.Extensions;

internal static class ProjectMemberQueryExtensions
{
    /// <summary>
    /// Gets all projects that user is member of.
    /// </summary>
    internal static IQueryable<ProjectId> GetUserProjectsIds(this IQueryable<ProjectMember> query, UserId userId)
    {
        return query.Where(x => x.MemberId == userId).Select(x => x.ProjectId);
    }

    /// <summary>
    /// Gets all projects that user is member of.
    /// </summary>
    internal static Task<bool> IsUserMember(this IQueryable<ProjectMember> query, UserId userId, ProjectId projectId)
    {
        return query.AnyAsync(x => x.ProjectId == projectId && x.MemberId == userId);
    }
}