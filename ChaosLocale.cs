using System.Collections.Generic;
using MegaCrit.Sts2.Core.Saves;

namespace ChaosSpire;

public static class ChaosLocale
{
    // ── Localized effect NAMES (transcreated per culture) ──
    private static readonly Dictionary<(string Key, string Lang), string> EffectNames = new()
    {
        // BLESSED
        { ("STONKS", "eng"), "STONKS" },
        { ("STONKS", "kor"), "떡상" },
        { ("STONKS", "zho"), "赚麻了" },
        { ("STONKS", "jpn"), "爆益" },

        { ("PLOT ARMOR", "eng"), "PLOT ARMOR" },
        { ("PLOT ARMOR", "kor"), "주인공 보정" },
        { ("PLOT ARMOR", "zho"), "主角光环" },
        { ("PLOT ARMOR", "jpn"), "主人公補正" },

        { ("IDDQD", "eng"), "IDDQD" },
        { ("IDDQD", "kor"), "불사신" },
        { ("IDDQD", "zho"), "IDDQD" },
        { ("IDDQD", "jpn"), "IDDQD" },

        { ("UNLIMITED POWER", "eng"), "UNLIMITED POWER" },
        { ("UNLIMITED POWER", "kor"), "무한한 힘이다" },
        { ("UNLIMITED POWER", "zho"), "无限力量" },
        { ("UNLIMITED POWER", "jpn"), "無限の力だ" },

        { ("THANOS SNAP", "eng"), "THANOS SNAP" },
        { ("THANOS SNAP", "kor"), "타노스 핑거스냅" },
        { ("THANOS SNAP", "zho"), "灭霸响指" },
        { ("THANOS SNAP", "jpn"), "サノスの指パッチン" },

        { ("CARD PRINTER GO BRRR", "eng"), "CARD PRINTER GO BRRR" },
        { ("CARD PRINTER GO BRRR", "kor"), "카드 무한 복사기" },
        { ("CARD PRINTER GO BRRR", "zho"), "印卡机启动" },
        { ("CARD PRINTER GO BRRR", "jpn"), "カード刷り放題" },

        { ("JUGGERNAUT", "eng"), "JUGGERNAUT" },
        { ("JUGGERNAUT", "kor"), "광전사" },
        { ("JUGGERNAUT", "zho"), "势不可挡" },
        { ("JUGGERNAUT", "jpn"), "無双" },

        // CURSED
        { ("NOT STONKS", "eng"), "NOT STONKS" },
        { ("NOT STONKS", "kor"), "폭락" },
        { ("NOT STONKS", "zho"), "亏麻了" },
        { ("NOT STONKS", "jpn"), "大暴落" },

        { ("SKILL ISSUE", "eng"), "SKILL ISSUE" },
        { ("SKILL ISSUE", "kor"), "실력 이슈" },
        { ("SKILL ISSUE", "zho"), "菜就多练" },
        { ("SKILL ISSUE", "jpn"), "下手くそ" },

        { ("NERF BAT", "eng"), "NERF BAT" },
        { ("NERF BAT", "kor"), "너프 당함" },
        { ("NERF BAT", "zho"), "惨遭削弱" },
        { ("NERF BAT", "jpn"), "ナーフの嵐" },

        { ("PAPER TIGER", "eng"), "PAPER TIGER" },
        { ("PAPER TIGER", "kor"), "종이호랑이" },
        { ("PAPER TIGER", "zho"), "纸老虎" },
        { ("PAPER TIGER", "jpn"), "張り子の虎" },

        { ("HEALING SURGE", "eng"), "HEALING SURGE" },
        { ("HEALING SURGE", "kor"), "적 힐 받음" },
        { ("HEALING SURGE", "zho"), "敌方回血" },
        { ("HEALING SURGE", "jpn"), "敵が回復した" },

        // CHAOTIC
        { ("YOLO", "eng"), "YOLO" },
        { ("YOLO", "kor"), "욜로" },
        { ("YOLO", "zho"), "搏一把" },
        { ("YOLO", "jpn"), "一か八か" },

        { ("GLASS CANNON", "eng"), "GLASS CANNON" },
        { ("GLASS CANNON", "kor"), "유리대포" },
        { ("GLASS CANNON", "zho"), "玻璃大炮" },
        { ("GLASS CANNON", "jpn"), "ガラスの大砲" },

        { ("CRITICAL HIT", "eng"), "CRITICAL HIT!" },
        { ("CRITICAL HIT", "kor"), "치명타!" },
        { ("CRITICAL HIT", "zho"), "暴击!" },
        { ("CRITICAL HIT", "jpn"), "会心の一撃!" },

        { ("TRADE OFFER", "eng"), "TRADE OFFER" },
        { ("TRADE OFFER", "kor"), "거래 제안" },
        { ("TRADE OFFER", "zho"), "来做个交易" },
        { ("TRADE OFFER", "jpn"), "取引しようぜ" },

        { ("ARMS RACE", "eng"), "ARMS RACE" },
        { ("ARMS RACE", "kor"), "군비 경쟁" },
        { ("ARMS RACE", "zho"), "军备竞赛" },
        { ("ARMS RACE", "jpn"), "軍拡競争" },

        { ("NOTHING HAPPENED", "eng"), "NOTHING HAPPENED" },
        { ("NOTHING HAPPENED", "kor"), "아무 일도 없었다" },
        { ("NOTHING HAPPENED", "zho"), "什么都没发生" },
        { ("NOTHING HAPPENED", "jpn"), "何も起きなかった" },
    };

    // ── Localized DESCRIPTIONS ──
    private static readonly Dictionary<(string Key, string Lang), string> Descriptions = new()
    {
        // BLESSED
        { ("STONKS", "eng"), "Gain 77 Gold" },
        { ("STONKS", "kor"), "77 골드 획득" },
        { ("STONKS", "zho"), "获得 77 金币" },
        { ("STONKS", "jpn"), "77 ゴールド獲得" },

        { ("PLOT ARMOR", "eng"), "Gain 40 Block" },
        { ("PLOT ARMOR", "kor"), "40 방어도 획득" },
        { ("PLOT ARMOR", "zho"), "获得 40 格挡" },
        { ("PLOT ARMOR", "jpn"), "40 ブロック獲得" },

        { ("IDDQD", "eng"), "Heal 30 HP" },
        { ("IDDQD", "kor"), "30 HP 회복" },
        { ("IDDQD", "zho"), "恢复 30 生命值" },
        { ("IDDQD", "jpn"), "30 HP 回復" },

        { ("UNLIMITED POWER", "eng"), "Gain 3 Energy" },
        { ("UNLIMITED POWER", "kor"), "에너지 3 획득" },
        { ("UNLIMITED POWER", "zho"), "获得 3 点能量" },
        { ("UNLIMITED POWER", "jpn"), "エナジー 3 獲得" },

        { ("THANOS SNAP", "eng"), "Deal 30 damage to ALL enemies" },
        { ("THANOS SNAP", "kor"), "모든 적에게 30 피해" },
        { ("THANOS SNAP", "zho"), "对所有敌人造成 30 伤害" },
        { ("THANOS SNAP", "jpn"), "全ての敵に 30 ダメージ" },

        { ("CARD PRINTER GO BRRR", "eng"), "Draw 3 cards" },
        { ("CARD PRINTER GO BRRR", "kor"), "카드 3장 드로우" },
        { ("CARD PRINTER GO BRRR", "zho"), "抽 3 张牌" },
        { ("CARD PRINTER GO BRRR", "jpn"), "カードを 3 枚ドロー" },

        { ("JUGGERNAUT", "eng"), "Gain 3 Strength" },
        { ("JUGGERNAUT", "kor"), "힘 3 획득" },
        { ("JUGGERNAUT", "zho"), "获得 3 点力量" },
        { ("JUGGERNAUT", "jpn"), "筋力 3 獲得" },

        // CURSED
        { ("NOT STONKS", "eng"), "Lose 33% of your Gold" },
        { ("NOT STONKS", "kor"), "골드 33% 손실" },
        { ("NOT STONKS", "zho"), "失去 33% 金币" },
        { ("NOT STONKS", "jpn"), "ゴールドの 33% を失う" },

        { ("SKILL ISSUE", "eng"), "Take 12 damage" },
        { ("SKILL ISSUE", "kor"), "12 피해를 받는다" },
        { ("SKILL ISSUE", "zho"), "受到 12 点伤害" },
        { ("SKILL ISSUE", "jpn"), "12 ダメージを受ける" },

        { ("NERF BAT", "eng"), "Lose all Block" },
        { ("NERF BAT", "kor"), "방어도 전부 제거" },
        { ("NERF BAT", "zho"), "失去所有格挡" },
        { ("NERF BAT", "jpn"), "全ブロックを失う" },

        { ("PAPER TIGER", "eng"), "Gain 2 Vulnerable" },
        { ("PAPER TIGER", "kor"), "취약 2 부여" },
        { ("PAPER TIGER", "zho"), "获得 2 层易伤" },
        { ("PAPER TIGER", "jpn"), "脆弱 2 を得る" },

        { ("HEALING SURGE", "eng"), "All enemies heal 20 HP" },
        { ("HEALING SURGE", "kor"), "모든 적 20 HP 회복" },
        { ("HEALING SURGE", "zho"), "所有敌人恢复 20 生命值" },
        { ("HEALING SURGE", "jpn"), "全ての敵が 20 HP 回復" },

        // CHAOTIC
        { ("YOLO", "eng"), "Take 8 damage, gain 88 Gold" },
        { ("YOLO", "kor"), "8 피해, 88 골드 획득" },
        { ("YOLO", "zho"), "受到 8 伤害，获得 88 金币" },
        { ("YOLO", "jpn"), "8 ダメージ、88 ゴールド獲得" },

        { ("GLASS CANNON", "eng"), "Gain 4 Energy, take 10 damage" },
        { ("GLASS CANNON", "kor"), "에너지 4 획득, 10 피해" },
        { ("GLASS CANNON", "zho"), "获得 4 能量，受到 10 伤害" },
        { ("GLASS CANNON", "jpn"), "エナジー 4 獲得、10 ダメージ" },

        { ("CRITICAL HIT", "eng"), "Deal 99 damage to a random enemy" },
        { ("CRITICAL HIT", "kor"), "랜덤 적 하나에 99 피해" },
        { ("CRITICAL HIT", "zho"), "对随机敌人造成 99 伤害" },
        { ("CRITICAL HIT", "jpn"), "ランダムな敵に 99 ダメージ" },

        { ("TRADE OFFER", "eng"), "Heal 20 HP, lose 25 Gold" },
        { ("TRADE OFFER", "kor"), "20 HP 회복, 25 골드 손실" },
        { ("TRADE OFFER", "zho"), "恢复 20 生命值，失去 25 金币" },
        { ("TRADE OFFER", "jpn"), "20 HP 回復、25 ゴールド失う" },

        { ("ARMS RACE", "eng"), "You +5 Str, enemies +3 Str" },
        { ("ARMS RACE", "kor"), "내 힘 +5, 적 전원 힘 +3" },
        { ("ARMS RACE", "zho"), "你 +5 力量，敌人 +3 力量" },
        { ("ARMS RACE", "jpn"), "自分の筋力 +5、敵全員の筋力 +3" },

        { ("NOTHING HAPPENED", "eng"), "..." },
        { ("NOTHING HAPPENED", "kor"), "..." },
        { ("NOTHING HAPPENED", "zho"), "..." },
        { ("NOTHING HAPPENED", "jpn"), "..." },
    };

    // ── Tier names ──
    private static readonly Dictionary<(string Key, string Lang), string> TierNames = new()
    {
        { ("Blessed", "eng"), "BLESSED" },
        { ("Blessed", "kor"), "축복" },
        { ("Blessed", "zho"), "祝福" },
        { ("Blessed", "jpn"), "祝福" },

        { ("Cursed", "eng"), "CURSED" },
        { ("Cursed", "kor"), "저주" },
        { ("Cursed", "zho"), "诅咒" },
        { ("Cursed", "jpn"), "呪い" },

        { ("Chaotic", "eng"), "CHAOS" },
        { ("Chaotic", "kor"), "혼돈" },
        { ("Chaotic", "zho"), "混沌" },
        { ("Chaotic", "jpn"), "混沌" },
    };

    public static string GetLanguage()
    {
        try { return SaveManager.Instance.SettingsSave.Language ?? "eng"; }
        catch { return "eng"; }
    }

    private static string Lookup(
        Dictionary<(string, string), string> table, string key)
    {
        var lang = GetLanguage();
        if (table.TryGetValue((key, lang), out var val)) return val;
        if (table.TryGetValue((key, "eng"), out var fb)) return fb;
        return key;
    }

    public static string GetEffectName(string key) => Lookup(EffectNames, key);
    public static string GetDescription(string key) => Lookup(Descriptions, key);
    public static string GetTierName(ChaosTier tier) => Lookup(TierNames, tier.ToString());
}
