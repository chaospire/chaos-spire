using Godot;

namespace ChaosSpire;

/// <summary>
/// Shows a floating announcement at the top-center of the screen
/// with tier, effect name, and localized description.
/// Fades out after a short delay.
/// </summary>
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

        var colorHex = color.ToHtml(false);

        // ── CanvasLayer on top ──
        var layer = new CanvasLayer();
        layer.Layer = 100;

        // ── Background panel ──
        var panel = new PanelContainer();
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Color(0f, 0f, 0f, 0.75f);
        styleBox.SetCornerRadiusAll(14);
        styleBox.SetContentMarginAll(24);
        panel.AddThemeStyleboxOverride("panel", styleBox);

        // ── Text: tier + name + description ──
        var label = new RichTextLabel();
        label.BbcodeEnabled = true;
        label.FitContent = true;
        label.ScrollActive = false;
        label.AutowrapMode = TextServer.AutowrapMode.Off;
        label.Text =
            $"[center][font_size=18][color=#{colorHex}]{tierName}[/color][/font_size]\n" +
            $"[font_size=38][b][color=#{colorHex}]{localizedName}[/color][/b][/font_size]\n" +
            $"[font_size=20][color=#bbbbbb]{description}[/color][/font_size][/center]";

        panel.AddChild(label);

        // ── Center at top ──
        var container = new CenterContainer();
        container.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.CenterTop);
        container.OffsetTop = 50;
        container.OffsetBottom = 220;
        container.OffsetLeft = -400;
        container.OffsetRight = 400;
        container.AddChild(panel);

        layer.AddChild(container);
        viewport.AddChild(layer);

        // ── Fade out ──
        var tween = viewport.CreateTween();
        tween.TweenInterval(2.0);
        tween.TweenProperty(container, "modulate:a", 0.0f, 0.8f);
        tween.TweenCallback(Callable.From(() => layer.QueueFree()));
    }
}
