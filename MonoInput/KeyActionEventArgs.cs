using System;
using Microsoft.Xna.Framework.Input;

namespace MonoInput;

public class KeyActionEventArgs(Keys key) : EventArgs
{
    public Keys Key => key;
}