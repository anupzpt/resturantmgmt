namespace DMS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_required_cfg01_values : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT [dbo].[cfg01configurations] ([cfg01uin], [cfg01module], [cfg01key], [cfg01value], [cfg01created_date], [cfg01created_name], [cfg01updated_name], [cfg01updated_date]) VALUES (N'a32e18e1-ee35-4c47-a666-01eb66c063ab', N'Mail', N'SMTPServer', N'gmail', CAST(N'2020-08-25T22:26:27.007' AS DateTime), N'anish.thakuri@kamanasewabank.com', N'anish.thakuri@kamanasewabank.com', CAST(N'2022-04-05T17:37:07.350' AS DateTime));
INSERT [dbo].[cfg01configurations] ([cfg01uin], [cfg01module], [cfg01key], [cfg01value], [cfg01created_date], [cfg01created_name], [cfg01updated_name], [cfg01updated_date]) VALUES (N'026c6254-545a-484f-a664-2e40aeaa4587', N'Mail', N'EmailNotification', N'True', CAST(N'2020-10-12T00:00:00.000' AS DateTime), N'SuperAdmin', N'anish.thakuri@kamanasewabank.com', CAST(N'2022-04-05T17:37:07.460' AS DateTime));
INSERT [dbo].[cfg01configurations] ([cfg01uin], [cfg01module], [cfg01key], [cfg01value], [cfg01created_date], [cfg01created_name], [cfg01updated_name], [cfg01updated_date]) VALUES (N'85fa2265-f732-451b-9c3d-db786a4503c5', N'Mail', N'SMTPFrom', N'md.faijan0@gmail.com', CAST(N'2020-08-25T22:26:27.107' AS DateTime), N'anish.thakuri@kamanasewabank.com', N'anish.thakuri@kamanasewabank.com', CAST(N'2022-04-05T17:37:07.430' AS DateTime));
INSERT [dbo].[cfg01configurations] ([cfg01uin], [cfg01module], [cfg01key], [cfg01value], [cfg01created_date], [cfg01created_name], [cfg01updated_name], [cfg01updated_date]) VALUES (N'58dc96a4-aa65-4062-99f6-7466d7426386', N'Mail', N'SMTPPassword', N'4iQh48K4i2kxKkFo/rZwDzbqFoW1f3cFNAsrPYhblMg=', CAST(N'2020-08-25T22:26:27.060' AS DateTime), N'anish.thakuri@kamanasewabank.com', N'anish.thakuri@kamanasewabank.com', CAST(N'2020-12-06T21:53:20.880' AS DateTime));
INSERT [dbo].[cfg01configurations] ([cfg01uin], [cfg01module], [cfg01key], [cfg01value], [cfg01created_date], [cfg01created_name], [cfg01updated_name], [cfg01updated_date]) VALUES (N'b5c13274-40ca-49d3-9e06-9f7623f79b9d', N'Mail', N'SMTPPort', N'856', CAST(N'2020-08-25T22:26:27.077' AS DateTime), N'anish.thakuri@kamanasewabank.com', N'anish.thakuri@kamanasewabank.com', CAST(N'2022-04-05T17:37:07.400' AS DateTime));
INSERT [dbo].[cfg01configurations] ([cfg01uin], [cfg01module], [cfg01key], [cfg01value], [cfg01created_date], [cfg01created_name], [cfg01updated_name], [cfg01updated_date]) VALUES (N'ca89c609-95a6-4b25-afab-f804cfcf5665', N'Mail', N'SMTPSSL', N'True', CAST(N'2020-08-25T22:26:27.090' AS DateTime), N'anish.thakuri@kamanasewabank.com', N'anish.thakuri@kamanasewabank.com', CAST(N'2022-04-05T17:37:07.413' AS DateTime));
INSERT [dbo].[cfg01configurations] ([cfg01uin], [cfg01module], [cfg01key], [cfg01value], [cfg01created_date], [cfg01created_name], [cfg01updated_name], [cfg01updated_date]) VALUES (N'026c6254-545a-484f-a664-2e40aeaa0247', N'Mail', N'SMTPUser', N'md.faijan10@gmail.com', CAST(N'2020-08-25T22:26:27.040' AS DateTime), N'anish.thakuri@kamanasewabank.com', N'anish.thakuri@kamanasewabank.com', CAST(N'2022-04-05T17:37:07.373' AS DateTime));");
        }

        public override void Down()
        {
        }
    }
}
