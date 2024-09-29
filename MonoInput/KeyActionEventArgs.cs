using System;
using Microsoft.Xna.Framework.Input;

namespace MonoInput;

public class KeyActionEventArgs(Keys key) : EventArgs
{
    // ReSharper disable once MemberCanBePrivate.Global
    public Keys Key => key;

    public override string ToString()
    {
        return Key.ToString();
    }
}