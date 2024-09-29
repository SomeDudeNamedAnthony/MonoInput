using Microsoft.Xna.Framework;

namespace MonoInput;

public class MouseButtonEventArgs(
    MouseButton button,
    Vector2 previousMousePosition,
    Vector2 mousePosition,
    Vector2 velocity)
    : MouseMovementEventArgs(previousMousePosition, mousePosition, velocity)
{
    // ReSharper disable once MemberCanBePrivate.Global
    public MouseButton Button { get; } = button;

    public override string ToString()
    {
        return $"" +
               $"{base.ToString()}\n" +
               $"Button: {Button}";
    }
}