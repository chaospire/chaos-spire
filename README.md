# Chaos Spire — Random Effect Every Turn | Slay the Spire 2 Mod

**Chaos Spire** is a Slay the Spire 2 (STS2) mod that triggers a random effect at the start of every player turn. Gain 77 gold, take 12 damage, or deal 99 damage to a random enemy — you never know what's coming. Built with Harmony runtime patching. No game files are modified.

> Works with Slay the Spire 2 v0.99.1+ (Early Access). Compatible with other mods.

---

## What does this mod do?

Every time your turn starts in combat, the mod rolls one of **17 random effects**. Effects are split into three tiers:

- **Blessed** — Something good happens (gold, block, energy, damage to enemies)
- **Cursed** — Something bad happens (self-damage, lost gold, enemies heal)
- **Chaotic** — A mix of both, or something completely unpredictable

All effects use the game's built-in animation and UI — you see gold fly in, damage numbers pop, block shields appear, just like normal gameplay.

---

## Full Effect List

### Blessed (positive effects)

| Effect | What happens |
|--------|-------------|
| **STONKS** | Gain 77 gold |
| **PLOT ARMOR** | Gain 40 block |
| **IDDQD** | Heal 30 HP |
| **UNLIMITED POWER** | Gain 3 energy |
| **THANOS SNAP** | Deal 30 damage to ALL enemies |
| **CARD PRINTER GO BRRR** | Draw 3 cards |
| **JUGGERNAUT** | Gain 3 Strength |

### Cursed (negative effects)

| Effect | What happens |
|--------|-------------|
| **NOT STONKS** | Lose 33% of your gold |
| **SKILL ISSUE** | Take 12 damage |
| **NERF BAT** | Lose all block |
| **PAPER TIGER** | Gain 2 Vulnerable |
| **HEALING SURGE** | All enemies heal 20 HP |

### Chaotic (mixed effects)

| Effect | What happens |
|--------|-------------|
| **YOLO** | Take 8 damage, gain 88 gold |
| **GLASS CANNON** | Gain 4 energy, take 10 damage |
| **CRITICAL HIT** | Deal 99 damage to a random enemy |
| **TRADE OFFER** | Heal 20 HP, lose 25 gold |
| **ARMS RACE** | You gain 5 Strength, all enemies gain 3 Strength |
| **NOTHING HAPPENED** | ... |

---

## How to Install Slay the Spire 2 Mods

### Option A: Steam Workshop

Coming soon.

### Option B: Manual Install

1. Download `chaos_spire.dll` and `manifest.json` from the [latest release](https://github.com/chaospire/chaos-spire/releases).
2. Find your Slay the Spire 2 install folder:
   - **macOS:** Right-click STS2 in Steam → Manage → Browse Local Files → `SlayTheSpire2.app/Contents/MacOS/`
   - **Windows:** Right-click STS2 in Steam → Manage → Browse Local Files
   - **Linux:** `~/.steam/steam/steamapps/common/Slay the Spire 2/`
3. Create a `mods/chaos_spire/` folder inside that directory.
4. Place both files in the folder:
   ```
   mods/
   └── chaos_spire/
       ├── manifest.json
       └── chaos_spire.dll
   ```
5. Launch the game. Accept the mod loading prompt when it appears.
6. To uninstall, delete the `mods/chaos_spire/` folder.

---

## How It Works (Technical)

Chaos Spire uses [Harmony](https://github.com/pardeike/Harmony) to patch `Hook.AfterPlayerTurnStart` at runtime. A postfix is chained onto the existing async Task, so the random effect runs after all normal turn-start hooks complete.

Effects call the game's internal command API directly:

| API | Used for |
|-----|----------|
| `PlayerCmd.GainGold` / `LoseGold` | Gold changes |
| `CreatureCmd.Heal` / `Damage` / `GainBlock` | HP, damage, block |
| `PlayerCmd.GainEnergy` | Energy |
| `PowerCmd.Apply<T>` | Strength, Vulnerable, etc. |
| `CardPileCmd.Draw` | Card draw |

No game files are modified or replaced. The mod loads as a standard STS2 mod via the built-in `ModManager`.

---

## Build from Source

Requires [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```bash
git clone https://github.com/chaospire/chaos-spire.git
cd chaos-spire
dotnet build -c Release
```

Output: `bin/Release/net9.0/chaos_spire.dll`

The `.csproj` expects STS2 at the default Steam path. Edit the `GameDir` property in `chaos_spire.csproj` if yours differs.

---

## Add Your Own Effects

Adding a new effect is one `Effects.Add(...)` call in `ChaosRoulette.cs`:

```csharp
Effects.Add(new("YOUR EFFECT NAME",
    ChaosTier.Blessed,  // or Cursed, Chaotic
    async (combatState, choiceContext, player) =>
    {
        await PlayerCmd.GainGold(100, player);
    }));
```

Pull requests are welcome.

---

## FAQ

### Does this mod disable achievements?

Slay the Spire 2 marks modded runs separately. Your unmodded progress is not affected.

### Does this work in multiplayer?

The mod loads in multiplayer, but random effects are rolled locally per player. Desync is possible if effects change shared game state in unexpected ways. Single-player is the intended experience.

### Can I use this with other mods?

Yes. Chaos Spire uses standard Harmony patching and does not conflict with other mods unless they also patch `Hook.AfterPlayerTurnStart`.

### How do I see which effect triggered?

Open the dev console in-game (press `` ` `` or `Shift+8`). Each effect is logged as `[ChaosSpire] BLESSED >> STONKS` or similar.

### How do I enable the dev console?

Add `"full_console": true` to your `settings.save` file. On macOS, it's at:
```
~/Library/Application Support/SlayTheSpire2/steam/<your-steam-id>/settings.save
```

### Is this mod safe? Does it modify game files?

No game files are modified. The mod is loaded at runtime via Harmony and the game's built-in mod loader. Delete the mod folder to fully remove it.

---

## Compatibility

- Slay the Spire 2 v0.99.1+ (Early Access)
- macOS, Windows, Linux
- Single-player and multiplayer
- Works alongside other Harmony-based STS2 mods

## License

[MIT](LICENSE)
