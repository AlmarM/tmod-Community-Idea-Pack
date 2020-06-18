using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace CommunityPack.Config
{
    [Label("Configurable Data")]
    public class CommunityPackConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Jungle Mimic Config")]
        public JungleMimicData JungleMimicData;

        [Header("Band of Lifeforce Config")]
        public BandOfLifeforceData BandOfLifeforceData;

        [Header("Gem Gun Config")]
        public GemDunData GemDunData;

        [Header("Pre-Hardmode Summoner Buff Config")]
        public PreHardmodeSummonerBuffData PreHardmodeSummonerBuffData;

        [Header("Jungle Solution Config")]
        public JungleSolutionData JungleSolutionData;

        [Header("Spike Gun Config")]
        public SpikeGunData SpikeGunData;

        public CommunityPackConfig()
        {
            JungleMimicData = new JungleMimicData();
            BandOfLifeforceData = new BandOfLifeforceData();
            GemDunData = new GemDunData();
            PreHardmodeSummonerBuffData = new PreHardmodeSummonerBuffData();
            JungleSolutionData = new JungleSolutionData();
            SpikeGunData = new SpikeGunData();
        }

        public override void OnChanged()
        {
            LoadAllData();
        }

        public override void OnLoaded()
        {
            LoadAllData();
        }

        void LoadAllData()
        {
            JungleMimicData.ApplyChangedValues();
            BandOfLifeforceData.ApplyChangedValues();
            GemDunData.ApplyChangedValues();
            PreHardmodeSummonerBuffData.ApplyChangedValues();
            JungleSolutionData.ApplyChangedValues();
            SpikeGunData.ApplyChangedValues();
        }
    }

    public interface IConfigData
    {
        void ApplyChangedValues();
    }

    [Label("Jungle Mimic - Idea by u/xSeum")]
    public class JungleMimicData : IConfigData
    {
        [Label("Enabled (requires reload):")]
        public bool Enabled = true;

        [Label("Jungle Mimic spawn chance, 1 out of:")]
        [Range(1, 9999)]
        public int MimicSpawnChance = 150;

        [Label("Seedy Necklace cooldown time:")]
        [Range(0, 999)]
        public int SeedyNecklaceCooldownTime = 20;

        [Label("Seedy Necklace popping seeds spawn count:")]
        [Range(1, 99)]
        public int SeedyNecklacePoppingSeedCount = 5;

        [Label("Dart Scattergun damage:")]
        [Range(1, 999)]
        public int DartScattergunDamage = 30;

        [Label("Dart Scattergun knock back")]
        [Range(0f, 20f)]
        public float DartScattergunKnockBack = 30;

        [Label("Dart Scattergun minimum ammo shoot count:")]
        [Range(0, 99)]
        public int DartScattergunMinProjectileCount = 3;

        [Label("Dart Scattergun maximum ammo shoot count:")]
        [Range(0, 99)]
        public int DartScattergunMaxProjectileCount = 6;

        [Label("Dart Scattergun ammo shoot spread angle:")]
        [Range(0, 360)]
        public int DartScattergunProjectileSpread = 30;

        [Label("Ivy Launcher damage:")]
        [Range(0, 999)]
        public int IvyLauncherDamage = 30;

        [Label("Ivy Launcher knock back")]
        [Range(0f, 20f)]
        public float IvyLauncherKnockBack = 4f;

        [Label("Ivy Crystal trigger count:")]
        [Range(0, 99)]
        public int IvyCrystalTriggerCount = 3;

        [Label("Ivy Crystal spawn count:")]
        [Range(0, 99)]
        public int IvyCrystalSpawnCount = 3;

        [Label("Ivy Crystal spawn angle")]
        [Range(0, 360)]
        public int IvyCrystalSpawnAngle = 5;

        [Label("Spore Tome use delay:")]
        [Range(0, 999)]
        public int SporeTomeUseDelay = 60;

        [Label("Spore Tome mana cost:")]
        [Range(0, 200)]
        public int SporeTomeManaCost = 14;

        [Label("Spore Tome minimum spore spawn count:")]
        [Range(0, 99)]
        public int SporeTomeMinProjectileCount = 2;

        [Label("Spore Tome maximum spore spawn count:")]
        [Range(0, 99)]
        public int SporeTomeMaxProjectileCount = 4;

        [Label("Spore Tome spore damage:")]
        [Range(0, 999)]
        public int SporeTomeSporeDamage = 70;

        [Label("Spore Tome spore knock back")]
        [Range(0f, 20f)]
        public float SporeTomeSporeKnockBack = 1.5f;

        [Label("Popping Seed buff duration:")]
        [Range(0, 999)]
        public int PoppingSeedBuffDuration = 15;

        [Label("Vine Ball ammo damage:")]
        [Range(0, 999)]
        public int VineBallDamage = 25;

        [Label("Spore Tome spore knock back")]
        [Range(0f, 20f)]
        public float VineBallKnockBack = 4f;

        public void ApplyChangedValues()
        {
            NPCs.JungleMimic.Enabled = Enabled;
            NPCs.JungleMimic.SpawnChance = MimicSpawnChance;
            Items.Accessories.SeedyNecklace.ActivationCooldown = SeedyNecklaceCooldownTime;
            Items.Accessories.SeedyNecklace.SeedSpawnCount = SeedyNecklacePoppingSeedCount;
            Items.Weapons.DartScattergun.Damage = DartScattergunDamage;
            Items.Weapons.DartScattergun.KnockBack = DartScattergunKnockBack;
            Items.Weapons.DartScattergun.MinProjectileCount = DartScattergunMinProjectileCount;
            Items.Weapons.DartScattergun.MaxProjectileCount = DartScattergunMaxProjectileCount;
            Items.Weapons.DartScattergun.ProjectileSpread = DartScattergunProjectileSpread;
            Items.Weapons.IvyLauncher.Damage = IvyLauncherDamage;
            Items.Weapons.IvyLauncher.KnockBack = IvyLauncherKnockBack;
            Items.Weapons.IvyLauncher.IvyCrystalTriggerCount = IvyCrystalTriggerCount;
            Items.Weapons.IvyLauncher.IvyCrystalSpawnCount = IvyCrystalSpawnCount;
            Items.Weapons.IvyLauncher.IvyCrystalSpawnAngle = IvyCrystalSpawnAngle;
            Items.Weapons.SporeTome.UseTime = SporeTomeUseDelay;
            Items.Weapons.SporeTome.ManaCost = SporeTomeManaCost;
            Items.Weapons.SporeTome.MinProjectileCount = SporeTomeMinProjectileCount;
            Items.Weapons.SporeTome.MaxProjectileCount = SporeTomeMaxProjectileCount;
            Items.Weapons.SporeTome.SporeDamage = SporeTomeSporeDamage;
            Items.Weapons.SporeTome.SporeKnockBack = SporeTomeSporeKnockBack;
            Projectiles.PoppingSeed.BuffDuration = PoppingSeedBuffDuration;
            Items.Weapons.VineBallAmmo.Damage = VineBallDamage;
            Items.Weapons.VineBallAmmo.KnockBack = VineBallKnockBack;
        }
    }

    [Label("Band of Lifeforce - Idea by u/BermudaNiccholas")]
    public class BandOfLifeforceData : IConfigData
    {
        [Label("Enabled (requires reload):")]
        public bool Enabled = true;

        [Label("Band of Lifeforce extra health:")]
        [Range(0, 999)]
        public int BandOfLifeforceExtraHealth = 40;

        [Label("Band of Lifeforce potion cooldown reduction:")]
        [Range(0, 100)]
        public int BandOfLifeforceCooldownReduction = 20;

        [Label("Band of Lifeforce passive regen:")]
        [Range(0, 999)]
        public int BandOfLifeforceRegen = 1;

        [Label("Band of Regeneration recipe (requires reload):")]
        public Dictionary<ItemDefinition, int> BandOfRegenerationRecipe = new Dictionary<ItemDefinition, int>()
        {
            { new ItemDefinition(ItemID.Chain), 1 },
            { new ItemDefinition(ItemID.LifeCrystal), 1 }
        };

        [Label("Band of Lifeforce recipe (requires reload):")]
        public Dictionary<ItemDefinition, int> BandOfLifeforceRecipe = new Dictionary<ItemDefinition, int>()
        {
            { new ItemDefinition(ItemID.CharmofMyths), 1 },
            { new ItemDefinition(ItemID.LifeCrystal), 5 },
            { new ItemDefinition(ItemID.LifeFruit), 5 }
        };

        public void ApplyChangedValues()
        {
            Items.Accessories.BandOfLifeforce.Enabled = Enabled;
            Items.Accessories.BandOfLifeforce.ExtraHealth = BandOfLifeforceExtraHealth;
            Items.Accessories.BandOfLifeforce.PotionCooldownReduction = BandOfLifeforceCooldownReduction;
            Items.Accessories.BandOfLifeforce.LifeRegen = BandOfLifeforceRegen;
        }
    }

    [Label("Gem Gun - Idea by u/pitchforkpopcornsale")]
    public class GemDunData : IConfigData
    {
        [Label("Enabled (requires reload):")]
        public bool Enabled = true;

        [Label("Gem Dust use delay:")]
        [Range(0, 999)]
        public int GemGunUseTime = 12;

        [Label("Gem Dust projectile spread angle:")]
        [Range(0, 360)]
        public int GemGunSpreadAngle = 5;

        [Label("Gem Gun recipe (requires reload):")]
        public Dictionary<ItemDefinition, int> GemGunRecipe = new Dictionary<ItemDefinition, int>()
        {
            { new ItemDefinition(ItemID.SoulofMight), 20 },
            { new ItemDefinition(ItemID.SoulofSight), 20 },
            { new ItemDefinition(ItemID.SoulofFright), 20 },
            { new ItemDefinition(ItemID.TempleKey), 1 }
        };

        [Label("Gems wood trade in count:")]
        [Range(0, 999)]
        public int GemsWoodRecipeCount = 10;

        [Label("Amethyst damage:")]
        [Range(0, 999)]
        public int AmethystDamage = 50;

        [Label("Topaz damage:")]
        [Range(0, 999)]
        public int TopazDamage = 60;

        [Label("Sapphire damage:")]
        [Range(0, 999)]
        public int SapphireDamage = 70;

        [Label("Emerald damage:")]
        [Range(0, 999)]
        public int EmeraldDamage = 80;

        [Label("Ruby damage:")]
        [Range(0, 999)]
        public int RubyDamage = 90;

        [Label("Diamond damage:")]
        [Range(0, 999)]
        public int DiamondDamage = 100;

        [Label("Amber damage:")]
        [Range(0, 999)]
        public int AmberDamage = 110;

        [Label("Gem ammo buff duration in seconds:")]
        [Range(0, 999)]
        public int GemAmmoBuffDuration = 5;

        [Label("Gem ammo debuff duration in seconds:")]
        [Range(0, 999)]
        public int GemAmmoDebuffDuration = 5;

        public void ApplyChangedValues()
        {
            Items.Weapons.GemGun.Enabled = Enabled;
            Items.Weapons.GemGun.UseTime = GemGunUseTime;
            Items.Weapons.GemGun.SpreadAngle = GemGunSpreadAngle;
            Items.Weapons.GemGun.GemAmmoWoodRecipeCount = GemsWoodRecipeCount;
            Items.Ammo.GemAmmo.AmethystDamage = AmethystDamage;
            Items.Ammo.GemAmmo.TopazDamage = TopazDamage;
            Items.Ammo.GemAmmo.SapphireDamage = SapphireDamage;
            Items.Ammo.GemAmmo.EmeraldDamage = EmeraldDamage;
            Items.Ammo.GemAmmo.RubyDamage = RubyDamage;
            Items.Ammo.GemAmmo.DiamondDamage = DiamondDamage;
            Items.Ammo.GemAmmo.AmberDamage = AmberDamage;
            Items.Ammo.GemAmmo.BuffDuration = GemAmmoBuffDuration;
            Items.Ammo.GemAmmo.DebuffDuration = GemAmmoDebuffDuration;
        }
    }

    [Label("Pre-Hardmode Summoner Buff - Idea by u/memeymatt")]
    public class PreHardmodeSummonerBuffData : IConfigData
    {
        [Label("Sparkle Pin enabled (requires reload):")]
        public bool SparklePinEnabled = true;

        [Label("Sparkle Pin % damage boost")]
        [Range(1, 999)]
        public int SparklePinDamageBoost = 10;

        [Label("Sparkle Pin crafting recipe (requires reload):")]
        public Dictionary<ItemDefinition, int> SparklePinRecipe = new Dictionary<ItemDefinition, int>()
        {
            { new ItemDefinition(ItemID.Amethyst), 1 },
            { new ItemDefinition(ItemID.Topaz), 1 },
            { new ItemDefinition(ItemID.Sapphire), 1 },
            { new ItemDefinition(ItemID.Emerald), 1 },
            { new ItemDefinition(ItemID.Ruby), 1 },
            { new ItemDefinition(ItemID.Diamond), 1 },
            { new ItemDefinition(ItemID.Amber), 1 },
            { new ItemDefinition(ItemID.FallenStar), 1 }
        };

        [Label("Overgrown Spore enabled (requires reload):")]
        public bool OvergrownSporeEnabled = true;

        [Label("Overgrown Spore additional minion count:")]
        [Range(1, 99)]
        public int OvergrownSporeMinionCount = 1;

        [Label("Mutated Spore enabled (requires reload):")]
        public bool MutatedSporeEnabled = true;

        [Label("Sea Creature Clump enabled (requires reload):")]
        public bool SeaCreasureClumpEnabled = true;

        [Label("Sea Creature Clump damage:")]
        [Range(1, 999)]
        public int SeaCreasureClumpDamage = 11;

        [Label("Sea Creature Clump mana used:")]
        [Range(1, 999)]
        public int SeaCreasureClumpManaUsed = 20;

        public void ApplyChangedValues()
        {
            Items.Accessories.SparklePin.Enabled = SparklePinEnabled;
            Items.Accessories.SparklePin.DamageBoost = SparklePinDamageBoost;
            Items.Accessories.OvergrownSpore.Enabled = OvergrownSporeEnabled;
            Items.Accessories.OvergrownSpore.ExtraMinionCount = OvergrownSporeMinionCount;
            Items.Accessories.MutatedSpore.Enabled = MutatedSporeEnabled;
            Items.Weapons.SeaCreatureClump.Enabled = SeaCreasureClumpEnabled;
            Items.Weapons.SeaCreatureClump.Damage = SeaCreasureClumpDamage;
            Items.Weapons.SeaCreatureClump.ManaUsed = SeaCreasureClumpManaUsed;
        }
    }

    [Label("Jungle Solution - Idea by u/mdclmeme")]
    public class JungleSolutionData : IConfigData
    {
        [Label("Jungle Solution enabled (requires reload):")]
        public bool JungleSolutionEnabled = true;

        public void ApplyChangedValues()
        {
            Items.Ammo.DarkGreenSolutionItem.Enabled = JungleSolutionEnabled;
        }
    }

    [Label("Spike Gun - Idea by u/pitchforkpopcornsale")]
    public class SpikeGunData : IConfigData
    {
        [Label("Spike Gun enabled (requires reload):")]
        public bool SpikeGunEnabled = true;

        [Label("Spike Gun damage:")]
        [Range(0, 999)]
        public int SpikeGunDamage = 30;

        [Label("Spike Gun knock back")]
        [Range(0f, 20f)]
        public float SpikeGunKnockBack = 1.5f;

        [Label("Spike Gun use delay:")]
        [Range(0, 999)]
        public int SpikeGunUseTime = 25;

        [Label("Spike Gun projectile spread angle:")]
        [Range(0, 360)]
        public int SpikeGunSpreadAngle = 8;

        [Label("Spike ammo bonus damage:")]
        [Range(0, 999)]
        public int SpikeAmmoBonusDamage = 5;

        [Label("Wooden Spike ammo bonus damage:")]
        [Range(0, 999)]
        public int WoodenSpikeAmmoBonusDamage = 15;

        public void ApplyChangedValues()
        {
            Items.Weapons.SpikeGun.Enabled = SpikeGunEnabled;
            Items.Weapons.SpikeGun.Damage = SpikeGunDamage;
            Items.Weapons.SpikeGun.KnockBack = SpikeGunKnockBack;
            Items.Weapons.SpikeGun.UseTime = SpikeGunUseTime;
            Items.Weapons.SpikeGun.SpreadAngle = SpikeGunSpreadAngle;
            Items.Ammo.SpikeAmmo.SpikeDamage = SpikeAmmoBonusDamage;
            Items.Ammo.SpikeAmmo.WoodenSpikeDamage = WoodenSpikeAmmoBonusDamage;
        }
    }
}
