using ConferenceRoom.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Api.Data
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Opt-out of the global soft-delete filter to include deleted entities.
        /// </summary>
        public static IQueryable<T> IncludeDeleted<T>(this IQueryable<T> query, bool includeDeleted)
        where T : BaseEntity
        {
            return includeDeleted ? query.IgnoreQueryFilters() : query;
        }
    }
}
