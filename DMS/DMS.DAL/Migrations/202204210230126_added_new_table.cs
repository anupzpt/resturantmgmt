namespace DMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_new_table : DbMigration
    {
        public override void Up()
        {
            Sql(@"Create table test(
                id int primary key identity(1,1),
                name nvarchar(255) NOT NULL,
                address nvarchar(255) NOT NULL,
                contact nvarchar(10) NOT NULL
            );");
        }
        
        public override void Down()
        {
        }
    }
}
