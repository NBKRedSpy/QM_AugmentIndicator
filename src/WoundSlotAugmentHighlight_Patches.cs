using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;
using UnityEngine;
using UnityEngine.UI;


namespace AugmentIndicator
{

    /// <summary>
    /// Handles outlining the body part box (aka the "wound slots") if there is an implant or augment.
    /// </summary>
    //[HarmonyPatch(typeof(CustomWoundSlot), nameof(CustomWoundSlot.OnPointerEnter))]
    //[HarmonyPatch(typeof(CustomWoundSlot), nameof(CustomWoundSlot.OnPointerExit))]
    //[HarmonyPatch(typeof(CustomWoundSlot), nameof(CustomWoundSlot.Select))]
    //[HarmonyPatch(typeof(CustomWoundSlot), nameof(CustomWoundSlot.Diselect))]
    //[HarmonyPatch(typeof(CustomWoundSlot), nameof(CustomWoundSlot.Initialize))]
    [HarmonyPatch]
    public static class WoundSlotAugmentHighlight_Patches
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(CustomWoundSlot), nameof(CustomWoundSlot.OnPointerEnter));
            yield return AccessTools.Method(typeof(CustomWoundSlot), nameof(CustomWoundSlot.OnPointerExit));
            yield return AccessTools.Method(typeof(CustomWoundSlot), nameof(CustomWoundSlot.Select));
            yield return AccessTools.Method(typeof(CustomWoundSlot), nameof(CustomWoundSlot.Diselect));
            yield return AccessTools.Method(typeof(CustomWoundSlot), nameof(CustomWoundSlot.Initialize));
            yield return AccessTools.Method(typeof(CustomWoundSlot), nameof(CustomWoundSlot.SetTooltip));
        }

        public static void Prefix()
        {

        }
        public static void Postfix(CustomWoundSlot __instance)
        {
            SetHighlightColor(__instance);
        }

        public static void SetHighlightColor(CustomWoundSlot instance)
        {
            if (instance.IsAmputated || !(instance._installedImplants.Count > 0 || ImplantUtility.AugmentWoundIds.Contains(instance.WoundSlotId)))
            {
                return;
            }

            instance._tooltipHandler._slot.color = Color.yellow;
        }

    }

    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.BodyPartsButtonOnOnSelected))]
    public static class RefreshScreenPatch
    {
        public static void Postfix(CorpseInspectWindow __instance)
        {
            foreach (CustomWoundSlot slot in __instance._woundSlots)
            {
                WoundSlotAugmentHighlight_Patches.SetHighlightColor(slot);
            }
        }
    }
}