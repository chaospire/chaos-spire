using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace ChaosSpire;

[ModInitializer(nameof(Initialize))]
public static class ChaosPlugin
{
    internal static Harmony? HarmonyInstance;

    public static void Initialize()
    {
        HarmonyInstance = new Harmony("com.chaospire.chaos_spire");
        HarmonyInstance.PatchAll();
        GD.Print("[ChaosSpire] Loaded. Every turn is a gamble now.");
    }
}
