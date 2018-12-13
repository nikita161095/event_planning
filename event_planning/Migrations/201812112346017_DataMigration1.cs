namespace event_planning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Events", "field_1", c => c.String());
            AddColumn("dbo.Events", "field_2", c => c.String());
            AddColumn("dbo.Events", "field_3", c => c.String());
            AddColumn("dbo.Events", "field_4", c => c.String());
            AddColumn("dbo.Events", "field_5", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "field_5");
            DropColumn("dbo.Events", "field_4");
            DropColumn("dbo.Events", "field_3");
            DropColumn("dbo.Events", "field_2");
            DropColumn("dbo.Events", "field_1");
            DropTable("dbo.Participants");
        }
    }
}
