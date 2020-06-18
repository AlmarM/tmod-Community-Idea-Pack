using System.IO;
using Terraria.Localization;
using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ModLoader.Config;
using static Terraria.ModLoader.ModContent;
using Terraria.UI;

namespace CommunityPack
{
	public class CommunityPack : Mod
	{
        public override void AddRecipeGroups()
        {
            RecipeGroup.RegisterGroup("CommunityPack:DungeonBrick", new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dungeon Brick", new int[]
            {
                    ItemID.BlueBrick,
                    ItemID.GreenBrick,
                    ItemID.RedBrick
            }));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = null;

            if (Items.Accessories.BandOfLifeforce.Enabled)
            {
                recipe = new ModRecipe(this);

                var ingredients = GetInstance<Config.CommunityPackConfig>().BandOfLifeforceData.BandOfRegenerationRecipe;
                foreach (var kv in ingredients)
                {
                    recipe.AddIngredient(kv.Key.Type, kv.Value);
                }

                recipe.AddTile(TileID.Anvils);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(ItemID.BandofRegeneration);
                recipe.AddRecipe();
            }

            if (Items.Weapons.GemGun.Enabled)
            {
                int[] gems = new int[]
                {
                    ItemID.Amethyst,
                    ItemID.Topaz,
                    ItemID.Sapphire,
                    ItemID.Emerald,
                    ItemID.Ruby,
                    ItemID.Diamond,
                    ItemID.Amber
                };

                int woodCount = Items.Weapons.GemGun.GemAmmoWoodRecipeCount;

                foreach (int gem in gems)
                {
                    recipe = new ModRecipe(this);
                    recipe.AddIngredient(ItemID.Wood, woodCount);
                    recipe.AddIngredient(gem, 1);
                    recipe.SetResult(gem, 2);
                    recipe.AddRecipe();
                }
            }
        }
    }
}