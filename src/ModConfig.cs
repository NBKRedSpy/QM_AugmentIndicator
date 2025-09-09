using AugmentIndicator.Utility.Mcm;
using AugmentIndicator.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using UnityEngine;


namespace AugmentIndicator;

public class ModConfig : PersistentConfig<ModConfig>, IMcmConfigTarget
{

    /// <summary>
    /// If true, only cybernetic augmentations will be shown with the indicator.
    /// Otherwise, all augments except for "normal" augments will be shown.
    /// </summary>
    public bool OnlyCyberAugs { get; set; } = true;

    public ModConfig() 
    {
    }

    public ModConfig(string configPath, Utility.Logger logger) : base(configPath, logger)
    {
    }

}
