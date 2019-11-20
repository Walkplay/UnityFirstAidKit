using System;
using System.Collections.Generic;
using Assets.Source.InputHandler;
using InputHandler.Interfaces;
using Source.InputHandler.Data;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.InputHandler
{
    public class InputHandlerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        private Subject<UIKeyCode> buttonInputStream = new Subject<UIKeyCode>();
        private Subject<KeyValuePair<UIKeyCode, Vector2>> joystickInputStream = new Subject<KeyValuePair<UIKeyCode, Vector2>>();

        public override void InstallBindings()
        {
            Container.BindInstance(buttonInputStream).AsSingle();
            Container.BindInstance(joystickInputStream).AsSingle();
            Container.Bind<IKeyMapper>().To<KeyMapper>().AsSingle();
            Run();
        }

        private void Run()
        {
            // listen buttons input
            foreach (var buttonMap in _settings.actions)
                buttonMap.button.OnClickAsObservable()
                    .Select(x => buttonMap.code)
                    .Subscribe(buttonInputStream)
                    .AddTo(this);

            // listen joystick input
            foreach (var joystickMap in _settings.ranges)
                Observable.EveryLateUpdate()
                    .Select(_ => new KeyValuePair<UIKeyCode, Vector2>(joystickMap.code, joystickMap.joystick.Direction))
                    .Subscribe(joystickInputStream)
                    .AddTo(this);

        }
    }

    [Serializable]
    public struct Settings
    {
        public FrameCountType FrameCountType;

        public ButtonMap[] actions;
        public JoystickMap[] ranges;
    }

    /// <summary>
    /// An action is a single-time thing, like casting a spell or opening a door;
    /// generally if the player just holds the button down, the action should only happen once,
    /// generally when the button is first pressed,
    /// or when it is finally released. "Key repeat" should not affect actions.
    /// </summary>
    [Serializable]
    public struct ButtonMap
    {
        public UIKeyCode code;
        public Button button;
    }

    /// <summary>
    /// Ranges are most useful for dealing with analog input, such as joysticks, analog controller thumbtacks, and mice.
    /// </summary>
    [Serializable]
    public struct JoystickMap
    {
        public UIKeyCode code;
        //public AxisOptions
        public Joystick joystick;
    }
}