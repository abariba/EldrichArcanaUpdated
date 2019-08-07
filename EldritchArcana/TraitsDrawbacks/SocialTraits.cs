
using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class SocialTraits
    {

        public static BlueprintFeatureSelection CreateSocialTraits(out BlueprintFeatureSelection adopted)
        {
            var noFeature = Helpers.PrerequisiteNoFeature(null);
            var socialTraits = Helpers.CreateFeatureSelection("SocialTrait", "Social Trait",
                "Social traits focus on your character’s social class or upbringing.",
                "9e41e60c929e45bc84ded046148c07ec", null, FeatureGroup.None, noFeature);
            noFeature.Feature = socialTraits;
            var choices = new List<BlueprintFeature>();

            // This trait is finished by CreateRaceTraits.
            adopted = Helpers.CreateFeatureSelection("AdoptedTrait", "Adopted",
                "You were adopted and raised by someone not of your race, and raised in a society not your own.\nBenefit: As a result, you picked up a race trait from your adoptive parents and society, and may immediately select a race trait from your adoptive parents’ race.",
                "b4b37968273b4782b29d31c0ca215f41",
                Helpers.GetIcon("26a668c5a8c22354bac67bcd42e09a3f"), // Adaptability
                FeatureGroup.None);

            adopted.IgnorePrerequisites = true;
            adopted.Obligatory = true;
            choices.Add(adopted);

            choices.Add(Traits.CreateAddStatBonus("ChildOfTheStreetsTrait", "Child of the Streets",
                "You grew up on the streets of a large city, and as a result you have developed a knack for picking pockets and hiding small objects on your person.",
                "a181fd2561134715a04e1b05776ab7a3",
                StatType.SkillThievery));

            choices.Add(Traits.CreateAddStatBonus("FastTalkerTrait", "Fast-Talker",
                "You had a knack for getting yourself into trouble as a child, and as a result developed a silver tongue at an early age.",
                "509458a5ded54ecd9a2a4ef5388de2b7",
                StatType.SkillPersuasion));
            /*
            var summonedBow = library.Get<BlueprintWeaponType>("2fe00e2c0591ecd4b9abee963373c9a7");
            choices.Add(Helpers.CreateFeature("HairloomTrait", "Family Hairloom",
                "you inheritid this bow \nBenefit:you can shoot better with longbows",
                "e16eb56b2f964321a30086226dccb29e",
                Helpers.GetIcon("c3a66c1bbd2fb65498b130802d5f183a"), // DuelingMastery
                FeatureGroup.None,
                Helpers.Create<AddStartingEquipment>(a =>
                {
                    a.CategoryItems = new WeaponCategory[] {WeaponCategory.Longbow};
                    a.RestrictedByClass = Array.Empty<BlueprintCharacterClass>();
                    a.BasicItems = Array.Empty<BlueprintItem>();
                }),
                //Helpers.Create<WeaponAttackAndCombatManeuverBonus>(a => { a.WeaponType = duelingSword; a.AttackBonus = 1; a.Descriptor = ModifierDescriptor.Trait; }),
                Helpers.Create<WeaponAttackAndCombatManeuverBonus>(a => { a.WeaponType = summonedBow; a.AttackBonus = 1; a.Descriptor = ModifierDescriptor.Trait; })));*/
            
            var performanceResource = Traits.library.Get<BlueprintAbilityResource>("e190ba276831b5c4fa28737e5e49e6a6");
            choices.Add(Helpers.CreateFeature("MaestroOfTheSocietyTrait", "Maestro of the Society",
                "The skills of the greatest musicians are at your fingertips, thanks to the vast treasure trove of musical knowledge in the vaults you have access to.\nBenefit: You may use bardic performance 3 additional rounds per day.",
                "847cdf262e4147cda2c670db81852c58",
                Helpers.GetIcon("0d3651b2cb0d89448b112e23214e744e"),
                FeatureGroup.None,
                Helpers.Create<IncreaseResourceAmount>(i => { i.Resource = performanceResource; i.Value = 3; })));

            choices.Add(Traits.CreateAddStatBonus("SuspiciousTrait", "Suspicious",
                "You discovered at an early age that someone you trusted, perhaps an older sibling or a parent, had lied to you, and lied often, about something you had taken for granted, leaving you quick to question the claims of others.",
                "2f4e86a9d42547bc85b4c829a47d054c",
                StatType.SkillPerception));

            choices.Add(UndoSelection.Feature.Value);
            socialTraits.SetFeatures(choices);
            return socialTraits;
        }
    }
}
 