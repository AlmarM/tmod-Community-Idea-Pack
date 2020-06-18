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
    public class MutatedSpore : ModItem
    {
        public static bool Enabled;

        public override void SetStaticDefaults()
        {
            string text = "Grants {0} additional minion{1}";
            text = string.Format(text, OvergrownSpore.ExtraMinionCount, OvergrownSpore.ExtraMinionCount > 1 ? "s" : "");

            Tooltip.SetDefault(text + "\nMelee minions inflict a poison debuff");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Green;
            item.width = 37;
            item.height = 30;
            item.accessory = true;
            item.scale = 0.85f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += OvergrownSpore.ExtraMinionCount;
            player.GetModPlayer<CommunityPlayer>().mutatedSpore = true;
        }

        public override void AddRecipes()
        {
            if (!Enabled) return;

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<OvergrownSpore>());
            recipe.AddIngredient(ItemID.Bezoar);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
