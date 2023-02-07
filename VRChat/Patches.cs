
namespace ReMod.Core.VRChat
{
    [HarmonyLib.HarmonyPatch(typeof(ActionMenu), "Method_Public_Void_PDM_4")]
    public class ActionMenuPatch
    {
        [HarmonyLib.HarmonyPostfix]
        internal static void OnActionMenu(ActionMenu __instance) => UI.ActionMenu.ActionMenuAPI.OpenMainPage(__instance);
    }
}

