using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Minesweeper.Tools;

public static class Animator
{
    public static void AnimateBackground (UIElement element, 
                                          Brush from, Brush to,
                                          TimeSpan duration)
    {
        var anim = new ColorAnimation {
            From = ((SolidColorBrush)from).Color, To = ((SolidColorBrush)to).Color,
            Duration = duration
        };

        Storyboard.SetTarget(anim, element);
        Storyboard.SetTargetProperty(anim, new($"({element.GetType().Name}.Background).(SolidColorBrush.Color)"));

        var sb = new Storyboard();
        sb.Children.Add(anim);
        sb.Begin();
    }

    public static void AnimateBlur (UIElement element,
                                    double from, double to,
                                    TimeSpan duration)
    {
        var anim = new DoubleAnimation {
            From = from, To = to,
            Duration = duration
        };

        element.Effect = new BlurEffect();

        Storyboard.SetTarget (anim, element);
        Storyboard.SetTargetProperty(anim, new(BlurEffect.RadiusProperty));

        var sb = new Storyboard();
        sb.Children.Add(anim);
        sb.Begin();
    }
}