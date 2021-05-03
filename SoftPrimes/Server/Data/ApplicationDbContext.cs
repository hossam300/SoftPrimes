using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPrimes.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<Agent>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<CheckPoint> CheckPoints { get; set; }
        public virtual DbSet<CheckPointTourComment> CheckPointTourComments { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourCheckPoint> TourCheckPoints { get; set; }
        public virtual DbSet<TourComment> TourComments { get; set; }
        public virtual DbSet<TourStateLog> TourStateLogs { get; set; }
        public virtual DbSet<AgentLocationLog> AgentLocationLogs { get; set; }
        //public virtual DbSet<Attachment> Attachments { get; set; }
        //public virtual DbSet<Attachment> Attachments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Agent>().ToTable("Agents", "dbo");
        }
    }
}
