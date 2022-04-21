namespace DMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_required_tables1 : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE TABLE [dbo].[bra01branches](
	[bra01uin] [int] IDENTITY(1,1) NOT NULL,
	[bra01name] [nvarchar](255) NOT NULL,
	[bra01address] [nvarchar](255) NOT NULL,
	[bra01telephone] [nvarchar](255) NULL,
	[bra01status] [bit] NOT NULL,
	[bra01deleted] [bit] NOT NULL,
	[bra01code] [nvarchar](20) NOT NULL,
	[bra01created_by] [varchar](255) NOT NULL,
	[bra01created_name] [varchar](255) NOT NULL,
	[bra01created_date_eng] [datetime] NOT NULL,
	[bra01created_date_nep] [char](10) NOT NULL,
	[bra01updated_by] [varchar](255) NOT NULL,
	[bra01updated_date_eng] [datetime] NOT NULL,
	[bra01updated_date_nep] [char](10) NOT NULL,
 CONSTRAINT [PK_bra01branche] PRIMARY KEY CLUSTERED 
(
	[bra01uin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"CREATE TABLE [dbo].[cfg01configurations](
	[cfg01uin] [varchar](255) NOT NULL,
	[cfg01module] [varchar](255) NOT NULL,
	[cfg01key] [varchar](255) NOT NULL,
	[cfg01value] [nvarchar](255) NULL,
	[cfg01created_date] [datetime] NOT NULL,
	[cfg01created_name] [varchar](255) NOT NULL,
	[cfg01updated_name] [varchar](255) NOT NULL,
	[cfg01updated_date] [datetime] NOT NULL,
 CONSTRAINT [PK_cfg01configurations] PRIMARY KEY CLUSTERED 
(
	[cfg01module] ASC,
	[cfg01key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"CREATE TABLE [dbo].[dep01department](
	[dep01uin] [int] IDENTITY(1,1) NOT NULL,
	[dep01title] [nvarchar](255) NOT NULL,
	[dep01code] [nvarchar](50) NOT NULL,
	[dep01status] [bit] NOT NULL,
	[dep01deleted] [bit] NOT NULL,
	[dep01updated_by] [varchar](255) NOT NULL,
	[dep01updated_date_eng] [datetime] NOT NULL,
	[dep01updated_date_nep] [char](10) NOT NULL,
	[dep01created_by] [varchar](255) NOT NULL,
	[dep01created_date_eng] [datetime] NOT NULL,
	[dep01created_date_nep] [char](10) NOT NULL,
 CONSTRAINT [PK_dep01department] PRIMARY KEY CLUSTERED 
(
	[dep01uin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"CREATE TABLE [dbo].[des01designations](
	[des01uin] [int] IDENTITY(1,1) NOT NULL,
	[des01title] [nvarchar](255) NOT NULL,
	[des01description] [nvarchar](255) NULL,
	[des01status] [bit] NOT NULL,
	[des01deleted] [bit] NOT NULL,
	[des01created_by] [varchar](255) NOT NULL,
	[des01created_date_nep] [char](10) NOT NULL,
	[des01created_date_eng] [datetime] NOT NULL,
	[des01updated_by] [varchar](255) NOT NULL,
	[des01updated_date_nep] [char](10) NOT NULL,
	[des01updated_date_eng] [datetime] NOT NULL,
	[des01code] [varchar](25) NOT NULL,
 CONSTRAINT [PK_des01designations] PRIMARY KEY CLUSTERED 
(
	[des01uin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"CREATE TABLE [dbo].[emp01employee](
	[emp01uin] [int] IDENTITY(1,1) NOT NULL,
	[emp01code] [nvarchar](50) NOT NULL,
	[emp01des01uin] [int] NOT NULL,
	[emp01dep01uin] [int] NOT NULL,
	[emp01lvl01uin] [int] NOT NULL,
	[emp01bra01uin] [int] NOT NULL,
	[emp01name] [nvarchar](255) NOT NULL,
	[emp01join_date_nep] [char](10) NOT NULL,
	[emp01join_date_eng] [datetime] NOT NULL,
	[emp01address] [nvarchar](255) NOT NULL,
	[emp01email] [nvarchar](255) NOT NULL,
	[emp01mobile] [char](10) NOT NULL,
	[emp01status] [bit] NOT NULL,
	[emp01deleted] [bit] NOT NULL,
	[emp01created_by] [nvarchar](255) NOT NULL,
	[emp01created_date_eng] [datetime] NOT NULL,
	[emp01created_date_nep] [char](10) NOT NULL,
	[emp01updated_by] [nvarchar](255) NOT NULL,
	[emp01updated_date_nep] [char](10) NOT NULL,
	[emp01update_date_eng] [datetime] NOT NULL,
 CONSTRAINT [PK_emp01employee] PRIMARY KEY CLUSTERED 
(
	[emp01uin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"CREATE TABLE [dbo].[lvl01employee_levels](
	[lvl01uin] [int] IDENTITY(1,1) NOT NULL,
	[lvl01title] [nvarchar](255) NOT NULL,
	[lvl01description] [nvarchar](255) NULL,
	[lvl01code] [varchar](50) NOT NULL,
	[lvl01status] [bit] NOT NULL,
	[lvl01deleted] [bit] NOT NULL,
	[lvl01created_by] [varchar](255) NOT NULL,
	[lvl01created_date_nep] [char](10) NOT NULL,
	[lvl01created_date_eng] [datetime] NOT NULL,
	[lvl01updated_by] [varchar](255) NOT NULL,
	[lvl01updated_date_nep] [char](10) NOT NULL,
	[lvl01updated_date_eng] [datetime] NOT NULL,
 CONSTRAINT [PK_lvl01employee_levels] PRIMARY KEY CLUSTERED 
(
	[lvl01uin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"CREATE TABLE [dbo].[UserCodes](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [nvarchar](128) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[requested_date] [datetime] NOT NULL,
	[valid_till_date] [datetime] NOT NULL,
	[ip] [nvarchar](20) NULL,
	[code] [bigint] NOT NULL,
	[CodeUsed] [bit] NOT NULL,
 CONSTRAINT [PK_UserCodes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"CREATE TABLE [dbo].[usr05users](
	[usr05userId] [nvarchar](128) NOT NULL,
	[usr05bra01uin] [int] NOT NULL,
	[usr05type] [tinyint] NOT NULL,
	[usr05status] [bit] NOT NULL,
	[usr05deleted] [bit] NOT NULL,
	[usr05created_date] [datetime] NOT NULL,
	[usr05created_by] [nvarchar](255) NOT NULL,
	[usr05updated_date] [datetime] NOT NULL,
	[usr05updated_by] [nvarchar](255) NOT NULL,
	[usr05emp01uin] [int] NULL,
	[usr05can_view_all_branch] [bit] NOT NULL,
 CONSTRAINT [PK_usr05users] PRIMARY KEY CLUSTERED 
(
	[usr05userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]");
            Sql(@"ALTER TABLE [dbo].[dep01department] ADD  CONSTRAINT [DF_dep01department_dep01status]  DEFAULT ((1)) FOR [dep01status];
ALTER TABLE [dbo].[dep01department] ADD  CONSTRAINT [DF__dep01depa__dep01__0E04126B]  DEFAULT ((0)) FOR [dep01deleted];
ALTER TABLE [dbo].[emp01employee] ADD  CONSTRAINT [DF_emp01employee_emp01code]  DEFAULT ((0)) FOR [emp01code];
ALTER TABLE [dbo].[emp01employee] ADD  CONSTRAINT [DF__emp01empl__emp01__0EF836A4]  DEFAULT ((0)) FOR [emp01deleted];
ALTER TABLE [dbo].[emp01employee]  WITH CHECK ADD  CONSTRAINT [FK_emp01employee_bra01branches] FOREIGN KEY([emp01bra01uin])
REFERENCES [dbo].[bra01branches] ([bra01uin]);
ALTER TABLE [dbo].[emp01employee] CHECK CONSTRAINT [FK_emp01employee_bra01branches];
ALTER TABLE [dbo].[emp01employee]  WITH CHECK ADD  CONSTRAINT [FK_emp01employee_dep01department] FOREIGN KEY([emp01dep01uin])
REFERENCES [dbo].[dep01department] ([dep01uin]);
ALTER TABLE [dbo].[emp01employee] CHECK CONSTRAINT [FK_emp01employee_dep01department];
ALTER TABLE [dbo].[emp01employee]  WITH CHECK ADD  CONSTRAINT [FK_emp01employee_des01designations] FOREIGN KEY([emp01des01uin])
REFERENCES [dbo].[des01designations] ([des01uin]);
ALTER TABLE [dbo].[emp01employee] CHECK CONSTRAINT [FK_emp01employee_des01designations];
ALTER TABLE [dbo].[emp01employee]  WITH CHECK ADD  CONSTRAINT [FK_emp01employee_lvl01employee_levels] FOREIGN KEY([emp01lvl01uin])
REFERENCES [dbo].[lvl01employee_levels] ([lvl01uin]);
ALTER TABLE [dbo].[emp01employee] CHECK CONSTRAINT [FK_emp01employee_lvl01employee_levels];
ALTER TABLE [dbo].[usr05users]  WITH CHECK ADD  CONSTRAINT [FK_usr05users_bra01branches] FOREIGN KEY([usr05bra01uin])
REFERENCES [dbo].[bra01branches] ([bra01uin]);
ALTER TABLE [dbo].[usr05users] CHECK CONSTRAINT [FK_usr05users_bra01branches];
ALTER TABLE [dbo].[usr05users]  WITH CHECK ADD  CONSTRAINT [FK_usr05users_emp01employee] FOREIGN KEY([usr05emp01uin])
REFERENCES [dbo].[emp01employee] ([emp01uin]);
ALTER TABLE [dbo].[usr05users] CHECK CONSTRAINT [FK_usr05users_emp01employee];
");
        }
        
        public override void Down()
        {
        }
    }
}
