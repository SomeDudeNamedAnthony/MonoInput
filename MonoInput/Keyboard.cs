using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace MonoInput;

public static class Keyboard
{
    private static KeyboardState _previousState;
    private static KeyboardState _currentState;

    private static int KeyCount = ((Keys[])Enum.GetValues(typeof(Keys))).Distinct().Count();

    public delegate void KeyActionEventHandler(object sender, KeyActionEventArgs e);
    public static event KeyActionEventHandler OnKeyDown;
    public static event KeyActionEventHandler OnKeyPressed;
    public static event KeyActionEventHandler OnKeyReleased;

    public static bool ProcessEvents { get; set; } = true;

    public static void Update()
    {
        _previousState = _currentState;
        _currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        if (!ProcessEvents) return;
        for (var keyIndex = 0; keyIndex < KeyCount; keyIndex++)
        {
            var key = (Keys) keyIndex;
            if (IsKeyDown(key))
            {
                OnKeyDown?.Invoke(null, new KeyActionEventArgs(key));
            }

            if (IsKeyPressed(key))
            {
                OnKeyPressed?.Invoke(null, new KeyActionEventArgs(key));
            }

            if (IsKeyReleased(key))
            {
                OnKeyReleased?.Invoke(null, new KeyActionEventArgs(key));
            }
        }
    }

    public static bool IsKeyDown(Keys key)
    {
        return _currentState.IsKeyDown(key);
    }

    public static bool IsKeyPressed(Keys key)
    {
        return _currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key);
    }

    public static bool IsKeyReleased(Keys key)
    {
        return !_currentState.IsKeyDown(key) && _previousState.IsKeyDown(key);
    }
}