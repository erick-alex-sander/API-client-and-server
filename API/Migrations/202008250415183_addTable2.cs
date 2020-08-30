namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tb_m_division", new[] { "department_Id" });
            CreateIndex("dbo.tb_m_division", "Department_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tb_m_division", new[] { "Department_Id" });
            CreateIndex("dbo.tb_m_division", "department_Id");
        }
    }
}
