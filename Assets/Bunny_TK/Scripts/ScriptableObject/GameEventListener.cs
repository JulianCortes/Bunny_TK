using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bunny_TK.DataDriven
{
    public class GameEventListener : IGameEventListener
    {
        public GameEvent gameEvent;
        [Space(10)]
        public UnityEvent response;

        public void OnEventRaised(GameEvent ge)
        {
            response.Invoke();
        }

        private void OnEnable()
        {
            if (gameEvent != null)
                gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent != null)
                gameEvent.UnregisterListener(this);
        }
    }
}
