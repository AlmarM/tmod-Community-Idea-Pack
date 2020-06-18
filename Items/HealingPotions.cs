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

namespace CommunityPack.Items
{
    public class HealingPotions : GlobalItem
    {
        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.potion)
            {
                // Allow the Philosopher's Stone to have precedence.
                if (player.pStone)
                {
                    return;
                }

                if (player.GetModPlayer<CommunityPlayer>().bandOfLifeforce)
                {
                    float scalar = 1f - (Accessories.BandOfLifeforce.PotionCooldownReduction / 100f);
                    player.buffTime[player.FindBuffIndex(BuffID.PotionSickness)] = (int)(Item.potionDelay * scalar);
                }
            }
        }
    }
}
