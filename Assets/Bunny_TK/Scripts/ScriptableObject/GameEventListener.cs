using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bunny_TK.DataDriven
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        [Space(10)]
        public UnityEvent response;

        public void OnEventRaised()
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
