namespace DMS.DAL.Migrations
{
    using DMS.DAL.DatabaseContext;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DMS.DAL.DatabaseContext.IdentityEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(IdentityEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //use db migrator to generate pending code based migrations and apply it explicitly 
            string script = "";
            string cPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Private", "Migrations");
            if (!System.IO.Directory.Exists(cPath)) { System.IO.Directory.CreateDirectory(cPath); }
            try
            {
                var configuration = new Configuration();
                var migrator = new DbMigrator(configuration);
                IList<string> AppliedMigrations = migrator.GetDatabaseMigrations().ToList();
                IList<string> PendingMigrations = migrator.GetPendingMigrations().ToList();
                MigratorScriptingDecorator scripter = new MigratorScriptingDecorator(migrator);
                script = scripter.ScriptUpdate(null, null);

                string fPath = System.IO.Path.Combine(cPath, $"Migration_{DateTime.Now.ToString("yyyyMMdd")}.txt");

                if (script != null && script.Length > 0)
                    System.IO.File.AppendAllText(fPath, script);
                migrator.Update();
            }
            catch (Exception ex)
            {
                string fPath = System.IO.Path.Combine(cPath, $"Migration_error_{DateTime.Now.ToString("yyyyMMdd")}.txt");
                string msg = "Script: " + script + " Error: " + (ex.InnerException != null ? ex.InnerException.Message : ex.Message) + ex.StackTrace;
                System.IO.File.AppendAllText(fPath, msg);
            }
        }
    }
}
