// Copyright (c) 2019 Jennifer Messerly
// This code is licensed under MIT license (see LICENSE for details)

using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Root;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Newtonsoft.Json;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace EldritchArcana
{
    static class DrawbackFeats
    {
        static LibraryScriptableObject library => Main.library;

        static BlueprintCharacterClass magus;

        ///internal static BlueprintParametrizedFeature spellPerfection;

        internal static void Load()
        {
            magus = library.Get<BlueprintCharacterClass>("45a4607686d96a1498891b3286121780");

            // Load metamagic feats
            //var metamagicFeats = SafeLoad(MetamagicFeats.CreateMetamagicFeats, "Metamagic feats")?.ToArray();
            var metamagicFeats = Array.Empty<BlueprintFeature>();
            var feats = metamagicFeats.ToList();

            // Add metamagics to Magus/Sorcerer bonus feat list.
            //var feats = ;
            //----------------------------drawbackfeats testing
            var disfeat = DrawbackFeats.CreateFrail();
            feats.Add(disfeat);
            var BasicFeatSelection = library.Get<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
            SelectFeature_Apply_Patch.onApplyFeature.Add(disfeat, (state, unit) =>
            {
                BasicFeatSelection.AddSelection(state, unit, 1);
                BasicFeatSelection.AddSelection(state, unit, 1);
            });
            //-------------------------
            // Add all feats (including metamagic, wizard discoveries) to general feats.
            library.AddFeats(feats.ToArray());
 
        }

        internal static T SafeLoad<T>(Func<T> load, String name) => Main.SafeLoad(load, name);




        static BlueprintFeature CreateFrail()
        {
            var fraily = new BlueprintComponent[64];
            for (int i = 1; i < 65; i++)
            {
                fraily[i - 1] = Helpers.CreateAddStatBonusOnLevel(StatType.HitPoints, i * -3, ModifierDescriptor.Penalty, i);
            };

            var feat = Helpers.CreateFeature("Frail", "Frail {Drawback}",
                "You have A frail body and you break bones easily also your health is not great.\n" +
                "if you pick a Drawback at level one you can choose an extra feat" +
                "\nMajor Drawback: You gain - 3 hit points.For every Hit Die you possess.",
                "0639446638b04ecc85e069e050751bfb",
                Helpers.NiceIcons(9),
                FeatureGroup.Feat,
                Helpers.Create<SavingThrowBonusAgainstDescriptor>(s => { s.SpellDescriptor = SpellDescriptor.Death; s.ModifierDescriptor = ModifierDescriptor.Feat; }),
                //Helpers.Create<FeyFoundlingLogic>(s => { s.dieModefier = 2;}),
                //Helpers.CreateAddStatBonusOnLevel(StatType.HitPoints,-3,ModifierDescriptor.Penalty,1),
                PrerequisiteCharacterLevelExact.Create(1));
            feat.AddComponents(fraily);
            return feat;
        }

        }
    }

    

