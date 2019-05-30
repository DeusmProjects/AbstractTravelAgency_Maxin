namespace AbstractTravelAgencyServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecutorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Bookings", "ExecutorId", c => c.Int());
            CreateIndex("dbo.Bookings", "ExecutorId");
            AddForeignKey("dbo.Bookings", "ExecutorId", "dbo.Executors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.Bookings", new[] { "ExecutorId" });
            DropColumn("dbo.Bookings", "ExecutorId");
            DropTable("dbo.Executors");
        }
    }
}
