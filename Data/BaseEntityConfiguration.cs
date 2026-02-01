using ConferenceRoom.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoom.Api.Data
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            // Global query filter for soft delete
            builder.HasQueryFilter(e => !e.Deleted);
        }
    }
}
