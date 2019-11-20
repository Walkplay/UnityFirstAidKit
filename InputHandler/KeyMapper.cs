using InputHandler.Interfaces;
using Source.InputHandler;
using Source.InputHandler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.InputHandler
{
    public class KeyMapper : IKeyMapper, IDisposable
    {
        private Dictionary<UIKeyCode, ECommand> _map;
        public Subject<KeyValuePair<ECommand, Vector2>> joystickCommandStream { get; private set; }
        public Subject<ECommand> buttonCommandStream { get; private set; }

        private CompositeDisposable disposables = new CompositeDisposable();

        public KeyMapper(Settings settings,  Subject<UIKeyCode> buttonInputStream, Subject<KeyValuePair<UIKeyCode, Vector2>> joystickInputStream)
        {
            joystickCommandStream = new Subject<KeyValuePair<ECommand, Vector2>>();
            buttonCommandStream = new Subject<ECommand>();
            _map = settings.keyValues.ToDictionary(pair => pair.keyCode, pair => pair.command);

            buttonInputStream.Select(code => _map[code]).Subscribe(buttonCommandStream).AddTo(disposables);

            joystickInputStream
                //.Where(x => x.Value != Vector2.zero)
                .Select(pair => new KeyValuePair<ECommand, Vector2>(_map[pair.Key], pair.Value))
                .Subscribe(joystickCommandStream).AddTo(disposables);
        }
            

        [Serializable]
        public class Settings
        {
            public Map[] keyValues;

            [Serializable]
            public struct Map
            {
                public ECommand command;
                public UIKeyCode keyCode;
            }

        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }

}
