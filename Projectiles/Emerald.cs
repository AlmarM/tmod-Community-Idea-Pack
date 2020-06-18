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

namespace CommunityPack.Projectiles
{
    public class Emerald : GemProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Emerald Projectile");
        }

        protected override Color GetGemColor()
        {
            return new Color(33, 184, 115);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, TimerUtils.Seconds(Items.Ammo.GemAmmo.DebuffDuration));
        }
    }
}