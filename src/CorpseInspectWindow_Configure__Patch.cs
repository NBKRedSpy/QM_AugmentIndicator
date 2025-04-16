using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AugmentIndicator
{
    [HarmonyPatch(typeof(CorpseInspectWindow), nameof(CorpseInspectWindow.Configure))]
    public static class CorpseInspectWindow_Configure__Patch
    {
        /// <summary>
        /// Update the indicator when the screen is configured.  Usually
        /// the Configure functions mean init or refresh.
        /// </summary>
        /// <param name="__instance"></param>
        public static void Postfix(CorpseInspectWindow __instance)
        {
            ImplantUtility.UpdateCorpseIcon(__instance);
        }
    }
}
