using Godot;
using MegaCrit.Sts2.Core.Localization.Fonts;

namespace ChaosSpire;

public static class ChaosAnnouncer
{
    public static void Show(string effectName, ChaosTier tier)
    {
        var viewport = ((SceneTree)Engine.GetMainLoop()).Root;
        if (viewport == null) return;

        var tierName = ChaosLocale.GetTierName(tier);
        var localizedName = ChaosLocale.GetEffectName(effectName);
        var description = ChaosLocale.GetDescription(effectName);

        var color = tier switch
        {
            ChaosTier.Blessed => new Color(1f, 0.84f, 0f),
            ChaosTier.Cursed  => new Color(1f, 0.3f, 0.3f),
            _                 => new Color(0.3f, 0.9f, 1f),
        };

        // ── CanvasLayer on top of everything ──
        var layer = new CanvasLayer();
        layer.Layer = 100;

        // ── Background panel ──
        var panel = new PanelContainer();
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Color(0f, 0f, 0f, 0.75f);
        styleBox.SetCornerRadiusAll(14);
        styleBox.ContentMarginTop = 16;
        styleBox.ContentMarginBottom = 16;
        styleBox.ContentMarginLeft = 32;
        styleBox.ContentMarginRight = 32;
        panel.AddThemeStyleboxOverride("panel", styleBox);

        // ── Vertical layout ──
        var vbox = new VBoxContainer();
        vbox.Alignment = BoxContainer.AlignmentMode.Center;
        vbox.AddThemeConstantOverride("separation", 4);

        // ── Tier label ──
        var tierLabel = new Label();
        tierLabel.Text = tierName;
        tierLabel.HorizontalAlignment = HorizontalAlignment.Center;
        tierLabel.AddThemeColorOverride("font_color", color.Lerp(Colors.White, 0.3f));
        tierLabel.AddThemeFontSizeOverride("font_size", 20);
        ApplyGameFont(tierLabel, FontType.Bold);
        vbox.AddChild(tierLabel);

        // ── Effect name ──
        var nameLabel = new Label();
        nameLabel.Text = localizedName;
        nameLabel.HorizontalAlignment = HorizontalAlignment.Center;
        nameLabel.AddThemeColorOverride("font_color", color);
        nameLabel.AddThemeFontSizeOverride("font_size", 42);
        ApplyGameFont(nameLabel, FontType.Bold);
        vbox.AddChild(nameLabel);

        // ── Description ──
        var descLabel = new Label();
        descLabel.Text = description;
        descLabel.HorizontalAlignment = HorizontalAlignment.Center;
        descLabel.AddThemeColorOverride("font_color", new Color(0.8f, 0.8f, 0.8f));
        descLabel.AddThemeFontSizeOverride("font_size", 20);
        ApplyGameFont(descLabel, FontType.Regular);
        vbox.AddChild(descLabel);

        panel.AddChild(vbox);

        // ── Position: top-center ──
        var anchor = new Control();
        anchor.SetAnchorsPreset(Control.LayoutPreset.TopWide);
        anchor.CustomMinimumSize = new Vector2(0, 160);

        var center = new CenterContainer();
        center.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
        center.GrowHorizontal = Control.GrowDirection.Both;
        center.OffsetTop = 50;
        center.AddChild(panel);

        anchor.AddChild(center);
        layer.AddChild(anchor);
        viewport.AddChild(layer);

        // ── Fade out and remove ──
        var tween = viewport.CreateTween();
        tween.TweenInterval(2.0);
        tween.TweenProperty(anchor, "modulate:a", 0.0f, 0.8f);
        tween.TweenCallback(Callable.From(() => layer.QueueFree()));
    }

    private static void ApplyGameFont(Label label, FontType fontType)
    {
        try
        {
            var lang = ChaosLocale.GetLanguage();
            if (FontManager.NeedsFontSubstitution(lang))
            {
                var font = FontManager.GetSubstituteFont(lang, fontType);
                if (font != null)
                {
                    label.AddThemeFontOverride("font", font);
                    return;
                }
            }
        }
        catch { /* fall through to default */ }
    }
}
