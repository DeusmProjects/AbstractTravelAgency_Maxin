namespace AbstractTravelAgencyServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class infoMesMigr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InfoMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InfoMessages", "CustomerId", "dbo.Customers");
            DropIndex("dbo.InfoMessages", new[] { "CustomerId" });
            DropTable("dbo.InfoMessages");
        }
    }
}
