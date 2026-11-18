namespace UniversityProject_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        CampusID = c.Int(),
                        UniversityCampus_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UniversityCampus", t => t.UniversityCampus_ID)
                .Index(t => t.UniversityCampus_ID);
            
            CreateTable(
                "dbo.UniversityCampus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "UniversityCampus_ID", "dbo.UniversityCampus");
            DropIndex("dbo.Students", new[] { "UniversityCampus_ID" });
            DropTable("dbo.UniversityCampus");
            DropTable("dbo.Students");
        }
    }
}
