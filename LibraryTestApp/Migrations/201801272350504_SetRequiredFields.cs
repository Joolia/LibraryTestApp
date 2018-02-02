namespace LibraryTestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Authors", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Name", c => c.String());
            AlterColumn("dbo.Authors", "LastName", c => c.String());
            AlterColumn("dbo.Authors", "FirstName", c => c.String());
        }
    }
}
