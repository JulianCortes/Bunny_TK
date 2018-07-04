using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.DataDriven
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Data Driven/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        [MethodButton]
        public void Raise()
        {
            //From last, so if a listener removes itslef on event raised we do not go out of range.
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }
        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }

    }
}
