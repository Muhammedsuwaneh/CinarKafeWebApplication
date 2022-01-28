namespace CinarKafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUrunCategories : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT UrunKategorisis ON");
            Sql("INSERT INTO dbo.UrunKategorisis (Id, KategoriAdi) VALUES(1, 'Ana Yemek');");
            Sql("INSERT INTO dbo.UrunKategorisis (Id, KategoriAdi) VALUES(2, 'Içecek');");
            Sql("INSERT INTO dbo.UrunKategorisis (Id, KategoriAdi) VALUES(3, 'Tatli');");
        }
        
        public override void Down()
        {
        }
    }
}
