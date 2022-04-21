using DMS.DAL.EntityModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DMS.DAL.DatabaseContext
{
    public class IdentityEntities : IdentityDbContext<ApplicationUser>
    {
        public IdentityEntities() : base("IdentityConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<IdentityEntities, Migrations.Configuration>());
        }

        public static IdentityEntities Create() => new IdentityEntities();

        public DbSet<ControllerAction> ControllerActions { get; set; }
        public DbSet<ActionRole> ActionRoles { get; set; }
        public DbSet<MenuList> MenuLists { get; set; }
        //public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<GlobalErrorLog> GlobalErrorLogs { get; set; }
        public DbSet<AspNetRoleMenuItem> AspNetRoleMenuItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<MenuList>()
            //    .HasOptional(x => x.ControllerAction)
            //    //.WithMany(x => x.MenuLists)
            //    .HasForeignKey(x => x.ControllerActionId)
            //    .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}