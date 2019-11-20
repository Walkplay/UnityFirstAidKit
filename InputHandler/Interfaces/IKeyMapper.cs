using Source.InputHandler;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace InputHandler.Interfaces
{
    public interface IKeyMapper
    {
        Subject<KeyValuePair<ECommand, Vector2>> joystickCommandStream { get; }
        Subject<ECommand> buttonCommandStream { get; }
    }
}
