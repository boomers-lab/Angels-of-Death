using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using HarmonyLib;
using Verse.AI;

namespace GrimWorld
{
    [StaticConstructorOnStartup]
    public static class Start
    {

        static Start()
        {
            var harmony = new Harmony("com.example.patch");
            harmony.PatchAll();
            
        }
    }

    [HarmonyPatch(typeof(Pawn), nameof(Pawn.TickRare))]
    public static class OnApparelTrackerTick
    {
        public static void Postfix(Pawn __instance)
        {
            if(__instance.apparel == null) return;
            
            __instance.apparel.Notify_ApparelChanged();
        }
    }

    [HarmonyPatch(typeof(ApparelGraphicRecordGetter), nameof(ApparelGraphicRecordGetter.TryGetGraphicApparel))]
    public static class ShieldNoRenderRules
    {
        public static bool Prefix(Apparel apparel, BodyTypeDef bodyType, out ApparelGraphicRecord rec)
        {
            if (apparel.HasThingCategory(DefDatabase<ThingCategoryDef>.GetNamed("GW_Shield")))
            {
                if (!apparel.Wearer.Drafted)
                {
                    rec = new ApparelGraphicRecord();
                    return false;
                }
            }

            rec = new ApparelGraphicRecord(null, null);
            return true;
        }
    }
}
