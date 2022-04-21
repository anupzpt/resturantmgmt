namespace DMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_required_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ActionId = c.Int(nullable: false),
                        RoleId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedNepaliDate = c.String(),
                        CreatedEnglishDate = c.String(),
                        UpdatedNepaliDate = c.String(),
                        UpdatedEnglishDate = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.ControllerAction", t => t.ActionId, cascadeDelete: true)
                .Index(t => t.ActionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                        CreatedNepaliDate = c.String(),
                        CreatedEnglishDate = c.String(),
                        UpdatedNepaliDate = c.String(),
                        UpdatedEnglishDate = c.String(),
                        UpdatedBy = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ControllerAction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        ActiveAllTime = c.Boolean(nullable: false),
                        Attributes = c.String(),
                        ReturnType = c.String(),
                        CreatedNepaliDate = c.String(),
                        CreatedEnglishDate = c.String(),
                        UpdatedNepaliDate = c.String(),
                        UpdatedEnglishDate = c.String(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ControllerActionId = c.Int(),
                        ParentId = c.Int(),
                        DropDownName = c.String(),
                        Position = c.Int(nullable: false),
                        IconName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedNepaliDate = c.String(),
                        CreatedEnglishDate = c.String(),
                        UpdatedNepaliDate = c.String(),
                        UpdatedEnglishDate = c.String(),
                        UpdatedBy = c.String(),
                        MenuType = c.Byte(),
                        Area_id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ControllerAction", t => t.ControllerActionId)
                .Index(t => t.ControllerActionId);
            
            CreateTable(
                "dbo.AspNetRoleMenuItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        MenuListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuList", t => t.MenuListId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.MenuListId);
            
            CreateTable(
                "dbo.GlobalErrorLog",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Error = c.String(),
                        Attribute = c.String(),
                        EnglishDate = c.DateTime(nullable: false),
                        NepaliDate = c.String(),
                        ClientIpAddress = c.String(),
                        ClientName = c.String(),
                        UserName = c.String(),
                        UserId = c.Int(nullable: false),
                        Resolved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 256),
                        UserName = c.String(nullable: false, maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedNepaliDate = c.String(),
                        CreatedEnglishDate = c.String(),
                        UpdatedNepaliDate = c.String(),
                        UpdatedEnglishDate = c.String(),
                        UpdatedBy = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRoleMenuItem", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRoleMenuItem", "MenuListId", "dbo.MenuList");
            DropForeignKey("dbo.MenuList", "ControllerActionId", "dbo.ControllerAction");
            DropForeignKey("dbo.ActionRole", "ActionId", "dbo.ControllerAction");
            DropForeignKey("dbo.ActionRole", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetRoleMenuItem", new[] { "MenuListId" });
            DropIndex("dbo.AspNetRoleMenuItem", new[] { "RoleId" });
            DropIndex("dbo.MenuList", new[] { "ControllerActionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ActionRole", new[] { "RoleId" });
            DropIndex("dbo.ActionRole", new[] { "ActionId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.GlobalErrorLog");
            DropTable("dbo.AspNetRoleMenuItem");
            DropTable("dbo.MenuList");
            DropTable("dbo.ControllerAction");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ActionRole");
        }
    }
}
