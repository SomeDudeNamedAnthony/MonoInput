using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MonoInput;

public static class Keyboard
{
    private static KeyboardState _previousState;
    private static KeyboardState _currentState;

    private static readonly int KeyCount = ((Keys[])Enum.GetValues(typeof(Keys))).Distinct().Count();

    public delegate void KeyActionEventHandler(object sender, KeyActionEventArgs e);
    public static event KeyActionEventHandler OnKeyDown;
    public static event KeyActionEventHandler OnKeyPressed;
    public static event KeyActionEventHandler OnKeyReleased;

    // ReSharper disable once MemberCanBePrivate.Global
    public static bool ProcessEvents { get; set; } = true;


    private static Task _eventLoop = new Task(HandleEvents);
    public static void Update()
    {
        UpdateState();

        if (!ProcessEvents)
        {
            return;
        }
        HandleEvents();
    }

    private static void UpdateState()
    {
        _previousState = _currentState;
        _currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
    }


    private static void HandleEvents()
    {
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

    // ReSharper disable once MemberCanBePrivate.Global
    public static bool IsKeyDown(Keys key)
    {
        return _currentState.IsKeyDown(key);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static bool IsKeyPressed(Keys key)
    {
        return _currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static bool IsKeyReleased(Keys key)
    {
        return !_currentState.IsKeyDown(key) && _previousState.IsKeyDown(key);
    }
}