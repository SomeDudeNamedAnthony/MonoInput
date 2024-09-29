using Microsoft.Xna.Framework;

namespace MonoInput;

public class MouseMovementEventArgs(Vector2 previousMousePosition, Vector2 mousePosition, Vector2 velocity)
{
    // ReSharper disable once MemberCanBePrivate.Global
    public Vector2 PreviousMousePosition => previousMousePosition;

    // ReSharper disable once MemberCanBePrivate.Global
    public Vector2 MousePosition => mousePosition;

    public override string ToString()
    {
        return $"" +
               $"Current Position: [{MousePosition.X},{MousePosition.Y}]\n" +
               $"Previous Position: [{PreviousMousePosition.X},{PreviousMousePosition.Y}]\n" +
               $"Velocity: {velocity}\n";
    }
}