using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EldritchArcana.Objects
{
    public class TraitFeature
    {
        [NonSerialized]TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        public string name { get => $"{myTI.ToTitleCase(displayName.Replace(" ",string.Empty))}Trait"; }
        private string guid;
        public string Guid 
        { 
            get => guid ?? fastJSON.Helper.CreateGuid(displayName).ToString(); 
            internal set=>guid=value; 
        }
        public string description { get; internal set; }
        public string displayName { get; internal set; }
        public Sprite icon { get; internal set; }
        public FeatureGroup group { get; internal set; } = FeatureGroup.None;
        public BlueprintComponent[] components { get; internal set; }
    }
}
