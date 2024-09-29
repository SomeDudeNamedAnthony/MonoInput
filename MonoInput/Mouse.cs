using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoInput;

public static class Mouse
{
    private static MouseState _previousState;
    private static MouseState _currentState;
#pragma warning disable CA2211
    public static Vector2 Velocity;
#pragma warning restore CA2211

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    // ReSharper disable once ConvertToConstant.Global
#pragma warning disable CA2211
    public static bool ProcessEvents  = true;
#pragma warning restore CA2211

    public delegate void MouseButtonEventHandler(object sender, MouseButtonEventArgs args);
    public delegate void MouseMovementEventHandler(object sender, MouseMovementEventArgs args);

    private static readonly int ButtonCount = ((MouseButton[])Enum.GetValues(typeof(MouseButton))).Distinct().Count();

    public static event MouseMovementEventHandler OnMove;

    public static event MouseButtonEventHandler OnClick;
    public static event MouseButtonEventHandler OnPress;
    public static event MouseButtonEventHandler OnRelease;

    public static void Update()
    {
        _previousState = _currentState;
        _currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        Velocity = new Vector2(_currentState.X - _previousState.X, _currentState.Y - _previousState.Y);

        if (!ProcessEvents) return;
        ProcessMouseEvents();
    }

    private static void ProcessMouseEvents()
    {
        if (_currentState.Position != _previousState.Position)
        {
            OnMove?.Invoke(null, new MouseMovementEventArgs(_currentState.Position.ToVector2(), _previousState.Position.ToVector2(), Velocity));
        }

        for (var mouseButtonIndex = 0; mouseButtonIndex < ButtonCount; mouseButtonIndex++)
        {
            var mouseButton = (MouseButton)mouseButtonIndex;

            if (IsButtonReleased(mouseButton))
            {
                OnPress?.Invoke(null, new MouseButtonEventArgs(mouseButton, _previousState.Position.ToVector2(), _currentState.Position.ToVector2(), Velocity));
                continue;
            }

            if (IsButtonHeld(mouseButton))
            {
                OnPress?.Invoke(null, new MouseButtonEventArgs(mouseButton, _previousState.Position.ToVector2(), _currentState.Position.ToVector2(), Velocity));
            }

            if (IsButtonPressed(mouseButton))
            {
                OnClick?.Invoke(null, new MouseButtonEventArgs(mouseButton, _previousState.Position.ToVector2(), _currentState.Position.ToVector2(), Velocity));
            }
        }
    }

    public static bool IsButtonHeld(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => _currentState.LeftButton == ButtonState.Pressed,
            MouseButton.Middle => _currentState.MiddleButton == ButtonState.Pressed,
            MouseButton.Right => _currentState.RightButton == ButtonState.Pressed,
            _ => false
        };
    }

    public static bool IsButtonPressed(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => _currentState.LeftButton == ButtonState.Pressed && _previousState.LeftButton != ButtonState.Pressed,
            MouseButton.Middle => _currentState.MiddleButton == ButtonState.Pressed && _previousState.MiddleButton != ButtonState.Pressed,
            MouseButton.Right => _currentState.RightButton == ButtonState.Pressed && _previousState.RightButton != ButtonState.Pressed,
            _ => false
        };
    }

    public static bool IsButtonReleased(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => _currentState.LeftButton != ButtonState.Pressed && _previousState.LeftButton == ButtonState.Pressed,
            MouseButton.Middle => _currentState.MiddleButton != ButtonState.Pressed && _previousState.MiddleButton == ButtonState.Pressed,
            MouseButton.Right => _currentState.RightButton != ButtonState.Pressed && _previousState.RightButton == ButtonState.Pressed,
            _ => false
        };
    }

    public static Vector2 PreviousPosition => _previousState.Position.ToVector2();
    public static Vector2 Position => _previousState.Position.ToVector2();

}