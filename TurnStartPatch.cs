using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;

namespace ChaosSpire;

/// <summary>
/// Harmony postfix on Hook.AfterPlayerTurnStart.
/// Chains our chaos effect onto the existing async Task so it runs
/// after all normal turn-start hooks finish.
/// </summary>
[HarmonyPatch(typeof(Hook), nameof(Hook.AfterPlayerTurnStart))]
public static class TurnStartPatch
{
    public static void Postfix(
        ref Task __result,
        CombatState combatState,
        PlayerChoiceContext choiceContext,
        Player player)
    {
        var original = __result;
        __result = RunAfter(original, combatState, choiceContext, player);
    }

    private static async Task RunAfter(
        Task original,
        CombatState combatState,
        PlayerChoiceContext choiceContext,
        Player player)
    {
        await original;
        await ChaosRoulette.Roll(combatState, choiceContext, player);
    }
}
