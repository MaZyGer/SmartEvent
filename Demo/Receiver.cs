using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maz.Unity.SmartEvent.Demo
{
	public class Receiver : MonoBehaviour
	{
		private void OnEnable()
		{
			//SmartEvent.OnReceiveEvent<DamageEvent>(OnReceiveDamage);
		}

		private void OnDisable()
		{
			//SmartEvent.OnReceiveEventRemove<DamageEvent>(OnReceiveDamage);
		}

		public void OnReceiveDamage(DamageEvent damage)
		{
			Debug.Log(damage.Attacker);
			Debug.Log(damage.Amount);
		}
	}

}