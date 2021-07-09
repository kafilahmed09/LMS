using IdentityServer4.EntityFramework.Options;
using LMS.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Shared.Models;

namespace LMS.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<LMS.Shared.Models.District> Districts { get; set; }
        public DbSet<LMS.Shared.Models.Tehsil> Tehsils { get; set; }
        public DbSet<LMS.Shared.Models.UnionCouncil> UnionCouncils { get; set; }
        public DbSet<LMS.Shared.Models.School> Schools { get; set; }
        public DbSet<LMS.Shared.Models.SchoolLevel> SchoolLevels { get; set; }
        public DbSet<LMS.Shared.Models.Gander> Ganders { get; set; }
        public DbSet<LMS.Shared.Models.Grade> Grades { get; set; }
        public DbSet<LMS.Shared.Models.Subject> Subjects { get; set; }
        public DbSet<LMS.Shared.Models.Chapter> Chapters { get; set; }
        public DbSet<LMS.Shared.Models.Video> Videos { get; set; }                
        public DbSet<LMS.Shared.Models.Topic> Topic { get; set; }
    }
}
