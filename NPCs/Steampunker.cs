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

namespace CommunityPack.NPCs
{
    public class Steampunker : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (    Items.Ammo.DarkGreenSolutionItem.Enabled
                &&  type == NPCID.Steampunker
                &&  Main.LocalPlayer.ZoneJungle)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Ammo.DarkGreenSolutionItem>());
                nextSlot++;
            }
        }
    }
}
