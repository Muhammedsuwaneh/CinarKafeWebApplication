namespace CinarKafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMasaDurumu : DbMigration
    {
        public override void Up()
        {
                Sql("SET IDENTITY_INSERT MasaDurumus ON");
                Sql("INSERT INTO [dbo].[MasaDurumus] (Id, DurumAdi) VALUES(1, 'Mevcut')");
                Sql("INSERT INTO [dbo].[MasaDurumus] (Id, DurumAdi) VALUES(2, 'Mesgul')");
        }
        
        public override void Down()
        {
        }
    }
}
