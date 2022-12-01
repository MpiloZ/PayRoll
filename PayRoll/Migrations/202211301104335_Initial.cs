namespace PayRoll.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "BusinessAddressID", "dbo.Addresses");
            DropForeignKey("dbo.Employees", "HomeAddressID", "dbo.Addresses");
            DropIndex("dbo.Companies", new[] { "BusinessAddressID" });
            DropColumn("dbo.Addresses", "ID");
            RenameColumn(table: "dbo.Addresses", name: "BusinessAddressID", newName: "ID");
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Companies", "BusinessAddressID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Addresses", "ID");
            CreateIndex("dbo.Addresses", "ID");
            AddForeignKey("dbo.Employees", "HomeAddressID", "dbo.Addresses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "HomeAddressID", "dbo.Addresses");
            DropIndex("dbo.Addresses", new[] { "ID" });
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Companies", "BusinessAddressID", c => c.Int());
            AlterColumn("dbo.Addresses", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Addresses", "ID");
            RenameColumn(table: "dbo.Addresses", name: "ID", newName: "BusinessAddressID");
            AddColumn("dbo.Addresses", "ID", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Companies", "BusinessAddressID");
            AddForeignKey("dbo.Employees", "HomeAddressID", "dbo.Addresses", "ID");
            AddForeignKey("dbo.Companies", "BusinessAddressID", "dbo.Addresses", "ID");
        }
    }
}
