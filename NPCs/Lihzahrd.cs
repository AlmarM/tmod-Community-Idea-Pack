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
using System;
using Microsoft.Xna.Framework;

namespace CommunityPack.NPCs
{
    public class Lihzahrd : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (    Items.Weapons.SpikeGun.Enabled
                &&  (npc.type == NPCID.Lihzahrd
                ||  npc.type == NPCID.LihzahrdCrawler))
            {
                // Using defence as an arbitrary choice for drop chance ¯\_(ツ)_/¯
                if (Main.rand.NextFloat() <= 1f / ((npc.defense - 10) * 0.5f))
                {
                    Item.NewItem(npc.getRect(), ItemID.WoodenSpike, Main.rand.Next(1, npc.defense - 10));
                }
            }
        }
    }
}
