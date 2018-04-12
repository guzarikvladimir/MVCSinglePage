namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDateToImageMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "CreatedDate", i => i.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "CreatedDate");
        }
    }
}
