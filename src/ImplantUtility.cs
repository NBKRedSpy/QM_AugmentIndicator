using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AugmentIndicator
{
    public static class ImplantUtility
    {
        private static string OriginalCorpseName = "";



        /// <summary>
        /// The list of wound ids for augmentations.  
        /// Used to map the "Wound Id's" back to the augment.  It is one augment to many wound ids 
        /// Does not include the "Human normal" wounds as they are not actually augments.
        /// </summary>
        private static HashSet<string> _AugmentWoundIds = null;

        public static HashSet<string> AugmentWoundIds
        {
            get
            {
                if (_AugmentWoundIds == null)
                {
                    InitAugmentWoundIds();
                }
                return _AugmentWoundIds;
            }
        }


        /// <summary>
        /// If true, only cybernetic augmentations will be shown with the indicator.
        /// Otherwise, will be all but "normal" augments, which are actually just regular human body parts.
        /// </summary>
        public static bool OnlyCyberAugs { get; set; }

        public static void UpdateCorpseIcon(CorpseInspectWindow corpseInspectWindow)
        {
            if (corpseInspectWindow._corpseStorage == null) return;

            bool hasAugOrImplant = HasAugOrImplant(corpseInspectWindow._corpseStorage.CreatureData, 
                out bool hasImplants);

            InspectWindowHeaderButton bodyPartsButton = corpseInspectWindow._bodyPartsButton;

            if (corpseInspectWindow._corpseStorage == null || !hasAugOrImplant)
            {
                bodyPartsButton._selectedBorder.color = Color.clear;
                bodyPartsButton.Diselect();

            }
            else
            {
                bodyPartsButton.Select();
                bodyPartsButton._selectedBorder.color = 
                    hasImplants ? Color.cyan : Color.yellow;
            }
        }

        /// <summary>
        /// Inits the id lookup for all wound types that are augmentations.  This is from the game's Data.
        /// </summary>
        public static void InitAugmentWoundIds()
        {
            //Augmentation record example.  Found here:
            //((Data.Items.GetRecord("cyborg_feet", true) as CompositeItemRecord).Records[0] as AugmentationRecord).WoundSlotIds[0]

            _AugmentWoundIds = Data.Items._records.Values
                .Where(x => x is CompositeItemRecord)
                .SelectMany(x => (x as CompositeItemRecord).Records)
                //Note - was looking at "not NormalAug", but I think regular enemy parts are under
                //  PossessedAug, FaunaAugment, etc.
                //Kept getting notifications of low tier Quasi augments.
                .Where(x => 
                    x is AugmentationRecord aug  &&
                        (
                            (OnlyCyberAugs && aug.Categories.Contains("CyberAug")) || 
                            (!OnlyCyberAugs && !aug.Categories.Contains("NormalAug"))
                        )
                    )
                        
                //.Where(x => x is AugmentationRecord aug && !aug.Categories.Contains("NormalAug"))
                .SelectMany(x => ((AugmentationRecord)x).WoundSlotIds)
                .ToHashSet();
        }

        /// <summary>
        /// Returns true if there are augments or implants on the body.
        /// </summary>
        /// <param name="creatureData"></param>
        /// <param name="hasImplants">True if the body has implants.</param>
        /// <returns></returns>
        private static bool HasAugOrImplant(CreatureData creatureData, out bool hasImplants)
        {

            hasImplants = AugmentationSystem.HasAnyInstalledImplants(creatureData);


            //Augmentations:  
            //	Augmentations are "wound slots".  The actual Augmentation record can be linked back to the
            //  AugmentationRecord by the WoundSlotId.  
            //	The AugmentationRecord is used instead of "nature" since Categories is slightly more precise.

            //Amputations are different Wound Effects.  Can only tell if an implant or augmentation is still 
            //  present if there is not an amputation record for the augment's location.
            HashSet<string> amputations = creatureData.EffectsController.Effects
                .Where(x => x is BodyPartWound wound && wound.IsAmputation)
                .Cast<BodyPartWound>()
                .Select(x => x.SlotPositionType)
                .ToHashSet();

            bool hasAugmentations = creatureData.EffectsController.Effects
                .Any(x =>
                {
                    var augmentation = x as ImplicitAugEffect;
                    if (augmentation == null) return false;

                    if (amputations.Contains(augmentation.WoundSlotPosition)) return false;

                    //Note: AugmentWoundIds do not include the blacklisted augments such as "NormalAug" (human normal hands).
                    return AugmentWoundIds.Contains(augmentation.WoundSlotId);
                });

            return (hasImplants || hasAugmentations);
        }


    }
}
