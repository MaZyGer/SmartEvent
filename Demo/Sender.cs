using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maz.Unity.SmartEvent.Demo
{

    public class Sender : MonoBehaviour
    {

        [SerializeField]
        MonoBehaviour target;

        [ContextMenu("Invoke")]
        void SendEvent()
        {
            SmartEvent.Broadcast(new DamageEvent(30) { Attacker = "Soldier" });
        }

        [ContextMenu("Invoke Target")]
        void SendEventTarget()
        {
            SmartEvent.Send(target, new DamageEvent(100)
			{
				Attacker = "Attacker has single target"
			});
        }

    }


}