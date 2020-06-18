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
    public class OvergrownSpore : ModItem
    {
        public static bool Enabled;
        public static int ExtraMinionCount;

        public override void SetStaticDefaults()
        {
            string text = "Grants {0} additional minion{1}";
            text = string.Format(text, ExtraMinionCount, ExtraMinionCount > 1 ? "s" : "");

            Tooltip.SetDefault(text);
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Green;
            item.width = 30;
            item.height = 26;
            item.accessory = true;
            item.scale = 0.85f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += ExtraMinionCount;
        }
    }
}
