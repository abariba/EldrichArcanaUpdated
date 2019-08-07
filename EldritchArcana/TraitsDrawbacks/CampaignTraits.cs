
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
    internal class CampaignTraits
    {
        public static BlueprintFeatureSelection CreateCampaignTraits()
        {
            var noFeature = Helpers.PrerequisiteNoFeature(null);
            var campaignTraits = Helpers.CreateFeatureSelection("CampaignTrait", "Campaign Trait",
                "Campaign traits are specifically tailored to relate to the Kingmaker campaign.",
                "f3c611a76bbc482c9c15219fa982fa17", null, FeatureGroup.None, noFeature);
            noFeature.Feature = campaignTraits;

            var choices = new List<BlueprintFeature>();
            choices.Add(Helpers.CreateFeature("BastardTrait", "Bastard",
                "One of your parents was a member of one of the great families of Brevoy, perhaps even of the line of Rogarvia itself. Yet you have no substantive proof of your nobility, and you’ve learned that claiming nobility without evidence makes you as good as a liar. While you might own a piece of jewelry, a scrap of once-rich fabric, or an aged confession of love, none of this directly supports your claim. Thus, you’ve lived your life in the shadow of nobility, knowing that you deserve the comforts and esteem of the elite, even though the contempt of fate brings you nothing but their scorn. Whether a recent attempt to prove your heritage has brought down the wrath of a noble family’s henchmen or you merely seek to prove the worth of the blood in your veins, you’ve joined an expedition into the Stolen Lands, hoping to make a name all your own. You take a –1 penalty on all Charisma-based skill checks made when dealing with members of Brevic nobility but gain a +1 trait bonus on Will saves as a result of your stubbornness and individuality. (The penalty aspect of this trait is removed if you ever manage to establish yourself as a true noble.)",
                "d4f7e0915bd941cbac6f655927135817",
                Helpers.GetIcon("175d1577bb6c9a04baf88eec99c66334"), // Iron Will
                FeatureGroup.None,
                Helpers.Create<PrerequisiteFeature>(p => p.Feature = Helpers.human),
                // Other than the Prologue, there aren't many persuasion checks against members of the
                // nobility, prior to becoming a Baron. For simplicity, we simply remove the penalty after level 2.
                // (Ultimately this trait is more for RP flavor than anything.)
                Helpers.CreateAddStatBonusOnLevel(StatType.SkillPersuasion, -1, ModifierDescriptor.Penalty, 1, 2),
                Helpers.CreateAddStatBonus(StatType.SaveWill, 1, ModifierDescriptor.Trait)));

            var Outlander = Helpers.CreateFeatureSelection("OutlanderTrait", "Outlander",
                "You’ve recently come from somewhere else and are hoping to make your fortune here.\nChoose one of the following",
                "40DABEF7A6424982BC42CD39D8440029",
                Helpers.GetIcon("175d1577bb6c9a04baf88eec99c66334"), // Iron Will
                FeatureGroup.None);



            var NobleDescription = "You claim a tangential but legitimate connection to one of Brevoy’s noble families. If you aren’t human, you were likely adopted by one of Brevoy’s nobles or were instead a favored servant or even a childhood friend of a noble scion. Whatever the cause, you’ve had a comfortable life, but one far from the dignity and decadence your distant cousins know. Although you are associated with an esteemed name, your immediate family is hardly well to do, and you’ve found your name to be more of a burden to you than a boon in many social situations. You’ve recently decided to test yourself, to see if you can face the world without the aegis of a name you have little real claim or care for. An expedition into the storied Stolen Lands seems like just the test to see if you really are worth the title “noble.”";
            var NobleFamilyBorn = Helpers.CreateFeatureSelection("NobleFamilyBornTrait", "Noble born",
                NobleDescription + "\nBenefits: Select one of the following Royal family Traits: Garess(mountainpeople +5movement), Lebeda (are highly educated+int),Orlovsky(Defensive+AC),Lodovka(+2 atletics), or Surtova(you poison the opposition +sneakattack).",
                "ecacfcbeddfe453cafc8d60fc2fb5d45",
                Helpers.GetIcon("3adf9274a210b164cb68f472dc1e4544"), // Human Skilled
                FeatureGroup.None);


            var Orlovski = Helpers.CreateFeatureSelection("Noble family Orlovski Trait", "Noble family — Orlovski",
                "Their motto is 'High Above' \n" +
                "House Orlovsky controls northeastern Brevoy from Eagle's Watch on Mount Veshka. They try to rise above petty political maneuvers. As staunch allies of the now disappeared House Rogarvia, this has landed them in a prickly situation." +
                "\n Benefit: have +1 CMD and you can choose one of few skills to increase with one.",
                Helpers.MergeIds(Helpers.getGuids(StatType.AC), "9b03b7ff17394007a3fbec18bb42604b"),
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.AdditionalCMD, 1, ModifierDescriptor.Trait));
            

            var OrlovskiFamilyFeats = new StatType[] {

                 StatType.SkillAthletics,
                 StatType.SkillPersuasion,
                 StatType.SkillStealth,
                 //StatType.BaseAttackBonus,
                 //StatType.SneakAttack
             }.Select(skill => Traits.CreateAddStatBonus(
                $"Orlovsky{skill}Trait",
                $"{skill}",
                Orlovski.GetDescription(),
                Helpers.MergeIds(Helpers.GetSkillFocus(skill).AssetGuid, "2b01b7ff17394007a3fbec18bb42203b"),
                skill)).ToArray();
            
            //"0b183a3acaf5464eaad54276413fec08"
           

            var Lebda = Helpers.CreateFeatureSelection("Noble family Lebeda Trait", "Noble family — Lebeda",
                "family motto 'Success through Grace.'\n" +
                "House Lebeda is based to the southwest of Lake Reykal in Brevoy, controlling the plains and significant portions of the lake's shipping.[1] They are considered to be the Brevic noble family that epitomizes Rostland, having significant Taldan blood, an appreciation for fine things, and a love of sword fighting." +
                "\nBenefit:+1 knowledge arcana" +
                "\nBefefit:select a recource for a usable ability and you are able to use it more",
                Helpers.MergeIds(Helpers.getGuids(StatType.Intelligence), "9b03b7ff17394007a3fbec18bb42604c"),
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.SkillKnowledgeArcana, 1, ModifierDescriptor.Trait));
                //

            
            // var families = new List<BlueprintFeature>() { }
            //choices.Add( Helpers.CreateAddStatBonus(
            //Orlovski.SetFeatures(OrlovskiFeatures);
            Orlovski.SetFeatures(OrlovskiFamilyFeats);

            var duelingSword = Traits.library.Get<BlueprintWeaponType>("a6f7e3dc443ff114ba68b4648fd33e9f");
            var longsword =Traits.library.Get<BlueprintWeaponType>("d56c44bc9eb10204c8b386a02c7eed21");

            var layonhandsResource = Traits.library.Get<BlueprintAbilityResource>("9dedf41d995ff4446a181f143c3db98c");
            var MutagenResource = Traits.library.Get<BlueprintAbilityResource>("3b163587f010382408142fc8a97852b6");
            var JudgmentResource = Traits.library.Get<BlueprintAbilityResource>("394088e9e54ccd64698c7bd87534027f");
            var ItemBondResource = Traits.library.Get<BlueprintAbilityResource>("fbc6de6f8be4fad47b8e3ec148de98c2");
            var kiPowerResource = Traits.library.Get<BlueprintAbilityResource>("9d9c90a9a1f52d04799294bf91c80a82");
            var ArcanePoolResourse = Traits.library.Get<BlueprintAbilityResource>("effc3e386331f864e9e06d19dc218b37");
            var ImpromptuSneakAttackResource = Traits.library.Get<BlueprintAbilityResource>("78e6008db60d8f94b9196464983ad336");
            var WildShapeResource = Traits.library.Get<BlueprintAbilityResource>("ae6af4d58b70a754d868324d1a05eda4");
            var SenseiPerformanceResource = Traits.library.Get<BlueprintAbilityResource>("ac5600c9642692145b7eb4553a703c1a");

            var snowball =Traits.library.Get<BlueprintAbility>("9f10909f0be1f5141bf1c102041f93d9");
            var fireball =Traits.library.Get<BlueprintAbility>("2d81362af43aeac4387a3d4fced489c3");
            var alchemist =Traits.library.Get<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var bard =Traits.library.Get<BlueprintCharacterClass>("772c83a25e2268e448e841dcd548235f");
            var cleric =Traits.library.Get<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var druid =Traits.library.Get<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var scion =Traits.library.Get<BlueprintCharacterClass>("f5b8c63b141b2f44cbb8c2d7579c34f5");
            var magus =Traits.library.Get<BlueprintCharacterClass>("45a4607686d96a1498891b3286121780");
            var paladin =Traits.library.Get<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var sorcerer =Traits.library.Get<BlueprintCharacterClass>("b3a505fb61437dc4097f43c3f8f9a4cf");
            var ranger =Traits.library.Get<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
            var wizard =Traits.library.Get<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var oracle =Traits.library.Get<BlueprintCharacterClass>("ec73f4790c1d4554871b81cde0b9399b");
            var rogue =Traits.library.Get<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");

            var LebdaFeatures = new List<BlueprintFeature>() { };
            var Resources = new List<BlueprintAbilityResource> { kiPowerResource, MutagenResource, ItemBondResource , ImpromptuSneakAttackResource, JudgmentResource, ArcanePoolResourse, SenseiPerformanceResource };
            //CreateIncreaseResourceAmount for a few different resources
            int x = 0;
            int y = 1;
            //Resources.randomelement();
            foreach (var stat in Resources)
            {
                x++;
                y = x < 3 ? 1 : x-3;
                if(y == 0) y = 1;
                LebdaFeatures.Add(Helpers.CreateFeature($"Noble family {stat} Trait", $"Gain {y} extra {stat}",
                $"you are a resourcefull family becouse of this you " +
                $"gain {y} extra uses of the '{stat}' resource. ",
                Helpers.MergeIds(stat.AssetGuid, "9b03b7ff17394007a3fbec18bb42604c"),
                Helpers.GetIcon(stat.AssetGuid), //
                FeatureGroup.None,
                stat.CreateIncreaseResourceAmount(y)));
                
            };
            Lebda.SetFeatures(LebdaFeatures);

             var hoi = new List<BlueprintFeature>() {
                //family medyved
                Helpers.CreateFeature("Noble family none Trait", "Noble family — Medvyed",
                NobleDescription+" and your family made to deal with fey creatures so people from your family can cure each other better \n Benefit: you can use lay on hands 4 times more often",
                Helpers.MergeIds(Helpers.getGuids(StatType.AdditionalCMB), "9b03b7ff17394007a3fbec18bb42604b"),
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                layonhandsResource.CreateIncreaseResourceAmount(4)),
                
                //family lodovka + atletics and snowball
                Helpers.CreateFeature("Noble family Lodovka Trait", "Noble family — Lodovka",
                "House Lodovka is a noble family of Brevoy with their headquarters on Acuben Isle on the Lake of Mists and Veils."+
                "They have traditionally been a power on the lake.Led by Lord Kozek Lodovka, both their fleet size and influence along the lake continue to increase."+
                "The fleet primarily catches fish and freshwater crabs."+
                "The house's crest includes a green crab climbing out of the water towards a gray tower/keep. Their motto is 'The Waters, Our Fields'" +
                "\n Benefit:+ 2 atletics and atlecics, and if you are a caster you know the spell snowball",
                Helpers.MergeIds(Helpers.getGuids(StatType.BaseAttackBonus), "9b03b7ff17394007a3fbec18bb42604b"),
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddKnownSpell(snowball,wizard,0),
                Helpers.CreateAddKnownSpell(snowball,alchemist,1),
                Helpers.CreateAddKnownSpell(snowball,bard,1),
                Helpers.CreateAddKnownSpell(snowball,cleric,1),
                Helpers.CreateAddKnownSpell(snowball,druid,1),
                Helpers.CreateAddKnownSpell(snowball,scion,1),
                Helpers.CreateAddKnownSpell(snowball,magus,1),
                Helpers.CreateAddKnownSpell(snowball,paladin,1),
                Helpers.CreateAddKnownSpell(snowball,sorcerer,1),
                Helpers.CreateAddKnownSpell(snowball,ranger,1),
                Helpers.CreateAddKnownSpell(snowball,wizard,1),
                Helpers.CreateAddKnownSpell(snowball,oracle,1),
                Helpers.CreateAddKnownSpell(snowball,rogue,1),
                //Helpers.adc
                Helpers.CreateAddStatBonus(StatType.SkillAthletics,2,ModifierDescriptor.Trait)),
                //family garess + 5 mvnt spd
                Helpers.CreateFeature("Noble family Garess Trait", "Noble family — Garess",
                "famly motto:'Strong as the Mountains'\n" +
                "House Garess is based in the western part of Brevoy, in the foothills of the Golushkin Mountains." +
                "House Garess' crest is that of a snow-capped mountain peak in gray set against a dark blue field. There is a silvery crescent moon in the upper right corner, and there is a black hammer across the base of the peak. The Houses' motto is Strong as the Mountains."+
                "House Garess had a good relationship with the Golka dwarves until the dwarves vanished. Members of the house worked the metal that the dwarves mined."+
                "The House has built several strongholds, Highdelve and Grayhaven, in the Golushkin Mountains. \n Benefit: Your movementspeed is 5ft faster",
                Helpers.MergeIds(Helpers.getGuids(StatType.Reach), "9b03b7ff17394007a3fbec18bb42604b"),
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.Speed,5,ModifierDescriptor.Trait)),
                //family rogarvia
                Helpers.CreateFeature("Noble family Rogarvia Trait", "Noble family — Rogarvia",
                "famly motto:'With Sword and Flame.'\n" +
                "The former ruling house of Brevoy, House Rogarvia was founded by the descendants of Choral the Conqueror and Myrna Rogarvia, daughter of Nikos Surtova. Choral united Rostland and Issia into the kingdom of Brevoy after invading from Iobaria, accompanied by dragons. Most members of the House, including King Urzen Rogarvia, disappeared mysteriously in 4699 AR, in an event called the Vanishing. Their loss is not greatly mourned by the Brevic people and loyalists are calling for an investigation instead of blind allegiance to Noleski Surtova, who declared himself king."+
                "The Rogarvians were known to be ruthless rulers who did their best to hold Brevoy's disparate houses and factions together."+
                "A two-headed red dragon is the family's crest. One head of the dragon breathes fire while the other wields a sword. The house motto is 'With Sword and Flame.'" +
                "\nDrawback:-10 on persuasion checks vs nobles(they hate you)" +
                "\nBenefit:you know the spell fireball if you are any caster",
                "B48B8234942C4FD191E99721728BF49D",
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddStatBonusOnLevel(StatType.SkillPersuasion, -10, ModifierDescriptor.Penalty, 1, 3),
                Helpers.CreateAddKnownSpell(fireball,sorcerer,0),
                Helpers.CreateAddKnownSpell(fireball,wizard,3),
                Helpers.CreateAddKnownSpell(fireball,alchemist,3),
                Helpers.CreateAddKnownSpell(fireball,bard,3),
                Helpers.CreateAddKnownSpell(fireball,cleric,3),
                Helpers.CreateAddKnownSpell(fireball,druid,3),
                Helpers.CreateAddKnownSpell(fireball,scion,3),
                Helpers.CreateAddKnownSpell(fireball,magus,3),
                Helpers.CreateAddKnownSpell(fireball,paladin,3),
                Helpers.CreateAddKnownSpell(fireball,sorcerer,3),
                Helpers.CreateAddKnownSpell(fireball,ranger,3),
                Helpers.CreateAddKnownSpell(fireball,wizard,3),
                Helpers.CreateAddKnownSpell(fireball,oracle,3),
                Helpers.CreateAddKnownSpell(fireball,rogue,3)),

                
                /*/ family lebda
                Helpers.CreateFeature("Noble family Lebeda Trait", "Noble family — Lebeda",
                "family motto 'Success through Grace.'\n" +
                "House Lebeda is based to the southwest of Lake Reykal in Brevoy, controlling the plains and significant portions of the lake's shipping.[1] They are considered to be the Brevic noble family that epitomizes Rostland, having significant Taldan blood, an appreciation for fine things, and a love of sword fighting." +
                "\nBenefit:+1 knowledge arcana" +
                "\nBefefit:if you prepare mutagens you prepare one extra",
                Helpers.MergeIds(Helpers.getGuids(StatType.Intelligence), "9b03b7ff17394007a3fbec18bb42604c"),
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.SkillKnowledgeArcana,1,ModifierDescriptor.Trait),
                MutagenResource.CreateIncreaseResourceAmount(1)),
                */
                //family khartorov
                Helpers.CreateFeature("Noble family Khavortorov Trait", "Noble family — Khavortorov",
                "famly Motto:'We Won't Be Saddled'\n" +
                "are a hot-tempered family that has produced knights for many generations.\n " +
                "They are trying to better establish themselves as a great house of Brevoy now that their lieges, the Rogarvias, have disappeared. \n" +
                "Their crest is a white dragon with a helmet embedded in its chest. Many of the Khavortorov's are experts with the Aldori dueling sword.\n" +
                "Benefit:dueling sword and longsword deal 1 extra damage and you start with one of both",
                "44DFCE0451FC4188A06E2184EF65064B",
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.Create<AddStartingEquipment>(a =>
                {
                    
                    a.CategoryItems = new WeaponCategory[] { WeaponCategory.DuelingSword, WeaponCategory.Longsword };
                    a.RestrictedByClass = Array.Empty<BlueprintCharacterClass>();
                    a.BasicItems = Array.Empty<BlueprintItem>();
                    
                }),
                Helpers.Create<WeaponTypeDamageBonus>(a => { a.WeaponType = duelingSword; a.DamageBonus = 1; }),
                //Helpers.Create<WeaponConditionalDamageDice>(a => {a.CheckWielder = null })
                Helpers.Create<WeaponTypeDamageBonus>(a => { a.WeaponType = longsword; a.DamageBonus = 1; })),
                // family surtova
                Helpers.CreateFeature("Noble family Surtova Trait", "Noble family — Surtova",
                "famly Motto:'Ours Is the Right,'\n" +
                "House Surtova is the current ruling family of Brevoy is the oldest Brevic noble family and the most influential. Their original holdings are the environs of Port Ice in northern Issia on the shores of the Lake of Mists and Veils. Their claim to the throne is linked to Nikos Surtova giving the hand of his daughter, Myrna Surtova, to Choral the Conqueror in marriage. This marriage allowed the house to keep its power as a staunch ally of House Rogarvia. In 4699 AR, during the Vanishing, House Surtova was able to use its high position to immediately claim regency until the Rogarvia's returned."+
                "The Surtovans are known as careful and cunning diplomats. Before Choral the Conqueror invaded, the Surtovans were known as pirates and raiders, and the family still has many connections with the pirates and brigands of the region, many of whom are distant relations of the Surtova clan. One of the more active pirates of the Lake of Mists and Veils, Captain Vali Dobos, is rumoured to have a close connection with the Surtova's, although he keeps his lineage hidden."+
                "Their family motto is 'Ours Is the Right,' which likely reflects their belief in a right to rulership of Brevoy since their family formerly ruled Issia as a group of crafty pirate-kings. Their crest is a gray ship in front of fields of blue on the lower half and black with silver stars on the upper half." +
                "\nBenefit:+2 damage against flanked targets",
                Helpers.MergeIds(Helpers.getGuids(StatType.SneakAttack), "9b03b7ff17394007a3fbec18bb42604c"),
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.Create<DamageBonusAgainstFlankedTarget>(a => a.Bonus = 2))
                //
            };

            hoi.Add(Orlovski);
            hoi.Add(Lebda);
            NobleFamilyBorn.SetFeatures(hoi);
            choices.Add(NobleFamilyBorn);


            var miscdes = "Nobles think about you but they don't know:\n";
            choices.Add(Helpers.CreateFeatureSelection("NobleBornTrait", "Noble Born(Human)",
                miscdes + "You claim a tangential but legitimate connection to one of Brevoy’s noble families. you’ve had a comfortable life, one you exploited untill you where send off to the be a monk and your luxury life ended.\nBenefits:you will start out with a bab penalty that will become a massive boon if you live the tale starts at -2 ends at +4",
                "a820521d923f4e569c3c69d091bf8865",
                Helpers.GetIcon("3adf9274a210b164cb68f472dc1e4544"), // Human Skilled
                FeatureGroup.None,
                PrerequisiteCharacterLevelExact.Create(10),
                Helpers.CreateAddStatBonusOnLevel(StatType.BaseAttackBonus, -2, ModifierDescriptor.Penalty, 1, 5),
                Helpers.CreateAddStatBonusOnLevel(StatType.BaseAttackBonus, -1, ModifierDescriptor.Penalty, 6, 10),
                //Helpers.CreateAddStatBonusOnLevel(StatType.AC, 1, ModifierDescriptor.Trait, 10,15),
                Helpers.CreateAddStatBonusOnLevel(StatType.BaseAttackBonus, 1, ModifierDescriptor.Trait, 11, 13),
                Helpers.CreateAddStatBonusOnLevel(StatType.BaseAttackBonus, 2, ModifierDescriptor.Trait, 14, 15),
                //Helpers.CreateAddStatBonusOnLevel(StatType.AC, 1, ModifierDescriptor.Trait, 15),
                Helpers.CreateAddStatBonusOnLevel(StatType.BaseAttackBonus, 3, ModifierDescriptor.Trait, 16, 17),
                Helpers.CreateAddStatBonusOnLevel(StatType.BaseAttackBonus, 4, ModifierDescriptor.Trait, 18)
                ));

            var SpellExpertise = Helpers.CreateFeatureSelection("OutlanderMissionary", "Outlander:Missionary",
                "You have come here to see about expanding the presence of your chosen faith after receiving visions that told you your faith is needed—what that need is, though, you’re not quite sure." +
                "\nBenefit: Pick three spells when you choose this trait—from this point on, whenever you cast that spell, you do so at caster +1 level. and +1 dc and lore religion +1",
                "6a3dfe274f45432b85361bdbb0a3009b",
                Helpers.GetIcon("fe9220cdc16e5f444a84d85d5fa8e3d5"), // Spell Specialization Progression
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.SkillLoreReligion, 1, ModifierDescriptor.Trait));
            Traits.FillSpellSelection(SpellExpertise, 1, 9, Helpers.Create<IncreaseCasterLevelForSpellMax>());
            //choices.Add(SpellExpertise);
            /*var newthiny = CreateFeature("OutlanderLoreseeker", "Outlander:Loreseeker",
                "You have come here to see about expanding the presence of your chosen faith after receiving visions that told you your faith is needed—what that need is, though, you’re not quite sure.\nBenefit: Pick one spell when you choose this trait—from this point on, whenever you cast that spell, you do so at caster level max.",
                "6a3dfe274f45432b85361bdbb0a3010c",
                Helpers.GetIcon("fe9220cdc16e5f444a84d85d5fa8e3d5");*/
            var SpellExpertise2 = Helpers.CreateFeatureSelection("OutlanderLoreseeker", "Outlander:Loreseeker",
                "The secrets of ancient fallen civilizations intrigue you, particularly magical traditions. You’ve studied magic intensely, and hope to increase that knowledge by adding lost lore. You’ve come to pursue that study, and chose this place as your base because it was out of the way of bigger cities—meaning less competition to study the ancient monuments in the region, you hope!" +
                ".\nBenefit: Pick three spells when you choose this trait—from this point on, whenever you cast that spell, you do so at caster level +1 and dc +1." +
                "\nBenefit2:lore arcana + 1",
                "6a3dfe274f45432b85361bdbb0a3010c",
                Helpers.GetIcon("fe9220cdc16e5f444a84d85d5fa8e3d5"), // Spell Specialization Progression
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.SkillKnowledgeArcana, 1, ModifierDescriptor.Trait));
            //FillSpellSelection(SpellExpertise2, 1, 9, Helpers.Create<IncreaseCasterLevelForSpellMax>(), Helpers.Create<IncreaseSpellDC>());
            var spellchoises = Traits.FillTripleSpellSelection(SpellExpertise2, 1, 9, Helpers.Create<IncreaseCasterLevelForSpellMax>());
            SpellExpertise2.SetFeatures(spellchoises);
            SelectFeature_Apply_Patch.onApplyFeature.Add(SpellExpertise2, (state, unit) =>
            {
                SpellExpertise2.AddSelection(state, unit, 1);
                SpellExpertise2.AddSelection(state, unit, 1);
            });
            SelectFeature_Apply_Patch.onApplyFeature.Add(SpellExpertise, (state, unit) =>
            {
                SpellExpertise.AddSelection(state, unit, 1);
                SpellExpertise.AddSelection(state, unit, 1);
            });
            //SpellExpertise2.
            //SpellExpertise2.SetFeatures(SpellExpertise2.Features);
            //FillSpellSelection(SpellExpertise2, 3, 3, Helpers.Create<IncreaseCasterLevelForSpell>(), Helpers.Create<IncreaseSpellDC>());
            //var ding1 = Helpers.CreateAddStatBonus(StatType.SkillKnowledgeArcana, 1, ModifierDescriptor.Trait);
            //FillSpellSelection(ding1, 1, 9, Helpers.Create<IncreaseCasterLevelForSpellMax>());
            //choices.Add(SpellExpertise2);

            //new BlueprintComponent = Helpers.Cre

            var OutlanderFeatures = new List<BlueprintFeature>()
            {
                SpellExpertise2,
                SpellExpertise,
                Helpers.CreateFeature("OutlanderExile", "Outlander:exile",
                "For whatever reason, you were forced to flee your homeland. Chance or fate has brought you here, and it’s here that your money ran out, leaving you stranded in this small town. You are also being pursued by enemies from your homeland, and that has made you paranoid and quick to react to danger.\nBenefit: You gain a +2 trait bonus on initiative checks.",
                "fa2c636580ee431297de8806a046054a",
                Helpers.GetIcon("797f25d709f559546b29e7bcb181cc74"), // Improved Initiative
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.Initiative, 2, ModifierDescriptor.Trait))
            };
            //FillSpellSelection(OutlanderFeatures, 1, 9, Helpers.Create<IncreaseCasterLevelForSpellMax>());
            Outlander.SetFeatures(OutlanderFeatures);
            //Outlander.AddSelection(1,SpellExpertise,1);
            choices.Add(Outlander);

            /*
            
            /* TODO: Noble Born. This will require some adaptation to the game. *
            var nobleBorn = Helpers.CreateFeatureSelection("NobleBornTrait", "Noble Born",
                "You claim a tangential but legitimate connection to one of Brevoy’s noble families. If you aren’t human, you were likely adopted by one of Brevoy’s nobles or were instead a favored servant or even a childhood friend of a noble scion. Whatever the cause, you’ve had a comfortable life, but one far from the dignity and decadence your distant cousins know. Although you are associated with an esteemed name, your immediate family is hardly well to do, and you’ve found your name to be more of a burden to you than a boon in many social situations. You’ve recently decided to test yourself, to see if you can face the world without the aegis of a name you have little real claim or care for. An expedition into the storied Stolen Lands seems like just the test to see if you really are worth the title “noble.”",
                "a820521d923f4e569c3c69d091bf8865",
                null,
                FeatureGroup.None);
            choices.Add(nobleBorn);
            /*
            var families = new List<BlueprintFeature>();
            // TODO: Garess, Lebeda are hard to adapt to PF:K, need to invent new bonuses.
            // Idea for Garess:
            // - Feather Step SLA 1/day?
            // Lebeda:
            // - Start with extra gold? Or offer a permanent sell price bonus (perhaps 5%?)
            //
            families.Add();
            */
                                                                      
            choices.Add(Helpers.CreateFeature("RostlanderTrait", "Rostlander",
                "You were raised in the south of Brevoy, a land of dense forests and rolling plains, of crystalline rivers and endless sapphire skies. You come from hearty stock and were raised with simple sensibilities of hard work winning well-deserved gains, the importance of charity and compassion, and the value of personal and familial honor. Yours is the country of the Aldori swordlords and the heroes who refused to bend before the armies of a violent conqueror. You care little for matters of politics and nobles or of deception and schemes. As you are thoroughly Brevic, the call for champions willing to expand your land’s influence into the Stolen Lands has inflamed your sense of patriotism and honor, and so you have joined an expedition to quest southward. Your hardy nature grants you a +1 trait bonus on all Fortitude saves.",
                "d99b9398af66406cac173884df308eb7",
                Helpers.GetIcon("79042cb55f030614ea29956177977c52"), // Great Fortitude
                FeatureGroup.None,
                Helpers.CreateAddStatBonus(StatType.SaveFortitude, 1, ModifierDescriptor.Trait)));

            var summonedBow =Traits.library.Get<BlueprintItem>("2fe00e2c0591ecd4b9abee963373c9a7");
            //var duelingSword =Traits.library.Get<BlueprintWeaponType>("a6f7e3dc443ff114ba68b4648fd33e9f");
            //var longsword =Traits.library.Get<BlueprintWeaponType>("d56c44bc9eb10204c8b386a02c7eed21");

            choices.Add(Helpers.CreateFeature("SwordScionTrait", "Sword Scion",
                "You have lived all your life in and around the city of Restov, growing up on tales of Baron Sirian Aldori and the exploits of your home city’s heroic and legendary swordlords. Perhaps one of your family members was an Aldori swordlord, you have a contact among their members, or you have dreamed since childhood of joining. Regardless, you idolize the heroes, styles, and philosophies of the Aldori and have sought to mimic their vaunted art. Before you can petition to join their ranks, however, you feel that you must test your mettle. Joining an expedition into the Stolen Lands seems like a perfect way to improve your skills and begin a legend comparable to that of Baron Aldori. You begin play with a longsword or Aldori dueling sword and gain a +1 trait bonus on all attacks and combat maneuvers made with such weapons.",
                "e16eb56b2f964321a29076226dccb29e",
                Helpers.GetIcon("c3a66c1bbd2fb65498b130802d5f183a"), // DuelingMastery
                FeatureGroup.None,
                Helpers.Create<AddStartingEquipment>(a =>
                {
                    a.CategoryItems = new WeaponCategory[] { WeaponCategory.DuelingSword, WeaponCategory.Longsword };
                    a.RestrictedByClass = Array.Empty<BlueprintCharacterClass>();
                    a.BasicItems = Array.Empty<BlueprintItem>();
                }),
                Helpers.Create<WeaponAttackAndCombatManeuverBonus>(a => { a.WeaponType = duelingSword; a.AttackBonus = 1; a.Descriptor = ModifierDescriptor.Trait; }),
                //Helpers.Create<WeaponAttackAndCombatManeuverBonus>(a => { a.WeaponType = dagger; a.AttackBonus = 1; a.Descriptor = ModifierDescriptor.Trait; }),
                Helpers.Create<WeaponAttackAndCombatManeuverBonus>(a => { a.WeaponType = longsword; a.AttackBonus = 1; a.Descriptor = ModifierDescriptor.Trait; })));





            campaignTraits.SetFeatures(choices);
            return campaignTraits;
        }
    }
}
 