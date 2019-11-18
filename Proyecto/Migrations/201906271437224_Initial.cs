namespace Proyecto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrador",
                c => new
                    {
                        IdAdministrador = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        ApPaterno = c.String(),
                        ApMaterno = c.String(),
                        Dni = c.String(),
                        Direccion = c.String(),
                        Pass = c.String(),
                    })
                .PrimaryKey(t => t.IdAdministrador);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        IdCliente = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Dni = c.String(),
                        Direccion = c.String(),
                    })
                .PrimaryKey(t => t.IdCliente);
            
            CreateTable(
                "dbo.DetalleVenta",
                c => new
                    {
                        IdDetalleVenta = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        IdVendedor = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cantidad = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDetalleVenta);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Stock = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descripcion = c.String(),
                        UnidadMedida = c.String(),
                    })
                .PrimaryKey(t => t.IdProducto);
            
            CreateTable(
                "dbo.Vendedor",
                c => new
                    {
                        IdVendedor = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        ApPaterno = c.String(),
                        ApMaterno = c.String(),
                        Dni = c.String(),
                        Direccion = c.String(),
                        Pass = c.String(),
                    })
                .PrimaryKey(t => t.IdVendedor);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vendedor");
            DropTable("dbo.Producto");
            DropTable("dbo.DetalleVenta");
            DropTable("dbo.Cliente");
            DropTable("dbo.Administrador");
        }
    }
}
