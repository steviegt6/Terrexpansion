using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terrexpansion.Content.Items.Weapons.Swords
{
    public class FirstFractal : BaseItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.FirstFractal;

        public override void SafeSetDefaults()
        {
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.DamageType = DamageClass.Melee;
            item.channel = true;
            item.noMelee = true;
            item.shoot = ProjectileID.FirstFractal;
            item.useAnimation = 35;
            item.useTime = item.useAnimation / 3;
            item.shootSpeed = 16f;
            item.damage = 190;
            item.knockBack = 6.5f;
            item.value = Item.sellPrice(gold: 20);
            item.crit = 10;
            item.rare = ItemRarityID.Purple;
            item.glowMask = GlowMaskID.FirstFractal;
        }

        // Taken from Player.cs
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 targetPos = Main.MouseWorld;
            List<NPC> validNPCs;
            bool sparkleGuitarTarget = GetSparkleGuitarTarget(player, out validNPCs);

            if (sparkleGuitarTarget)
            {
                NPC npc = validNPCs[Main.rand.Next(validNPCs.Count)];
                targetPos = npc.Center + npc.velocity * 20f;
            }

            Vector2 targetPosFixed = targetPos - player.Center;
            Vector2 circularEdge = Main.rand.NextVector2CircularEdge(1f, 1f);

            if (!sparkleGuitarTarget)
            {
                targetPos += Main.rand.NextVector2Circular(24f, 24f);

                if (targetPosFixed.Length() > 700f)
                {
                    targetPosFixed *= 700f / targetPosFixed.Length();
                    targetPos = player.Center + targetPosFixed;
                }

                float rotationAmount = Utils.GetLerpValue(0f, 6f, player.velocity.Length(), true) * 0.8f;
                circularEdge *= 1f - rotationAmount;
                circularEdge += player.velocity * rotationAmount;
                circularEdge = circularEdge.SafeNormalize(Vector2.UnitX);
            }

            float num76 = 60f;
            float ai0 = Main.rand.NextFloatDirection() * (float)Math.PI * (1f / num76) * 0.5f * 1f;
            float num78 = num76 / 2f;
            float scaleFactor = 12f + Main.rand.NextFloat() * 2f;
            Vector2 velocity = circularEdge * scaleFactor;
            Vector2 offsetVector = new Vector2(0f, 0f);
            Vector2 offsetVelocity = velocity;

            for (int num79 = 0; num79 < num78; num79++)
            {
                offsetVector += offsetVelocity;
                offsetVelocity = offsetVelocity.RotatedBy(ai0);
            }

            Vector2 flippedOffsetVector = -offsetVector;
            Projectile.NewProjectile(targetPos + flippedOffsetVector, velocity, type, damage, knockBack, player.whoAmI, ai0, Utils.GetLerpValue(player.itemAnimationMax, 0f, player.itemAnimation, true));;

            return false;
        }

        // Taken from Player.cs
        private bool GetSparkleGuitarTarget(Player player, out List<NPC> validTargets)
        {
            validTargets = new List<NPC>();
            Rectangle value = Utils.CenteredRectangle(player.Center, new Vector2(1000f, 800f));
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(this) && nPC.Hitbox.Intersects(value))
                {
                    validTargets.Add(nPC);
                }
            }

            if (validTargets.Count == 0)
            {
                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Meowmere)
                .AddIngredient(ItemID.StarWrath)
                .AddIngredient(ItemID.TerraBlade)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
