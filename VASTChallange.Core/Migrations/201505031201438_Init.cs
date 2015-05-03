namespace VASTChallange.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 50),
                        Timestamp = c.DateTime(nullable: false),
                        From_Id = c.Int(),
                        To_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visitors", t => t.From_Id)
                .ForeignKey("dbo.Visitors", t => t.To_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Type = c.String(maxLength: 50),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Visitor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visitors", t => t.Visitor_Id)
                .Index(t => t.Visitor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movements", "Visitor_Id", "dbo.Visitors");
            DropForeignKey("dbo.CommDatas", "To_Id", "dbo.Visitors");
            DropForeignKey("dbo.CommDatas", "From_Id", "dbo.Visitors");
            DropIndex("dbo.Movements", new[] { "Visitor_Id" });
            DropIndex("dbo.CommDatas", new[] { "To_Id" });
            DropIndex("dbo.CommDatas", new[] { "From_Id" });
            DropTable("dbo.Movements");
            DropTable("dbo.Visitors");
            DropTable("dbo.CommDatas");
        }
    }
}
