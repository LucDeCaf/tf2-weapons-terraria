using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TF2Weapons.Content.Items
{
    public class Knife : ModItem
    {
        public const float BackstabRange = 50f;

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.knockBack = 1;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(silver: 50);
            Item.ChangePlayerDirectionOnShoot = false;
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe testRecipe = CreateRecipe();
            testRecipe.AddIngredient(ItemID.DirtBlock, 10);
            testRecipe.AddTile(TileID.WorkBenches);
            testRecipe.Register();
        }


        /// <summary>
        /// Returns true if the stabber is in position to backstab the stabee.
        /// This function doesn't care how far apart the two are, so a bounds check of sorts
        /// must be done separately to prevent world-length stabs.
        /// </summary>
        public static bool CanBackstab(Entity stabber, Entity stabee)
        {
            // You can't stab the air and the air can't stab you
            if (stabber == null | stabee == null)
            {
                Main.NewText($"Stabber or stabee not found ({stabber}, {stabee})");
                return false;
            }

            // Has to be a backstab, not a frontstab
            if (stabber.direction != stabee.direction)
            {
                Main.NewText("Not facing same direction");
                return false;
            }

            if (stabber.direction == 1)
            {
                // If facing right, target must be on right
                return stabber.position.X <= stabee.position.X;
            }
            else
            {
                // If facing left, target must be on left
                return stabber.position.X >= stabee.position.X;
            }
        }
    }
}
