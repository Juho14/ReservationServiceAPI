using ConferenceRoom.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoom.Api.Data
{


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }


        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RoomEntity> Rooms => Set<RoomEntity>();
        public DbSet<ReservationEntity> Reservations => Set<ReservationEntity>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ReservationEntity>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<ReservationEntity>()
            .HasOne(r => r.Room)
            .WithMany(rm => rm.Reservations)
            .HasForeignKey(r => r.RoomId);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(AppDbContext)
                        .GetMethod(nameof(ApplySoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { modelBuilder });
                }
            }
        }

        // Strongly-typed method for filter
        private static void ApplySoftDeleteFilter<TEntity>(ModelBuilder builder)
            where TEntity : BaseEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.Deleted);
        }
    }
}
