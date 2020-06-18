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
    public class SeaCreatureClumpProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sea Creature Clump");

            ProjectileID.Sets.TurretFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 44;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.manualDirectionChange = true;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.ignoreWater = true;
            projectile.sentry = true;
            projectile.netImportant = true;
            projectile.penetrate = -1;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool? CanHitNPC(NPC npc)
        {
            return false;
        }

        public override void AI()
        {
            // Since we only spawn projectiles at this moment, there is no need for every player to update.
            if (projectile.owner != Main.myPlayer)
            {
                return;
            }

            if (Main.GameUpdateCount % 60 == 0)
            {
                if (Utils.SentryFindTarget(projectile, out Vector2 target, out float distance))
                {
                    Vector2 direction = target - projectile.Center;

                    int projId = -1;
                    Vector2 speed = Vector2.Zero;

                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            projId = ProjectileType<Crabby>();
                            speed = direction * (5f / distance);
                            break;

                        case 1:
                            projId = ProjectileType<Seashell>();
                            speed = direction * (10f / distance);
                            break;

                        case 2:
                            projId = ProjectileType<Starfish>();
                            speed = direction * (10f / distance);
                            break;
                    }

                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, projId, projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }
    }
}