using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Maz.Unity.SmartEvent.Test
{
	public class Test
	{
		class PlayerHealthEvent
		{
			public int Health;
		}

		// A Test behaves as an ordinary method
		[Test]
		public void TestPlayerHealth()
		{
			int testValue = 100;

			SmartEvent.OnReceiveEvent<PlayerHealthEvent>(this, evt =>
			{
				Assert.AreEqual(testValue, evt.Health);

			});

			SmartEvent.Broadcast(new PlayerHealthEvent() { Health = 100 });

			SmartEvent.Clear();
		}

		[Test]
		public void TestRegisterUnregister()
		{
			int resultvalue = 0;
			SmartEvent.OnReceiveEvent<PlayerHealthEvent>(this, evt =>
			{
				resultvalue = evt.Health;
			});

			SmartEvent.Broadcast(new PlayerHealthEvent() { Health = 100 });

			Assert.AreEqual(100, resultvalue, "Maybe OnReceiveEvent was not called");

			resultvalue = 50;
			SmartEvent.OnReceiveEventRemove<PlayerHealthEvent>(this);

			SmartEvent.Broadcast(new PlayerHealthEvent() { Health = 100 });

			Assert.AreEqual(resultvalue, 50, "Maybe OnReceiveEvent was called and couldn't get removed");

			SmartEvent.Clear();
		}

	}

}