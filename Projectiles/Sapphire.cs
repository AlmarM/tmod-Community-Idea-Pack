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
    public class Sapphire : GemProjectile
    {
        bool firstFrame;
        bool spawnedProjectile;
        Vector2 spawnPosition;

        float Timer
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Sapphire Projectile");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            firstFrame = true;
            pingPongAnimation = false;
        }

        protected override Color GetGemColor()
        {
            return new Color(13, 107, 216);
        }

        public override void AI()
        {
            base.AI();

            if (firstFrame)
            {
                firstFrame = false;
                spawnPosition = projectile.Center;
            }

            Timer++;
            if (Timer > TimerUtils.Seconds(0.15f) && !spawnedProjectile)
            {
                if (projectile.owner == Main.myPlayer)
                {
                    spawnedProjectile = true;
                    Projectile.NewProjectile(spawnPosition, projectile.velocity, ProjectileID.SapphireBolt, projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }
    }
}