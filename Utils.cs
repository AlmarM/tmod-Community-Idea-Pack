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

using CommunityPack.Items;

namespace CommunityPack
{
    public static class Utils
    {
        public static Vector2 RandomInsideUnitCircle()
        {
            double val = Main.rand.NextFloat() * Math.PI * 2f;
            return new Vector2((float)Math.Cos(val), (float)Math.Sin(val));
        }

        public static Vector2 FindSentrySpawnSpot(Player player, Vector2 worldStartPosition, int yOffset)
        {
            // Player.cs @ 33914
            int tileX = (int)worldStartPosition.X / 16;
            int tileY = (int)worldStartPosition.Y / 16;

            if (player.gravDir == -1f)
            {
                tileY = (int)(Main.screenPosition.Y + Main.screenHeight - Main.mouseY) / 16;
            }

            for (; tileY < Main.maxTilesY - 10
                && Main.tile[tileX, tileY] != null
                && !WorldGen.SolidTile2(tileX, tileY)
                && Main.tile[tileX - 1, tileY] != null
                && !WorldGen.SolidTile2(tileX - 1, tileY)
                && Main.tile[tileX + 1, tileY] != null
                && !WorldGen.SolidTile2(tileX + 1, tileY); tileY++)
            {
            }
            tileY--;

            return new Vector2(worldStartPosition.X, tileY * 16 - yOffset);
        }

        public static bool SentryFindTarget(Projectile projectile, out Vector2 target, out float targetDistance)
        {
            // Projectile.cs @ 19095
            bool hasTarget = false;
            float targetX = projectile.Center.X;
            float targetY = projectile.Center.Y;
            float distanceToTarget = 1000f;
            NPC ownerMinionAttackTargetNPC11 = projectile.OwnerMinionAttackTargetNPC;

            if (ownerMinionAttackTargetNPC11 != null && ownerMinionAttackTargetNPC11.CanBeChasedBy(projectile))
            {
                float targetCenterX = ownerMinionAttackTargetNPC11.position.X + ownerMinionAttackTargetNPC11.width / 2;
                float targetCenterY = ownerMinionAttackTargetNPC11.position.Y + ownerMinionAttackTargetNPC11.height / 2;
                float distance = Math.Abs(projectile.position.X + projectile.width / 2 - targetCenterX) + Math.Abs(projectile.position.Y + projectile.height / 2 - targetCenterY);
                if (distance < distanceToTarget && Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC11.position, ownerMinionAttackTargetNPC11.width, ownerMinionAttackTargetNPC11.height))
                {
                    distanceToTarget = distance;
                    targetX = targetCenterX;
                    targetY = targetCenterY;
                    hasTarget = true;
                }
            }

            if (!hasTarget)
            {
                for (int num1114 = 0; num1114 < 200; num1114++)
                {
                    if (Main.npc[num1114].CanBeChasedBy(projectile))
                    {
                        float targetCenterX = Main.npc[num1114].position.X + Main.npc[num1114].width / 2;
                        float targetCenterY = Main.npc[num1114].position.Y + Main.npc[num1114].height / 2;
                        float distance = Math.Abs(projectile.position.X + projectile.width / 2 - targetCenterX) + Math.Abs(projectile.position.Y + projectile.height / 2 - targetCenterY);
                        if (distance < distanceToTarget && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num1114].position, Main.npc[num1114].width, Main.npc[num1114].height))
                        {
                            distanceToTarget = distance;
                            targetX = targetCenterX;
                            targetY = targetCenterY;
                            hasTarget = true;
                        }
                    }
                }
            }

            if (hasTarget)
            {
                target = new Vector2(targetX, targetY);
                targetDistance = distanceToTarget;

                return true;
            }

            target = Vector2.Zero;
            targetDistance = 1000f;

            return false;
        }

        public static void SpawnProjectilesFromSky(Player player, int projectileCount, int projectileType, float speed)
        {
            // Player.cs @ 28745
            for (int i = 0; i < projectileCount; i++)
            {
                float x = player.position.X + Main.rand.Next(-400, 400);
                float y3 = player.position.Y - Main.rand.Next(500, 800);
                Vector2 vector = new Vector2(x, y3);
                float num42 = player.position.X + player.width / 2 - vector.X;
                float num41 = player.position.Y + player.height / 2 - vector.Y;
                num42 += Main.rand.Next(-100, 101);
                float num39 = (float)Math.Sqrt(num42 * num42 + num41 * num41);
                num39 = 23f / num39;
                num42 *= num39;
                num41 *= num39;
                int num35 = Projectile.NewProjectile(x, y3, num42 * speed, num41 * speed, projectileType, 70, 5f, player.whoAmI);
                Main.projectile[num35].ai[1] = player.position.Y;
            }
        }

        public static List<int> ApplyBuffInArea(Vector2 position, float distance, int buffType, int buffDuration)
        {
            List<int> playersAffected = new List<int>();

            for (int i = 0; i < 255; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead)
                {
                    float dist = Vector2.Distance(position, player.Center);
                    if (dist <= distance)
                    {
                        if (player.HasBuff(buffType) && player.buffTime[player.FindBuffIndex(buffType)] > buffDuration)
                        {
                            continue;
                        }

                        player.AddBuff(buffType, buffDuration);

                        playersAffected.Add(i);
                    }
                }
            }

            return playersAffected;
        }

        public static void ProjectilesSpawnEvenSpread(float projectileCount, float coneAngle, Vector2 position, Vector2 speed, int projType, int damage, float knockBack, int owner)
        {
            float rotation = MathHelper.ToRadians(coneAngle);

            for (int i = 0; i < projectileCount; i++)
            {
                Vector2 perturbedSpeed = speed.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (projectileCount - 1)));
                Projectile.NewProjectile(position, perturbedSpeed, projType, damage, knockBack, owner);
            }
        }

        public static void ProjectilesSpawnRandomSpread(float projectileCount, float coneAngle, Vector2 position, Vector2 speed, int projType, int damage, float knockBack, int owner)
        {
            float rotation = MathHelper.ToRadians(coneAngle);

            for (int i = 0; i < projectileCount; i++)
            {
                Vector2 perturbedSpeed = speed.RotatedByRandom(rotation);
                Projectile.NewProjectile(position, perturbedSpeed, projType, damage, knockBack, owner);
            }
        }

        // NPC.cs @ 56835
        public static bool JungleMimicSummonCheck(int x, int y)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !Main.hardMode)
            {
                return false;
            }
            int num13 = Chest.FindChest(x, y);
            if (num13 < 0)
            {
                return false;
            }
            int num11 = 0;
            int num10 = 0;
            for (int l = 0; l < 40; l++)
            {
                ushort num7 = Main.tile[Main.chest[num13].x, Main.chest[num13].y].type;
                int num6 = Main.tile[Main.chest[num13].x, Main.chest[num13].y].frameX / 36;
                if (TileID.Sets.BasicChest[num7] && (num7 != 21 || num6 < 5 || num6 > 6) && Main.chest[num13].item[l] != null && Main.chest[num13].item[l].type > ItemID.None)
                {
                    if (Main.chest[num13].item[l].type == ItemType<KeyOfFright>())
                    {
                        num11 += Main.chest[num13].item[l].stack;
                    }
                    else
                    {
                        num10++;
                    }
                }
            }
            if (num10 == 0 && num11 == 1)
            {
                _ = 1;
                if (TileID.Sets.BasicChest[Main.tile[x, y].type])
                {
                    if (Main.tile[x, y].frameX % 36 != 0)
                    {
                        x--;
                    }
                    if (Main.tile[x, y].frameY % 36 != 0)
                    {
                        y--;
                    }
                    int number3 = Chest.FindChest(x, y);
                    for (int k = x; k <= x + 1; k++)
                    {
                        for (int i = y; i <= y + 1; i++)
                        {
                            if (TileID.Sets.BasicChest[Main.tile[k, i].type])
                            {
                                Main.tile[k, i].active(active: false);
                            }
                        }
                    }
                    for (int j = 0; j < 40; j++)
                    {
                        Main.chest[num13].item[j] = new Item();
                    }
                    Chest.DestroyChest(x, y);
                    int number2 = 1;
                    if (Main.tile[x, y].type == 467)
                    {
                        number2 = 5;
                    }
                    if (Main.tile[x, y].type >= 470)
                    {
                        number2 = 101;
                    }
                    NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, number2, x, y, 0f, number3, Main.tile[x, y].type);
                    NetMessage.SendTileSquare(-1, x, y, 3);
                }

                int num8 = NPC.NewNPC(x * 16 + 16, y * 16 + 32, NPCID.BigMimicJungle);
                Main.npc[num8].whoAmI = num8;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num8);
                Main.npc[num8].BigMimicSpawnSmoke();
            }
            return false;
        }
    }
}