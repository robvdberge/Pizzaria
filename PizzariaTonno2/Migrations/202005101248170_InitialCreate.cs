namespace PizzariaTonno2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        PizzaId = c.Int(nullable: false, identity: true),
                        PizzaSoort = c.Boolean(nullable: false),
                        IngredientId1 = c.Int(nullable: false),
                        IngredientId2 = c.Int(nullable: false),
                        IngredientId3 = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PizzaId);
            
            CreateTable(
                "dbo.PizzaIngredients",
                c => new
                    {
                        Pizza_PizzaId = c.Int(nullable: false),
                        Ingredient_IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pizza_PizzaId, t.Ingredient_IngredientId })
                .ForeignKey("dbo.Pizzas", t => t.Pizza_PizzaId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_IngredientId, cascadeDelete: true)
                .Index(t => t.Pizza_PizzaId)
                .Index(t => t.Ingredient_IngredientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PizzaIngredients", "Ingredient_IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.PizzaIngredients", "Pizza_PizzaId", "dbo.Pizzas");
            DropIndex("dbo.PizzaIngredients", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.PizzaIngredients", new[] { "Pizza_PizzaId" });
            DropTable("dbo.PizzaIngredients");
            DropTable("dbo.Pizzas");
            DropTable("dbo.Ingredients");
        }
    }
}
