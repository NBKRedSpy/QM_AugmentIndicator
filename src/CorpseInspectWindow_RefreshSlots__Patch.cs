using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;

namespace AugmentIndicator
{



    /// <summary>
    /// The RefreshSlots is called when the corpse screen changes.  For example
    /// amputation
    /// </summary>
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.RefreshSlots))]
    public static class CorpseInspectWindow_RefreshSlots__Patch
    {
        public static void Postfix(CorpseInspectWindow __instance)
        {
            ImplantUtility.UpdateCorpseIcon(__instance);
        }
    }
}
