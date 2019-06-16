namespace AbstractTravelAgencyServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        VoucherName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conditions", t => t.ConditionId, cascadeDelete: true)
                .ForeignKey("dbo.Vouchers", t => t.VoucherId, cascadeDelete: true)
                .Index(t => t.VoucherId)
                .Index(t => t.ConditionId);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConditionName = c.String(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Conditions", t => t.ConditionId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.ConditionId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoucherConditions", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.VoucherConditions", "ConditionId", "dbo.Conditions");
            DropForeignKey("dbo.CityConditions", "ConditionId", "dbo.Conditions");
            DropForeignKey("dbo.CityConditions", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Bookings", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CityConditions", new[] { "ConditionId" });
            DropIndex("dbo.CityConditions", new[] { "CityId" });
            DropIndex("dbo.VoucherConditions", new[] { "ConditionId" });
            DropIndex("dbo.VoucherConditions", new[] { "VoucherId" });
            DropIndex("dbo.Bookings", new[] { "VoucherId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropTable("dbo.Cities");
            DropTable("dbo.CityConditions");
            DropTable("dbo.Conditions");
            DropTable("dbo.VoucherConditions");
            DropTable("dbo.Vouchers");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
