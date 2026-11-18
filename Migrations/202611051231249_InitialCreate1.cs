namespace UniversityProject_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "CampusID");
            RenameColumn(table: "dbo.Students", name: "UniversityCampus_ID", newName: "CampusID");
            RenameIndex(table: "dbo.Students", name: "IX_UniversityCampus_ID", newName: "IX_CampusID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Students", name: "IX_CampusID", newName: "IX_UniversityCampus_ID");
            RenameColumn(table: "dbo.Students", name: "CampusID", newName: "UniversityCampus_ID");
            AddColumn("dbo.Students", "CampusID", c => c.Int());
        }
    }
}
