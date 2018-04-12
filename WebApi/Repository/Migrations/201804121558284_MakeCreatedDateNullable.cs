namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCreatedDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "CreatedDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
