using CommunityPack.Items.Weapons;
using CommunityPack.Items.Accessories;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace CommunityPack.NPCs
{
    public class JungleMimic : GlobalNPC
    {
        public static bool Enabled;
        public static int SpawnChance;

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.BigMimicJungle)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        Item.NewItem(npc.getRect(), ItemType<IvyLauncher>());
                        break;

                    case 1:
                        Item.NewItem(npc.getRect(), ItemType<SporeTome>());
                        break;

                    case 2:
                        Item.NewItem(npc.getRect(), ItemType<DartScattergun>());
                        break;

                    case 3:
                        Item.NewItem(npc.getRect(), ItemType<SeedyNecklace>());
                        break;
                }
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            if (!Enabled) return;

            if (!Main.hardMode) return;

            if (!spawnInfo.player.ZoneJungle) return;

            if (!spawnInfo.player.ZoneRockLayerHeight) return;

            pool.Add(NPCID.BigMimicJungle, 1f / SpawnChance);
        }
    }
}