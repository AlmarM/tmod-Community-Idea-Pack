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

namespace CommunityPack
{
    public class CommunityPlayer : ModPlayer
    {
        public bool seedyNecklace;
        public bool bandOfLifeforce;
        public bool mutatedSpore;

        public override void ResetEffects()
        {
            seedyNecklace = false;
            bandOfLifeforce = false;
            mutatedSpore = false;
        }

        public override void PreUpdate()
        {
            // Spawn the jungle mimic when the key of fright is placed inside.
            // Player.cs @ 19767
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int lastChest = player.lastChest;
                if (player.chest == -1 && lastChest >= 0 && Main.chest[lastChest] != null && Main.chest[lastChest] != null)
                {
                    Utils.JungleMimicSummonCheck(Main.chest[lastChest].x, Main.chest[lastChest].y);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (mutatedSpore && proj.minion)
            {
                int duration = Main.rand.Next(TimerUtils.Seconds(5f), TimerUtils.Seconds(10f));
                target.AddBuff(BuffID.Poisoned, duration);
            }
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (seedyNecklace && !player.HasBuff(BuffType<Buffs.Seedless>()))
            {
                int cooldown = Items.Accessories.SeedyNecklace.ActivationCooldown;
                player.AddBuff(BuffType<Buffs.Seedless>(), TimerUtils.Seconds(cooldown));

                if (player.whoAmI == Main.myPlayer)
                {
                    Items.Accessories.SeedyNecklace.CreatePoppingSeeds(player);
                }
            }
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (    Items.Weapons.SeaCreatureClump.Enabled 
                &&  liquidType == 0
                &&  worldLayer == 1
                &&  player.ZoneBeach
                &&  Main.rand.NextBool(33))
            {
                caughtType = ItemType<Items.Weapons.SeaCreatureClump>();
            }
        }
    }
}