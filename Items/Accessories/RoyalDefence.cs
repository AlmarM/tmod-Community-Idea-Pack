using CommunityPack.Items.Weapons;
using CommunityPack.Items.Accessories;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CommunityPack.Items.Accessories
{
    public class RoyalDefence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Adds 1 defence for every minion summoned");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
            item.width = 0;
            item.height = 0;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += (int)player.slotsMinions;
        }
    }
}
