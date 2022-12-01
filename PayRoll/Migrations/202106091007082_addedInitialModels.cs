namespace PayRoll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedInitialModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        City = c.String(),
                        Street = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BusinessAddressID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.BusinessAddressID)
                .Index(t => t.BusinessAddressID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Lastname = c.String(),
                        Position = c.String(),
                        HomeAddressID = c.Int(),
                        CompanyID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .ForeignKey("dbo.Addresses", t => t.HomeAddressID)
                .Index(t => t.HomeAddressID)
                .Index(t => t.CompanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "HomeAddressID", "dbo.Addresses");
            DropForeignKey("dbo.Employees", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Companies", "BusinessAddressID", "dbo.Addresses");
            DropIndex("dbo.Employees", new[] { "CompanyID" });
            DropIndex("dbo.Employees", new[] { "HomeAddressID" });
            DropIndex("dbo.Companies", new[] { "BusinessAddressID" });
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
            DropTable("dbo.Addresses");
        }
    }
}
