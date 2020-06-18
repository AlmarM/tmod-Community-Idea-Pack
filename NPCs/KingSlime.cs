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
    public class KingSlime : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.KingSlime && Main.rand.NextBool(10))
            {
                Item.NewItem(npc.getRect(), ItemType<Items.Accessories.RoyalDefence>());
            }
        }
    }
}