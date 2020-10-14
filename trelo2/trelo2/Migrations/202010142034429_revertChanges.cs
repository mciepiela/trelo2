namespace trelo2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revertChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "CategoryOfTask_Id", "dbo.Categories");
            DropIndex("dbo.Tasks", new[] { "CategoryOfTask_Id" });
            DropColumn("dbo.Tasks", "Title");
            DropColumn("dbo.Tasks", "CategoryOfTask_Id");
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskCategory = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tasks", "CategoryOfTask_Id", c => c.Int());
            AddColumn("dbo.Tasks", "Title", c => c.String());
            CreateIndex("dbo.Tasks", "CategoryOfTask_Id");
            AddForeignKey("dbo.Tasks", "CategoryOfTask_Id", "dbo.Categories", "Id");
        }
    }
}
