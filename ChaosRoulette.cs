using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace ChaosSpire;

public enum ChaosTier { Blessed, Cursed, Chaotic }

public record ChaosEffect(
    string Name,
    ChaosTier Tier,
    Func<CombatState, PlayerChoiceContext, Player, Task> Execute);

public static class ChaosRoulette
{
    private static readonly Random Rng = new();
    private static readonly List<ChaosEffect> Effects = new();

    static ChaosRoulette()
    {
        // ── BLESSED ──────────────────────────────────────────
        Effects.Add(new("STONKS",
            ChaosTier.Blessed,
            async (cs, ctx, p) =>
            {
                await PlayerCmd.GainGold(77, p);
            }));

        Effects.Add(new("PLOT ARMOR",
            ChaosTier.Blessed,
            async (cs, ctx, p) =>
            {
                await CreatureCmd.GainBlock(
                    p.Creature, new BlockVar(40, ValueProp.Unpowered), null);
            }));

        Effects.Add(new("IDDQD",
            ChaosTier.Blessed,
            async (cs, ctx, p) =>
            {
                await CreatureCmd.Heal(p.Creature, 30);
            }));

        Effects.Add(new("UNLIMITED POWER",
            ChaosTier.Blessed,
            async (cs, ctx, p) =>
            {
                await PlayerCmd.GainEnergy(3, p);
            }));

        Effects.Add(new("THANOS SNAP",
            ChaosTier.Blessed,
            async (cs, ctx, p) =>
            {
                var enemies = cs.Enemies.Where(e => e.IsAlive).ToList();
                if (enemies.Count > 0)
                {
                    await CreatureCmd.Damage(
                        new BlockingPlayerChoiceContext(),
                        enemies, 30, ValueProp.Unpowered, null, null);
                    await CombatManager.Instance.CheckWinCondition();
                }
            }));

        Effects.Add(new("CARD PRINTER GO BRRR",
            ChaosTier.Blessed,
            async (cs, ctx, p) =>
            {
                var hookCtx = new HookPlayerChoiceContext(
                    p, LocalContext.NetId.Value, GameActionType.Combat);
                var task = CardPileCmd.Draw(hookCtx, 3, p);
                await hookCtx.AssignTaskAndWaitForPauseOrCompletion(task);
            }));

        Effects.Add(new("JUGGERNAUT",
            ChaosTier.Blessed,
            async (cs, ctx, p) =>
            {
                await PowerCmd.Apply<StrengthPower>(
                    p.Creature, 3, p.Creature, null);
            }));

        // ── CURSED ───────────────────────────────────────────
        Effects.Add(new("NOT STONKS",
            ChaosTier.Cursed,
            async (cs, ctx, p) =>
            {
                int loss = Math.Max(1, p.Gold / 3);
                await PlayerCmd.LoseGold(loss, p, GoldLossType.Lost);
            }));

        Effects.Add(new("SKILL ISSUE",
            ChaosTier.Cursed,
            async (cs, ctx, p) =>
            {
                await CreatureCmd.Damage(
                    new BlockingPlayerChoiceContext(),
                    new List<Creature> { p.Creature },
                    12, ValueProp.Unpowered, null, null);
            }));

        Effects.Add(new("NERF BAT",
            ChaosTier.Cursed,
            async (cs, ctx, p) =>
            {
                int block = p.Creature.Block;
                if (block > 0)
                    await CreatureCmd.LoseBlock(p.Creature, block);
            }));

        Effects.Add(new("PAPER TIGER",
            ChaosTier.Cursed,
            async (cs, ctx, p) =>
            {
                await PowerCmd.Apply<VulnerablePower>(
                    p.Creature, 2, null, null);
            }));

        Effects.Add(new("HEALING SURGE",
            ChaosTier.Cursed,
            async (cs, ctx, p) =>
            {
                var enemies = cs.Enemies.Where(e => e.IsAlive).ToList();
                foreach (var e in enemies)
                    await CreatureCmd.Heal(e, 20);
            }));

        // ── CHAOTIC ──────────────────────────────────────────
        Effects.Add(new("YOLO",
            ChaosTier.Chaotic,
            async (cs, ctx, p) =>
            {
                await CreatureCmd.Damage(
                    new BlockingPlayerChoiceContext(),
                    new List<Creature> { p.Creature },
                    8, ValueProp.Unpowered, null, null);
                await PlayerCmd.GainGold(88, p);
            }));

        Effects.Add(new("GLASS CANNON",
            ChaosTier.Chaotic,
            async (cs, ctx, p) =>
            {
                await PlayerCmd.GainEnergy(4, p);
                await CreatureCmd.Damage(
                    new BlockingPlayerChoiceContext(),
                    new List<Creature> { p.Creature },
                    10, ValueProp.Unpowered, null, null);
            }));

        Effects.Add(new("CRITICAL HIT",
            ChaosTier.Chaotic,
            async (cs, ctx, p) =>
            {
                var enemies = cs.Enemies.Where(e => e.IsAlive).ToList();
                if (enemies.Count > 0)
                {
                    var target = enemies[Rng.Next(enemies.Count)];
                    await CreatureCmd.Damage(
                        new BlockingPlayerChoiceContext(),
                        new List<Creature> { target },
                        99, ValueProp.Unpowered, null, null);
                    await CombatManager.Instance.CheckWinCondition();
                }
            }));

        Effects.Add(new("TRADE OFFER",
            ChaosTier.Chaotic,
            async (cs, ctx, p) =>
            {
                await CreatureCmd.Heal(p.Creature, 20);
                int cost = Math.Min(p.Gold, 25);
                if (cost > 0)
                    await PlayerCmd.LoseGold(cost, p, GoldLossType.Lost);
            }));

        Effects.Add(new("ARMS RACE",
            ChaosTier.Chaotic,
            async (cs, ctx, p) =>
            {
                await PowerCmd.Apply<StrengthPower>(
                    p.Creature, 5, p.Creature, null);
                var enemies = cs.Enemies.Where(e => e.IsAlive).ToList();
                foreach (var e in enemies)
                    await PowerCmd.Apply<StrengthPower>(e, 3, null, null);
            }));

        Effects.Add(new("NOTHING HAPPENED",
            ChaosTier.Chaotic,
            (cs, ctx, p) => Task.CompletedTask));
    }

    public static async Task Roll(
        CombatState combatState,
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (!CombatManager.Instance.IsInProgress) return;
        if (player.Creature.IsDead) return;

        var effect = Effects[Rng.Next(Effects.Count)];
        string tierTag = effect.Tier switch
        {
            ChaosTier.Blessed => "[color=gold]BLESSED[/color]",
            ChaosTier.Cursed  => "[color=red]CURSED[/color]",
            _                 => "[color=cyan]CHAOS[/color]",
        };

        GD.Print($"[ChaosSpire] {tierTag} >> {effect.Name}");

        try
        {
            await effect.Execute(combatState, choiceContext, player);
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[ChaosSpire] Effect '{effect.Name}' failed: {ex.Message}");
        }
    }
}
