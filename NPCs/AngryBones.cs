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
    public class AngryBones : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (    Items.Weapons.SpikeGun.Enabled
                &&  (npc.type == NPCID.AngryBones
                ||  npc.type == NPCID.ShortBones
                ||  npc.type == NPCID.BigBoned
                ||  npc.type == NPCID.AngryBonesBig
                ||  npc.type == NPCID.AngryBonesBigMuscle
                ||  npc.type == NPCID.AngryBonesBigHelmet))
            {
                // Using defence as an arbitrary choice for drop chance ¯\_(ツ)_/¯
                if (Main.rand.NextFloat() <= 1f / (npc.defense * 0.5f))
                {
                    Item.NewItem(npc.getRect(), ItemID.Spike, Main.rand.Next(1, npc.defense));
                }
            }
        }
    }
}
