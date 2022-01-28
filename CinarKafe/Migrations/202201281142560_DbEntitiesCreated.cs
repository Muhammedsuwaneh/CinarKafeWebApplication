namespace CinarKafe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbEntitiesCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Garsons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 255),
                        Soyad = c.String(nullable: false, maxLength: 255),
                        SCN = c.String(nullable: false, maxLength: 255),
                        Eposta = c.String(nullable: false, maxLength: 255),
                        Adres = c.String(nullable: false, maxLength: 255),
                        TelefonNumara = c.String(nullable: false, maxLength: 255),
                        Maas = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MasaDurumus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DurumAdi = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Masas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasaNumarasi = c.Int(nullable: false),
                        MasaKapasitesi = c.Int(nullable: false),
                        KacKisiOturuyor = c.Int(nullable: false),
                        MasaDurumuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MasaDurumus", t => t.MasaDurumuId, cascadeDelete: true)
                .Index(t => t.MasaDurumuId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Siperis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(nullable: false),
                        Adet = c.Int(nullable: false),
                        ToplamFiyat = c.Double(nullable: false),
                        Tarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Uruns", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Uruns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunAdi = c.String(nullable: false, maxLength: 255),
                        Fiyat = c.Double(nullable: false),
                        StokMiktari = c.Int(nullable: false),
                        UrunKategorisiId = c.Int(nullable: false),
                        Resim = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UrunKategorisis", t => t.UrunKategorisiId, cascadeDelete: true)
                .Index(t => t.UrunKategorisiId);
            
            CreateTable(
                "dbo.UrunKategorisis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Siperislers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        SiperisId = c.Int(nullable: false),
                        MasaId = c.Int(nullable: false),
                        odenmis = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Masas", t => t.MasaId, cascadeDelete: true)
                .ForeignKey("dbo.Siperis", t => t.SiperisId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.SiperisId)
                .Index(t => t.MasaId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        KullanicAdi = c.String(nullable: false, maxLength: 255),
                        TelefonNumara = c.String(nullable: false, maxLength: 255),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Stoks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Uruns", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stoks", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.Siperislers", "SiperisId", "dbo.Siperis");
            DropForeignKey("dbo.Siperislers", "MasaId", "dbo.Masas");
            DropForeignKey("dbo.Siperislers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Siperis", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.Uruns", "UrunKategorisiId", "dbo.UrunKategorisis");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Masas", "MasaDurumuId", "dbo.MasaDurumus");
            DropIndex("dbo.Stoks", new[] { "UrunId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Siperislers", new[] { "MasaId" });
            DropIndex("dbo.Siperislers", new[] { "SiperisId" });
            DropIndex("dbo.Siperislers", new[] { "ApplicationUserId" });
            DropIndex("dbo.Uruns", new[] { "UrunKategorisiId" });
            DropIndex("dbo.Siperis", new[] { "UrunId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Masas", new[] { "MasaDurumuId" });
            DropTable("dbo.Stoks");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Siperislers");
            DropTable("dbo.UrunKategorisis");
            DropTable("dbo.Uruns");
            DropTable("dbo.Siperis");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Masas");
            DropTable("dbo.MasaDurumus");
            DropTable("dbo.Garsons");
        }
    }
}
