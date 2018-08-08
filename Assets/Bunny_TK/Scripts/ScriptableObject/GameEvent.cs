using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.DataDriven
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Data Driven/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        [TextArea(2, 5)]
        [SerializeField]
        protected string Description;
        private List<IGameEventListener> listeners = new List<IGameEventListener>();

        [MethodButton]
        public void Raise()
        {
            //From last, so if a listener removes itslef on event raised we do not go out of range.
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(this);
        }

        public void RegisterListener(IGameEventListener listener)
        {
            listeners.Add(listener);
        }
        public void UnregisterListener(IGameEventListener listener)
        {
            listeners.Remove(listener);
        }

    }
}
