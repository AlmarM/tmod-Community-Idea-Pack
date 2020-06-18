using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace CommunityPack.Items.Accessories
{
    public class SparklePin : ModItem
    {
        public static bool Enabled;
        public static float DamageBoost;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants 10% more summon damage");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 1, silver: 30);
            item.rare = ItemRarityID.Blue;
            item.width = 29;
            item.height = 29;
            item.accessory = true;
            item.scale = 0.85f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += DamageBoost / 100f;
        }

        public override void AddRecipes()
        {
            if (!Enabled) return;

            ModRecipe recipe = new ModRecipe(mod);

            var ingredients = GetInstance<Config.CommunityPackConfig>().PreHardmodeSummonerBuffData.SparklePinRecipe;
            foreach (var kv in ingredients)
            {
                recipe.AddIngredient(kv.Key.Type, kv.Value);
            }

            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
