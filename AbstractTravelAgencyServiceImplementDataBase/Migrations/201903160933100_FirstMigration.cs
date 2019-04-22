namespace AbstractTravelAgencyServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        TotalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatusBooking = c.Int(nullable: false),
                        DateCreateBooking = c.DateTime(nullable: false),
                        DateImplementBooking = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.VoucherId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherName = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CityConditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        ConditionId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConditionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VoucherConditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(nullable: false),
                        ConditionId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Bookings", new[] { "VoucherId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropTable("dbo.VoucherConditions");
            DropTable("dbo.Conditions");
            DropTable("dbo.CityConditions");
            DropTable("dbo.Cities");
            DropTable("dbo.Vouchers");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
