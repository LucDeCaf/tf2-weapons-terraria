using Microsoft.Xna.Framework;
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
        /// Returns true if the attacker is in position to backstab the target.
        /// This function doesn't care how far apart the two are, so a bounds check of sorts
        /// must be done separately to prevent world-length stabs.
        /// </summary>
        public static bool CanBackstab(Entity attacker, Entity target)
        {
            // You can't stab the air and the air can't stab you
            if (attacker == null | target == null)
            {
                return false;
            }

            // Has to be a backstab, not a frontstab
            if (attacker.direction != target.direction)
            {
                return false;
            }

            //? Checks if attacker is actually behind target, but TF2 doesn't work that
            //? way so I'm leaving it out (at least for now)
            // if (attacker.direction == 1)
            // {
            //     // If facing right, target must be on right
            //     return attacker.position.X <= target.position.X;
            // }
            // else
            // {
            //     // If facing left, target must be on left
            //     return attacker.position.X >= target.position.X;
            // }

            return true;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            bool isBackstab = CanBackstab(player, target);

            if (isBackstab)
            {
                modifiers.SetInstantKill();

                Rectangle combatTextLocation = new()
                {
                    Location = target.Center.ToPoint(),
                    Width = 1,
                    Height = 1,
                };
                CombatText.NewText(combatTextLocation, Color.LimeGreen, 950, dramatic: true);
            }
        }

        public override void UseAnimation(Player player)
        {
            base.UseAnimation(player);
        }
    }
}
