namespace tryEFonce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _ini : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        OrganizerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Organizers", t => t.OrganizerId, cascadeDelete: true)
                .Index(t => t.OrganizerId);
            
            CreateTable(
                "dbo.Organizers",
                c => new
                    {
                        OrganizerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OrganizerId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EvenTime = c.DateTime(nullable: false),
                        OrganizerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Organizers", t => t.OrganizerId, cascadeDelete: true)
                .Index(t => t.OrganizerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "OrganizerId", "dbo.Organizers");
            DropForeignKey("dbo.Events", "OrganizerId", "dbo.Organizers");
            DropForeignKey("dbo.Customers", "EventId", "dbo.Events");
            DropIndex("dbo.Customers", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "OrganizerId" });
            DropIndex("dbo.Contacts", new[] { "OrganizerId" });
            DropTable("dbo.Customers");
            DropTable("dbo.Events");
            DropTable("dbo.Organizers");
            DropTable("dbo.Contacts");
        }
    }
}
