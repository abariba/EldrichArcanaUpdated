
using System;
using System.Collections.Generic;
using System.Linq;
using EldritchArcana.Helper;
using EldritchArcana.Objects;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Controllers.Combat;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.Common;
using Kingmaker.UI.ServiceWindow;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Parts;

namespace EldritchArcana
{
    internal class CombatTraits
    {
        public static BlueprintFeatureSelection CreateCombatTraits()
        {
            var noFeature = Helpers.PrerequisiteNoFeature(null);
            var combatTraits = Helpers.CreateFeatureSelection("CombatTrait", "Combat Trait",
                "Combat traits focus on martial and physical aspects of your character’s background.",
                "fab4225be98a4b3e9717883f22086c82", null, FeatureGroup.None, noFeature);
            noFeature.Feature = combatTraits;

            var choices = new List<BlueprintFeature>();

            var rageResource = Traits.library.Get<BlueprintAbilityResource>("24353fcf8096ea54684a72bf58dedbc9");

            var traitFeatures = new TraitFeature[]
            {
                new TraitFeature
                {
                    displayName = "Resilient",
                    description = "Growing up in a poor neighborhood or in the unforgiving wilds often forced you to subsist on food and water from doubtful sources.You’ve built up your constitution as a result.\nBenefit: You gain a + 1 trait bonus on Fortitude saves.",
                    Guid = "789d02217b6542ce8b0302249c86d49d",
                    icon = Helpers.GetIcon("79042cb55f030614ea29956177977c52"),
                    components = new BlueprintComponent[] { Helpers.CreateAddStatBonus(StatType.SaveFortitude, 1, ModifierDescriptor.Trait) }
                },
                new TraitFeature
                {
                    displayName= "Anatomist",
                    description="You have studied the workings of anatomy, either as a student at university or as an apprentice mortician or necromancer. You know where to aim your blows to strike vital organs.\nBenefit: You gain a +1 trait bonus on all rolls made to confirm critical hits.",
                    Guid="69245ef4b4ba44ddac917fc2aa10fbad",
                    icon=Image2Sprite.Create("Mods/EldritchArcana/sprites/anatomist.png"), // Improved Critical
                    components= new BlueprintComponent[] {Helpers.Create<CriticalConfirmationBonus>(a => { a.Bonus = 1; a.Value = 0; }) }
                },
                new TraitFeature
                {
                    displayName="Armor Expert",
                    description="You have worn armor as long as you can remember, either as part of your training to become a knight’s squire or simply because you were seeking to emulate a hero. Your childhood armor wasn’t the real thing as far as protection, but it did encumber you as much as real armor would have, and you’ve grown used to moving in such suits with relative grace.\nBenefit: When you wear armor of any sort, reduce that suit’s armor check penalty by 1, to a minimum check penalty of 0.",
                    Guid="94d526372a964b6db97c64291a3cb846",
                    icon=Helpers.GetIcon("3bc6e1d2b44b5bb4d92e6ba59577cf62"),
                    components=new BlueprintComponent[] { Helpers.Create<ArmorCheckPenaltyIncrease>(a => a.Bonus = 1) }
                },
                new TraitFeature
                {
                    displayName="Berserker of the Society",
                    description="Your time spent as a society member has taught you new truths about the origins of the your rage ability.\nBenefit: You may use your rage ability for 3 additional rounds per day.",
                    Guid="8acfcecfed05442594eed93fe448ab3d",
                    icon=Helpers.GetIcon("1a54bbbafab728348a015cf9ffcf50a7"), // Extra Rage
                    components=new BlueprintComponent[] { rageResource.CreateIncreaseResourceAmount(3) }
                },
                new TraitFeature
                {
                    displayName="Blade of the Society",
                    description="You have studied and learned the weak spots of many humanoids and monsters." +
                                "\nBenefit: you are able to sneak attack at level 3 even if your class normaly does not and You gain a +1 trait bonus to damage rolls from sneak attacks.",
                    Guid="ff8c90626a58436997cc41e4b121be9a",
                    icon= Helpers.GetIcon("9f0187869dc23744292c0e5bb364464e"),
                    components=new BlueprintComponent[] {
                        Helpers.Create<AdditionalDamageOnSneakAttack>(a => a.Value = 1),
                        Helpers.CreateAddStatBonusOnLevel(StatType.SneakAttack, 1, ModifierDescriptor.Trait, 3)
                    }
                },
                new TraitFeature
                {
                    displayName="Defender of the Society",
                    description="Your time spent fighting and studying the greatest warriors of the society has taught you new defensive skills while wearing armor.\nBenefit: You gain a +1 trait bonus to Armor Class when wearing medium or heavy armor.",
                    Guid="545bf7e13346473caf48f179083df894",
                    icon = Helpers.GetIcon("7dc004879037638489b64d5016997d12"), // Armor Focus Medium
                    components=new BlueprintComponent[] {
                        Helpers.Create<ArmorFocus>(a => a.ArmorCategory = ArmorProficiencyGroup.Medium),
                        Helpers.Create<ArmorFocus>(a => a.ArmorCategory = ArmorProficiencyGroup.Heavy)
                    }
                },
                new TraitFeature
                {
                    displayName="Deft Dodger",
                    description= "Growing up in a rough neighborhood or a dangerous environment has honed your senses.\nBenefit: You gain a +1 trait bonus on Reflex saves.",
                    Guid="7b57d86503314d32b753f77909c909bc",
                    icon=Helpers.GetIcon("15e7da6645a7f3d41bdad7c8c4b9de1e"), // Lightning Reflexes
                    components=new BlueprintComponent[] {Helpers.CreateAddStatBonus(StatType.SaveReflex, 1, ModifierDescriptor.Trait) }
                }

            };
            foreach (var traitFeature in traitFeatures)
            {
                choices.Add(Helpers.CreateFeature(traitFeature.name, traitFeature.displayName, traitFeature.description, traitFeature.Guid, traitFeature.icon, traitFeature.group, traitFeature.components));
            }

            //Util.WriteToJsonFile(traitFeatures, @"Mods\EldritchArcana\traitFeatures");







            choices.Add(Helpers.CreateFeature("DirtyFighterTrait", "Dirty Fighter",
                "You wouldn’t have lived to make it out of childhood without the aid of a sibling, friend, or companion you could always count on to distract your enemies long enough for you to do a little bit more damage than normal. That companion may be another PC or an NPC (who may even be recently departed from your side).\n" +
                "Benefit: When you hit a foe you are flanking, you deal 1 additional point of damage (this damage is added to your base damage, and is multiplied on a critical hit). This additional damage is a trait bonus.",
                "ac47c14063574a0a9ea6927bf637a02a",
                Helpers.GetIcon("5662d1b793db90c4b9ba68037fd2a768"), // precise strike
                FeatureGroup.None,
                DamageBonusAgainstFlankedTarget.Create(1)));

            var kiPowerResource = Traits.library.Get<BlueprintAbilityResource>("9d9c90a9a1f52d04799294bf91c80a82");
            choices.Add(Helpers.CreateFeature("HonoredFistOfTheSocietyTrait", "Honored First of the Society",
                "You have studied dozens of ancient texts on martial arts that only the Society possesses, and are more learned in these arts than most.\nBenefit: You increase your ki pool by 1 point.",
                "ee9c230cbbc2484084af61ac97e47e72",
                Helpers.GetIcon("7dc004879037638489b64d5016997d12"), // Armor Focus Medium
                FeatureGroup.None,
                kiPowerResource.CreateIncreaseResourceAmount(1)));

            // TODO: Killer

            choices.Add(Helpers.CreateFeature("ReactionaryTrait", "Reactionary",
                "You were bullied often as a child, but never quite developed an offensive response. Instead, you became adept at anticipating sudden attacks and reacting to danger quickly.\nBenefit: You gain a +2 trait bonus on initiative checks.",
                "fa2c636580ee431297de8806a046044a",
                Helpers.GetIcon("797f25d709f559546b29e7bcb181cc74"), // Improved Initiative
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.Initiative, 2, ModifierDescriptor.Trait)));

            choices.Add(Traits.CreateAddStatBonus("RecklessTrait", "Reckless",
                "You have a tendency for rash behavior, often disregarding your own safety as you move across the battlefield.",
                "edb2f4d0c2c34c7baccad11f2b5bfbd4",
                StatType.SkillMobility));




            choices.Add(Helpers.CreateFeature("ResilientTrait", "Resilient",
                "Growing up in a poor neighborhood or in the unforgiving wilds often forced you to subsist on food and water from doubtful sources. You’ve built up your constitution as a result.\nBenefit: You gain a +1 trait bonus on Fortitude saves.",
                "789d02217b6542ce8b0302249c86d49d",
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.SaveFortitude, 1, ModifierDescriptor.Trait)));

            choices.Add(Traits.CreateAddStatBonus("WittyReparteeTrait", "Witty Repartee",
                "You are quick with your tongue and have always possessed the talent to quickly admonish your enemies.",
                "c6dbc457c5de40dbb4cb9fe4d7706cd9",
                StatType.SkillPersuasion));

            combatTraits.SetFeatures(choices);
            return combatTraits;
        }
    }
}