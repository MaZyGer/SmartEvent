using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maz.Unity.SmartEvent.Demo
{
    public class OnDamageReceive : MonoBehaviour
    {
		[SerializeField]
		UnityEvent<DamageEvent> OnDamageReceiveEvent;

		private void OnEnable()
		{
			SmartEvent.OnReceiveEvent<DamageEvent>(dmg => OnDamageReceiveEvent.Invoke(dmg));

		}

		private void OnDisable()
		{
			SmartEvent.OnReceiveEventRemove<DamageEvent>(this);
		}
	}

    public class DamageEvent 
    {
        public string Attacker;
        public int Amount;

        public DamageEvent(int amount)
        {
            Amount = amount;
        }
    }
}