using System;
using System.Collections.Generic;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Newtonsoft.Json;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using static Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic;
using Kingmaker.UnitLogic.ActivatableAbilities;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace EldritchArcana
{
    public class Deities
    {
        static Kingmaker.Blueprints.LibraryScriptableObject library => Main.library;
        static public BlueprintFeature Achaekek;
        static public BlueprintFeature Sai_proficiency;


        static internal void create()
        {

            //Achaekek 
            //Death, Evil, Law, Trickery, War 


            Achaekek = library.CopyAndAdd<BlueprintFeature>("04bc2b62273ab744092d992ed72bff41", "LamashtuFeature", "04bc2b72275ab744092d992ed72bff41");//rovagug

            Achaekek.SetNameDescriptionIcon("Achaekek",
                                            "Achaekek (pronounced uh-CHAY-kek) is the god of assassins, and the patron god of the Red Mantis assassins based on the island of Mediogalti. Achaekek takes a middle position between Calistria, the goddess of revenge if not necessarily murder, and Norgorber who is the god of all murder, whether paid for or not. His symbol is a pair of mantis claws depicted as if in prayer. \n"
                                            + "Domains: Death, Evil, Law, War, Trickery.\nFavored Weapon: Sai.",
                                            Image2Sprite.Create("Mods/EldritchArcana/sprites/Achaekek.png"));
            Sai_proficiency = library.CopyAndAdd<BlueprintFeature>("70ab8880eaf6c0640887ae586556a652", "SaiProficiency", "70ab8880eaf3c0640887ae586556a653");
            Sai_proficiency.SetNameDescription("Weapon Proficiency (Sai)",
                                                "You become proficient with Daggers and can use them as a weapon.");
            Sai_proficiency.ReplaceComponent<AddProficiencies>(a => a.WeaponProficiencies = new Kingmaker.Enums.WeaponCategory[] { Kingmaker.Enums.WeaponCategory.Sai});

            Achaekek.ReplaceComponent<AddFeatureOnClassLevel>(a => a.Feature = Sai_proficiency);
            Achaekek.ReplaceComponent<AddStartingEquipment>(a => a.CategoryItems = new Kingmaker.Enums.WeaponCategory[] { Kingmaker.Enums.WeaponCategory.Sai });
            Achaekek.ReplaceComponent<AddFacts>(a => a.Facts = new Kingmaker.Blueprints.Facts.BlueprintUnitFact[]
            {
                library.Get<BlueprintFeature>("a099afe1b0b32554199b230699a69525"), //Death
                library.Get<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c"), //evil
                library.Get<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8"), //law
                library.Get<BlueprintFeature>("eaa368e08628a8641b16cd41cbd2cb33"), //trickery
                library.Get<BlueprintFeature>("3795653d6d3b291418164b27be88cb43"), //war
                library.Get<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6"), //channel negative
            });
            

            var deities = library.Get<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            deities.AllFeatures = deities.AllFeatures.AddToArray(Achaekek);
            
        }
    }
}
