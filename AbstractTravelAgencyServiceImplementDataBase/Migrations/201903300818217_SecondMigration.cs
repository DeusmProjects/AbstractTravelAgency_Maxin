namespace AbstractTravelAgencyServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vouchers", "VoucherName", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "CityName", c => c.String(nullable: false));
            AlterColumn("dbo.Conditions", "ConditionName", c => c.String(nullable: false));
            CreateIndex("dbo.VoucherConditions", "VoucherId");
            CreateIndex("dbo.VoucherConditions", "ConditionId");
            CreateIndex("dbo.CityConditions", "CityId");
            CreateIndex("dbo.CityConditions", "ConditionId");
            AddForeignKey("dbo.CityConditions", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CityConditions", "ConditionId", "dbo.Conditions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VoucherConditions", "ConditionId", "dbo.Conditions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VoucherConditions", "VoucherId", "dbo.Vouchers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoucherConditions", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.VoucherConditions", "ConditionId", "dbo.Conditions");
            DropForeignKey("dbo.CityConditions", "ConditionId", "dbo.Conditions");
            DropForeignKey("dbo.CityConditions", "CityId", "dbo.Cities");
            DropIndex("dbo.CityConditions", new[] { "ConditionId" });
            DropIndex("dbo.CityConditions", new[] { "CityId" });
            DropIndex("dbo.VoucherConditions", new[] { "ConditionId" });
            DropIndex("dbo.VoucherConditions", new[] { "VoucherId" });
            AlterColumn("dbo.Conditions", "ConditionName", c => c.String());
            AlterColumn("dbo.Cities", "CityName", c => c.String());
            AlterColumn("dbo.Vouchers", "VoucherName", c => c.String());
        }
    }
}
