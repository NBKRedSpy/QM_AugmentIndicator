using AugmentIndicator.Utility;
using AugmentIndicator.Utility.Mcm;
using AugmentIndicator;
using ModConfigMenu;
using ModConfigMenu.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace AugmentIndicator
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config, Utility.Logger logger) : base (config, logger) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Augment Indicator", new List<ConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.OnlyCyberAugs), "Only show cybernetic augmentations"),

            }, OnSave);
        }

        protected override bool OnSave(Dictionary<string, object> currenIMcmConfigTarget, out string feedbackMessage)
        {

            bool result = base.OnSave(currenIMcmConfigTarget, out feedbackMessage);

            ImplantUtility.OnlyCyberAugs = ((ModConfig)Config).OnlyCyberAugs;
            ImplantUtility.InitAugmentWoundIds();

            return result;


        }
    }
}
