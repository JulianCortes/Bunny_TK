using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.DataDriven
{
    public interface IGameEventListener
    {
         void OnEventRaised(GameEvent ge);
    }
}
