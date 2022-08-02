using EldritchArcana.Objects;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldritchArcana.TraitsDrawbacks
{
    internal class GamblerTraits
    {
        public static BlueprintFeatureSelection CreateGamblerTraits()
        {
            var noFeature = Helpers.PrerequisiteNoFeature(null);

     

            var gambler = new TraitFeature()
            {
                displayName = "Optimistic Gambler",
                description = "You’ve always seemed to have trouble keeping money. Worse, you always seem to have debts looming over your head. When you heard about the “Cheat the Devil and Take His Gold” gambling tournament, you felt in your gut that your luck was about to change. You’ve always been optimistic, in fact, and even though right now is one of those rare times where you don’t owe anyone any money (you just paid off a recent loan from local moneylender Lymas Smeed), you know that’ll change soon enough. Better to start amassing money now when you’re at one of those rare windfall times! You’ve set aside a gold coin for the entrance fee, and look forward to making it big—you can feel it in your bones! This time’s gonna be the big one! Your boundless optimism, even in the face of crushing situations, has always bolstered your spirit.\n" +
                "Benefit: take a chance you will get a random benefit",
                Guid = "c88b9398af66406cac173884df308eb8",
                icon = Image2Sprite.Create("Mods/EldritchArcana/sprites/optimistic_gambler.png"),
                components = new BlueprintComponent[] { noFeature }
            };

            var optiGamblerTraits = Helpers.CreateFeatureSelection(gambler.name, gambler.displayName,gambler.description, gambler.Guid, gambler.icon, gambler.group, gambler.components);
            noFeature.Feature = optiGamblerTraits;

            var choices = new List<BlueprintFeature>();

            string[] CampaignGuids = new string[300];
            //EmotionGuids = guids;
            string baseguid = "BB54279F30DA4802FFFF";
            int x = 0;
            for (long i = 442922691494; i < 442922691744; i++)
            {
                CampaignGuids[x] = baseguid + i.ToString();
                x++;
            }

            //possible extra trait pioneer: get +1 to a skill that relates to nature and get a murder pony staff


            // var families = new List<BlueprintFeature>() { }
            //choices.Add( Helpers.CreateAddStatBonus(
            //Orlovski.SetFeatures(OrlovskiFeatures);
            var duelingSword = Traits.library.Get<BlueprintWeaponType>("a6f7e3dc443ff114ba68b4648fd33e9f");
            var longsword = Traits.library.Get<BlueprintWeaponType>("d56c44bc9eb10204c8b386a02c7eed21");

            var layonhandsResource = Traits.library.Get<BlueprintAbilityResource>("9dedf41d995ff4446a181f143c3db98c");
            var MutagenResource = Traits.library.Get<BlueprintAbilityResource>("3b163587f010382408142fc8a97852b6");
            var JudgmentResource = Traits.library.Get<BlueprintAbilityResource>("394088e9e54ccd64698c7bd87534027f");
            var ItemBondResource = Traits.library.Get<BlueprintAbilityResource>("fbc6de6f8be4fad47b8e3ec148de98c2");
            var kiPowerResource = Traits.library.Get<BlueprintAbilityResource>("9d9c90a9a1f52d04799294bf91c80a82");
            var ArcanePoolResourse = Traits.library.Get<BlueprintAbilityResource>("effc3e386331f864e9e06d19dc218b37");
            var ImpromptuSneakAttackResource = Traits.library.Get<BlueprintAbilityResource>("78e6008db60d8f94b9196464983ad336");
            var WildShapeResource = Traits.library.Get<BlueprintAbilityResource>("ae6af4d58b70a754d868324d1a05eda4");
            var SenseiPerformanceResource = Traits.library.Get<BlueprintAbilityResource>("ac5600c9642692145b7eb4553a703c1a");

            var snowball = Traits.library.Get<BlueprintAbility>("9f10909f0be1f5141bf1c102041f93d9");
            var fireball = Traits.library.Get<BlueprintAbility>("2d81362af43aeac4387a3d4fced489c3");
            var alchemist = Traits.library.Get<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var bard = Traits.library.Get<BlueprintCharacterClass>("772c83a25e2268e448e841dcd548235f");
            var cleric = Traits.library.Get<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var druid = Traits.library.Get<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var scion = Traits.library.Get<BlueprintCharacterClass>("f5b8c63b141b2f44cbb8c2d7579c34f5");
            var magus = Traits.library.Get<BlueprintCharacterClass>("45a4607686d96a1498891b3286121780");
            var paladin = Traits.library.Get<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var sorcerer = Traits.library.Get<BlueprintCharacterClass>("b3a505fb61437dc4097f43c3f8f9a4cf");
            var ranger = Traits.library.Get<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
            var wizard = Traits.library.Get<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            var oracle = Traits.library.Get<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
            try
            {
                //oracle = Main.settings?.DrawbackForextraTraits == true ? Traits.library.Get<BlueprintCharacterClass>("ec73f4790c1d4554871b81cde0b9399b") : Traits.library.Get<BlueprintCharacterClass>("6b1d00511e824f0fbc27ec8f54b8edb2");
                oracle = Traits.library.Get<BlueprintCharacterClass>("ec73f4790c1d4554871b81cde0b9399b");
            }
            catch
            {
                oracle = Traits.library.Get<BlueprintCharacterClass>("6b1d00511e824f0fbc27ec8f54b8edb2");
            }


            var rogue = Traits.library.Get<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");

           
            //97143f0ec43f505409929934811144c3 continues freedom of movement

           
            string[] Diffforhumans = new string[] { "Ki power as a monk", "Alchemy mutagen as a Alchemist", "Bonded item restore spell as a wizard", "Impromtu sneak attack as a acrane trickster", "Judgement ability as a inquisitor", "Arcane weapon enhancements as a magus", "Performances as a Sensei" };
            string[] DiffforhumansT = new string[] { "Ki", "Alchemy Mutagen", "Bonded Item", "Impromtu Sneak Attack", "Judgement", "Arcane Weapon Enhancements", "Sensei Performances" };
            var LebdaFeatures = new List<BlueprintFeature>() { };
            var Resources = new List<BlueprintAbilityResource> { kiPowerResource, MutagenResource, ItemBondResource, ImpromptuSneakAttackResource, JudgmentResource, ArcanePoolResourse, SenseiPerformanceResource };
            //CreateIncreaseResourceAmount for a few different resources
            x = 0;
            int y = 1;
            //Resources.randomelement();
            
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
            var summonedBow = Traits.library.Get<BlueprintItem>("2fe00e2c0591ecd4b9abee963373c9a7");
            var EelCircletItem = Traits.library.Get<BlueprintItem>("f7d8c27c57d6bd949a5c2a85dc5ca045");

            //ishomebrew
            var dice = Helpers.GetIcon("3fcc181a8b2094b4d9a636b639f0b79b");
            var OptimisticGambler = Helpers.CreateFeatureSelection("OptimisticGamblerTrait", "Optimistic Gambler",
                 "You’ve always seemed to have trouble keeping money. Worse, you always seem to have debts looming over your head. When you heard about the “Cheat the Devil and Take His Gold” gambling tournament, you felt in your gut that your luck was about to change. You’ve always been optimistic, in fact, and even though right now is one of those rare times where you don’t owe anyone any money (you just paid off a recent loan from local moneylender Lymas Smeed), you know that’ll change soon enough. Better to start amassing money now when you’re at one of those rare windfall times! You’ve set aside a gold coin for the entrance fee, and look forward to making it big—you can feel it in your bones! This time’s gonna be the big one! Your boundless optimism, even in the face of crushing situations, has always bolstered your spirit.\n" +
                 "Benefit: take a chance you will get a random benefit",
                 "c88b9398af66406cac173884df308eb8",
                 Image2Sprite.Create("Mods/EldritchArcana/sprites/optimistic_gambler.png"),
                 FeatureGroup.None);

            var OptimisticGamblerSpel = Helpers.CreateFeatureSelection("OptimisticGamblerSpelTrait", "Optimistic Gambler Spell",
                 "You’ve always seemed to have trouble keeping money. Worse, you always seem to have debts looming over your head. When you heard about the “Cheat the Devil and Take His Gold” gambling tournament, you felt in your gut that your luck was about to change. You’ve always been optimistic, in fact, and even though right now is one of those rare times where you don’t owe anyone any money (you just paid off a recent loan from local moneylender Lymas Smeed), you know that’ll change soon enough. Better to start amassing money now when you’re at one of those rare windfall times! You’ve set aside a gold coin for the entrance fee, and look forward to making it big—you can feel it in your bones! This time’s gonna be the big one! Your boundless optimism, even in the face of crushing situations, has always bolstered your spirit.\n" +
                 "Benefit: take a chance you will get a random spell from level 1-7 with cl+2 dc+2 and metamagic reduction 2",
                 "c88b9398bf66446cac173884df308eb8",
                 Image2Sprite.Create("Mods/EldritchArcana/sprites/optimistic_gambler.png"),
                 FeatureGroup.None);
            int rnd = DateTime.Now.Millisecond % 30;
            var options = Traits.ReturnSpellOptions(OptimisticGamblerSpel, 1, 7, Helpers.Create<Increaselucky>(), Helpers.Create<ReduceMetamagicCostForSpell>(r => r.Reduction = 2));
            OptimisticGamblerSpel.SetFeatures(options);
            //list with random features
            string wwib = "What will it be?";
            string Gmbldsc = "Look at your stats and inventory or at your class ability usage stats and you will find out your bonus\n" +
                    "Or you won't. That's the thing. Sometimes you win, and sometimes you lose. But that's what you are all about.\n" +
                    "Benefit: To know what type of bonus you get in advance, right-click on the trait and scroll down." +
                    $"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n " +
                    $"Note if you want a different bonus you need to restart the game. this bonus will stay the same" +
                    $"\n";
            var UndeadSummonFeature = Traits.library.Get<BlueprintFeature>("f06f246950e76864fa545c13cb334ba5");
            //var TristianAngelFeature = Traits.library.Get<BlueprintFeature>("96e026d5a38b24e4b87cd7dcd831cc16");
            var RingOfEnhancedSummonsFeature = Traits.library.Get<BlueprintFeature>("2bf0c2547f455894b93083e589866030");
            var randsummom = Traits.library.CopyAndAdd<BlueprintFeature>(
                UndeadSummonFeature.AssetGuid,
                "RandomEffectUndeadSummons",
                CampaignGuids[32]);

            //var animate_dead = Traits.library.CopyAndAdd<BlueprintAbility>("4b76d32feb089ad4499c3a1ce8e1ac27", "At_wil_animate_dead", "4b76d32feb089bd4549c3a1ce8e1ac29");







            randsummom.SetDescription(Gmbldsc + randsummom.GetName());//"undead summons"
            randsummom.SetName(wwib);
            randsummom.SetIcon(dice);

            var guidfeaturelist = new string[]{
                "201614af25697594a865355182fdb558",
                "d7d8d9691f5b8b84497c8789672fe1ba",
                "bf9b14d6f65fa944f91f5cc2b9d02fa0",
                "576933720c440aa4d8d42b0c54b77e80",
                "789c7539ba659174db702e18d7c2d330",
                "15bac762b599b7e42824c333717d79d9",
                "734a29b693e9ec346ba2951b27987e33",
                "3c0b706c526e0654b8af90ded235a089",
                "e66154511a6f9fc49a9de644bd8922db",
                "9c141c687eae35f4ba5399c11a4bdbc3",
                "9993edb6c470a6f4ab0bb8aac0b7522a",
                "aae0cb964bf516a4480d6745b71de4e7",
                "2ee2ba60850dd064e8b98bf5c2c946ba",//leopard
                "126712ef923ab204983d6f107629c895",//smilodon companion
                "7775c915661f8fb449c38dd873524389",//mercy stunned
                "b3889f445dbe42948b8bb1ba02e6d949",//monk unarmed level 12
                "4b90455e9f0b4b7468a21e5fed3becab",//monster tactition summon 
                "57d5077b301ade749b840b0ea9230bb9",//Alchemist infusion discovery
                "2f571c1c93b7ebc4ab4ba98613c89b09",//necromancers staff
                "a39a8e310a554744db0cb6440b36563c",//manticore poison bite
                "1a8149c09e0bdfc48a305ee6ac3729a8",//pounce
                "01971351119121d429ecf62c2ab94de3",//dragondesciplebite
                "ff77d405aa192614d9ca43011e3d96d6",//octavia sneak attack
                "076e26ea2a16d4c42a355cc5caf4c0b4",//wildshape smilodon
                "fe9482aad88e5a54682d47d1df910ce8",//improved natural armor
                "96e026d5a38b24e4b87cd7dcd831cc16",//Tristianangelfeature
                "de23c90774447dd49878b531977939c6",//thieflingtwis -- 2 random at will spells
                "0f5d9fcbc16b61848a12ab5b60c08935",//permanent heroism
                "4503e1f707b824c4fb181355349c8cb3",//+ 4 wisdom
                "c89cc847cc6e55e409f2345c482eb902",//permanent blur
                "4870cf8732666c340952be56e6a309fb",//blinding beuty might be op
                "1ef8d7ab3ca4795498ff446cb027e2f3",//favored enemy animals
                "918555c021b3a2944beed35df53b4c56",//favored enemy dragons
                "19e1c418cbad1b540ad52fee0fd7f16b",//FavoriteTerrainForest.
                "5941963eae3e9864d91044ba771f2cc2",//FavoriteEnemyUndead.
                "f643b38acc23e8e42a3ed577daeb6949",//FavoriteEnemyOutsider.
                "7283344b0309d8e4cb77eb22f1e7c57a",//FavoriteEnemyHuman.
                "10a75ca624a7d3242a05003d22bc38f0",//3 quickened spells
                "90101ec16ba183c4fb38fbb40a66502f",//extended arcana feature
                "9d6546880b05e9a43bb6cc5e1d374c6a",//ReachArcanaFeature.
                "d86807f4e6d5ae34185f86c78979a995",//ringofplentifullhuntsfeature
                "2bf0c2547f455894b93083e589866030",//ringofenhancedsummonsfeature
                "7d7b7f69520babc40a3563bcbc1c7f72",//ringofcorrosion
                "4c0f0c9fafc3f8745a0e81ba95908c01",//safety teleport feature
                "094b291d04fdde84b963b07400c0df80",//aceberic ring feature
            };
            //d2cff9243a7ee804cb6d5be47af30c73  blueprintability lightningbolt at will
            Helpers.Create<AddAbilityToCharacterComponent>();
            /*
            randsummom,
            RingOfEnhancedSummonsFeature,
            TristianAngelFeature,
            UndeadSummonFeature,
            */

            var OptimisticGamblerOptions = new List<BlueprintFeature>() {
                //SpellAtWill,
                randsummom,
                
                //+3 healed
                Helpers.CreateFeature("randomeffectExtraHeal",wwib,Gmbldsc+"+3 healed by healspels"
                    ,"c88b9398af66406cac124884df308eb8",dice,FeatureGroup.None,
                    Helpers.Create<FeyFoundlingLogic>(s => { s.flatModefier = 3; })),
                //+3 healed per die
                Helpers.CreateFeature("randomeffectExtraHealdice",wwib,Gmbldsc+" +3 healed by healspels per die"
                    ,"c88b9398af66406cac124884df308ec3",dice,FeatureGroup.None,
                    Helpers.Create<FeyFoundlingLogic>(s => { s.dieModefier = 3; })),
                //get summoned bow
                Helpers.CreateFeature("randomeffectExtrabow",wwib,Gmbldsc+" You start with a very good bow +2 enhancement and speed"
                    ,"e82b9398af64406cac124884df308fb9",dice,FeatureGroup.None,
                    Helpers.Create<AddStartingEquipment>(a =>
                    {
                        a.CategoryItems = Array.Empty<WeaponCategory>();
                        a.RestrictedByClass = Array.Empty<BlueprintCharacterClass>();
                        a.BasicItems = new BlueprintItem[] { summonedBow };
                    })),
                //get at will lightning cap
                Helpers.CreateFeature("randomeffectExtraEelcirclet",wwib,Gmbldsc+" You start with eels circlet lightning at will"
                    ,"e42b9398aa15325cac124884df308fb9",dice,FeatureGroup.None,
                    Helpers.Create<AddStartingEquipment>(a =>
                    {
                        a.CategoryItems = Array.Empty<WeaponCategory>();
                        a.RestrictedByClass = Array.Empty<BlueprintCharacterClass>();
                        a.BasicItems = new BlueprintItem[] { EelCircletItem };
                    })),
                //+1 on spells damage
                Helpers.CreateFeature("randomeffectExtramagicdamage",wwib,Gmbldsc+"extra damage on spells per damage die"
                    ,"c88b9398af66405cac124884df338eb8",dice,FeatureGroup.None,
                    Helpers.Create<ArcaneBloodlineArcana>(),
                    Helpers.Create<ArcaneBloodlineArcana>(),
                    Helpers.Create<ArcaneBloodlineArcana>())
            };
            //BlueprintSpellList
            var anyspell = Helpers.allSpells;
            var OptimisticGamblerOptions_SpellAtWill = new List<BlueprintFeature>() { };
            var OptimisticGamblerOptions_Weapons = new List<BlueprintFeature>() { };
            //var resource = Helpers.CreateAbilityResource("RndResource", "", "", "1c73d22ceb039bd4549c3a1cf7e1ac30", null);
            //resource.SetFixedResource(1);

            foreach (var blup in anyspell)
            {
                var item = Traits.library.CopyAndAdd<BlueprintAbility>(blup.AssetGuid, blup.Name, Helpers.MergeIds(blup.AssetGuid, "e42b9398aa15325cac124884df305fb9"));
                item.Type = AbilityType.SpellLike;

                //var resource = Helpers.CreateAbilityResource($"{item.name}RndResource", "", "", Helpers.MergeIds("1c73d22ceb039bd4549c3a1cf7e1ac30", item.AssetGuid), null);
                //resource.SetFixedResource(1);
                //item.AddComponent(Helpers.CreateResourceLogic(resource));
                //item.RemoveComponents<SpellListComponent>();
                //item.RemoveComponents<SpellComponent>();
                //item.RemoveComponents<SpellListComponent>();
                //if(item.)
                if (item.Parent != null) continue;

                var spellLists = item.GetComponents<SpellListComponent>();
                if (spellLists.FirstOrDefault() == null) continue;

                var level = spellLists.Min(l => l.SpellLevel);
                if (level != 1) continue;
                var randomspellAbility = Helpers.CreateFeature($"aaRandomEffectTraitSpell{item.name}AtWill",
                                    "level 1 spell at will," + wwib,
                                    Gmbldsc + $" cast {item.name}  at Will" + item.Description,
                                    Helpers.MergeIds("4b76d32feb089bd4549c3a1ce8e1ac30", item.AssetGuid),
                                    dice,
                                    FeatureGroup.None,
                                    Helpers.CreateAddFacts(item)
                                    //,resource.CreateIncreaseResourceAmount(2)
                                    );

                OptimisticGamblerOptions_SpellAtWill.Add(randomspellAbility);
            }

            for (int i = 0; i < guidfeaturelist.Length; i++)
            {
                int effectnumber = 33 + i;
                var CopiedFeat = Traits.library.CopyAndAdd<BlueprintFeature>(
                guidfeaturelist[i],
                $"RandomEffectnumber{effectnumber}",
                CampaignGuids[effectnumber]);
                CopiedFeat.SetDescription("sometimes is useless unless you spec into the class this feature belongs to \n" + Gmbldsc + CopiedFeat.GetName());//feature name
                CopiedFeat.SetName("classfeature," + wwib);
                CopiedFeat.SetIcon(dice);
                CopiedFeat.PrerequisiteFeature(any: true);



                OptimisticGamblerOptions.Add(CopiedFeat);
            }

            var bob = new StatType[] {
                StatType.AC,
                StatType.AdditionalAttackBonus,
                StatType.AdditionalCMB,
                StatType.BaseAttackBonus,
                StatType.Charisma,//(1.2.4)
                StatType.Reach,
                StatType.SneakAttack,
                StatType.Strength,//(1.2.4)
                StatType.Intelligence,
                StatType.Wisdom,//(1.2.4)
            };

            foreach (StatType stat in bob)
            {
                OptimisticGamblerOptions.Add(Helpers.CreateFeature($"randomeffect{stat}", "statbonus " + wwib, Gmbldsc + $" +3 {stat} luck bonus"
                    , Helpers.MergeIds(Helpers.getStattypeGuid(stat), "c88b9398af66406cac173884df308eb8"), dice, FeatureGroup.None,
                    Helpers.CreateAddStatBonus(stat, 3, ModifierDescriptor.Luck)));
            }


            var weapons = new WeaponCategory[] {
                WeaponCategory.Dagger,                WeaponCategory.Dart,  WeaponCategory.Quarterstaff,
                WeaponCategory.DuelingSword,                WeaponCategory.ElvenCurvedBlade,
                WeaponCategory.Flail,                WeaponCategory.Greataxe,
                WeaponCategory.Javelin,                WeaponCategory.LightMace,
                WeaponCategory.Shuriken,                WeaponCategory.Sickle,
                WeaponCategory.Sling,                WeaponCategory.Kama,
                WeaponCategory.Kukri,                WeaponCategory.Starknife,
                WeaponCategory.ThrowingAxe,                WeaponCategory.LightPick,//possibly replace by quarterstaff
                WeaponCategory.DwarvenWaraxe,                WeaponCategory.Trident,
                WeaponCategory.BastardSword,                WeaponCategory.Battleaxe,
                WeaponCategory.Longsword,                WeaponCategory.Nunchaku,
                WeaponCategory.Rapier,                WeaponCategory.Sai,
                WeaponCategory.Scimitar,                WeaponCategory.Shortsword,
                WeaponCategory.Club,                WeaponCategory.WeaponLightShield,
                WeaponCategory.WeaponHeavyShield,                WeaponCategory.HeavyMace
            };
            x = 0;
            foreach (WeaponCategory weap in weapons)
            {

                x++;

                OptimisticGamblerOptions_Weapons.Add(
                    Helpers.CreateFeature(
                        $"randomeffectExtra{weap}", "Weaponbonus," + wwib, Gmbldsc + $" You start with a {weap} and you have a 3 bonus on attack rolls with weapons of this type"
                        , CampaignGuids[x], dice, FeatureGroup.None,
                        Helpers.Create<WeaponCategoryAttackBonus>(b => { b.Category = weap; b.AttackBonus = 3; }),
                        //Helpers.Create<WeaponTypeDamageBonus>(c=> { c.WeaponType = weap; })
                        Helpers.Create<AddStartingEquipment>(a =>
                        {
                            a.CategoryItems = new WeaponCategory[] { weap, weap };
                            a.RestrictedByClass = Array.Empty<BlueprintCharacterClass>();
                            a.BasicItems = Array.Empty<BlueprintItem>();
                        })
                    )
                 );

            }



            rnd = DateTime.Now.Millisecond % OptimisticGamblerOptions.Count;
            int randomWeaponNum = DateTime.Now.Millisecond % OptimisticGamblerOptions_Weapons.Count;
            int randomSpellAtWillNum = DateTime.Now.Millisecond % OptimisticGamblerOptions_SpellAtWill.Count;
            int rnd2 = OptimisticGamblerOptions.Count - 1 - (DateTime.Now.Millisecond % OptimisticGamblerOptions.Count);
            //geneates a random number that is basicly a random element from the list.
            //rnd = 0;
            //rnd2 = 1;
            //foreach (var OptimisticgamblerOption in OptimisticGamblerOptions)
            //{
            //    OptimisticgamblerOption.PrerequisiteFeature(noFeature);
            //}
            var xander = OptimisticGamblerOptions[rnd];
            var randomSpellAtWill = OptimisticGamblerOptions_SpellAtWill[randomSpellAtWillNum];
            var randomSpellWeapon = OptimisticGamblerOptions_Weapons[randomWeaponNum];
            //xander.HideInUI=true;
            var option2 = OptimisticGamblerOptions[rnd2];
            OptimisticGamblerOptions.AddRange(OptimisticGamblerOptions_Weapons);
            //OptimisticGamblerOptions.AddRange(OptimisticGamblerOptions_SpellAtWill);
            //var option3 = OptimisticGamblerOptions[DateTime.Now.Millisecond % OptimisticGamblerOptions.Count];
            OptimisticGamblerOptions = Main.settings?.CheatCustomTraits == true ? OptimisticGamblerOptions : new List<BlueprintFeature> { xander, option2, OptimisticGamblerSpel, randomSpellAtWill, randomSpellWeapon };
            //OptimisticGambler.SetFeatures(xander,option2);
            //OptimisticGambler.SetFeatures(OptimisticGamblerOptions);
            OptimisticGambler.IgnorePrerequisites = true;
            //choices.Add(OptimisticGambler);

            




            optiGamblerTraits.SetFeatures(OptimisticGamblerOptions);
            return optiGamblerTraits;
        }
    }
}
