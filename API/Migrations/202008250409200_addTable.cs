namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_m_division",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_m_department", t => t.department_Id, cascadeDelete: true)
                .Index(t => t.department_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_division", "department_Id", "dbo.tb_m_department");
            DropIndex("dbo.tb_m_division", new[] { "department_Id" });
            DropTable("dbo.tb_m_division");
            DropTable("dbo.tb_m_department");
        }
    }
}
