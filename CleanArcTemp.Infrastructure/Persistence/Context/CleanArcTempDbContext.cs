using System.Reflection;
using CleanArcTemp.Application.Common.Interfaces;
using CleanArcTemp.Domain.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArcTemp.Infrastructure.Persistence.Context;

    public class CleanArcTempDbContext : ApiAuthorizationDbContext<User>, ICleanArcTempDbContext
{
        public CleanArcTempDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }


        #region Methodes

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public int SaveChangesAsync()
        {
            return base.SaveChanges();
        }


        #endregion



        #region Models

        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<Policy> Policy { get; set; } = null!;

        #endregion


    }


