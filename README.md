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

An on-screen announcement shows the effect name and description each turn, automatically localized to your game language.

---

## Full Effect List

### Blessed (positive effects)

| Effect | EN | KR | What happens |
|--------|----|----|-------------|
| STONKS | STONKS | 떡상 | Gain 77 gold |
| PLOT ARMOR | PLOT ARMOR | 주인공 보정 | Gain 40 block |
| IDDQD | IDDQD | 불사신 | Heal 30 HP |
| UNLIMITED POWER | UNLIMITED POWER | 무한한 힘이다 | Gain 3 energy |
| THANOS SNAP | THANOS SNAP | 타노스 핑거스냅 | Deal 30 damage to ALL enemies |
| CARD PRINTER GO BRRR | CARD PRINTER GO BRRR | 카드 무한 복사기 | Draw 3 cards |
| JUGGERNAUT | JUGGERNAUT | 광전사 | Gain 3 Strength |

### Cursed (negative effects)

| Effect | EN | KR | What happens |
|--------|----|----|-------------|
| NOT STONKS | NOT STONKS | 폭락 | Lose 33% of your gold |
| SKILL ISSUE | SKILL ISSUE | 실력 이슈 | Take 12 damage |
| NERF BAT | NERF BAT | 너프 당함 | Lose all block |
| PAPER TIGER | PAPER TIGER | 종이호랑이 | Gain 2 Vulnerable |
| HEALING SURGE | HEALING SURGE | 적 힐 받음 | All enemies heal 20 HP |

### Chaotic (mixed effects)

| Effect | EN | KR | What happens |
|--------|----|----|-------------|
| YOLO | YOLO | 욜로 | Take 8 damage, gain 88 gold |
| GLASS CANNON | GLASS CANNON | 유리대포 | Gain 4 energy, take 10 damage |
| CRITICAL HIT | CRITICAL HIT! | 치명타! | Deal 99 damage to a random enemy |
| TRADE OFFER | TRADE OFFER | 거래 제안 | Heal 20 HP, lose 25 gold |
| ARMS RACE | ARMS RACE | 군비 경쟁 | You +5 Str, enemies +3 Str |
| NOTHING HAPPENED | NOTHING HAPPENED | 아무 일도 없었다 | ... |

### Supported Languages

Effect names and descriptions are fully localized with culturally adapted translations:

| Language | Example (STONKS) | Example (SKILL ISSUE) | Example (CRITICAL HIT) |
|----------|------------------|-----------------------|------------------------|
| English | STONKS | SKILL ISSUE | CRITICAL HIT! |
| Korean | 떡상 | 실력 이슈 | 치명타! |
| Chinese | 赚麻了 | 菜就多练 | 暴击! |
| Japanese | 爆益 | 下手くそ | 会心の一撃! |

The mod reads your game language setting automatically. Unsupported languages fall back to English.

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

Then add localized names and descriptions in `ChaosLocale.cs`.

Pull requests are welcome.

---

## FAQ

### Does this mod disable achievements?

Slay the Spire 2 marks modded runs separately. Your unmodded progress is not affected.

### Does this work in multiplayer?

The mod loads in multiplayer, but random effects are rolled locally per player. Desync is possible if effects change shared game state in unexpected ways. Single-player is the intended experience.

### Can I use this with other mods?

Yes. Chaos Spire uses standard Harmony patching and does not conflict with other mods unless they also patch `Hook.AfterPlayerTurnStart`.

### How do I see the effect log?

An on-screen announcement appears automatically each turn. You can also open the dev console (press `` ` `` or `Shift+8`) to see the full log.

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
