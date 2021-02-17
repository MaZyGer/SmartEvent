using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maz.Unity.SmartEvent
{
	public abstract class SmartDelegate
	{
		public object TargetInstance { get; }

		public SmartDelegate(object target)
		{
			this.TargetInstance = target;
		}
	}

	public class EventDelegate<T> : SmartDelegate
	{
		public Action<T> Action { get; }
		public EventDelegate(Action<T> action, object target) : base(target)
		{
			this.Action = action;		
		}
	}

	public static class SmartEvent
	{
		static List<SmartDelegate> delegates = new List<SmartDelegate>();

		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void Reset()
		{
			Clear();
		}

		public static void Clear()
		{
			delegates.Clear();
		}

		/// <summary>
		/// You can only Receive one event per Instance. So second same T will be ignored and not registered.
		/// If you have problems with this use OnReceiveEvent(instance, action)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		public static void OnReceiveEvent<T>(Action<T> action)
		{
			EventDelegate<T> eventDelegate;
			if ((eventDelegate = Find<T>(action.Target)) != null)
			{
				return;
			}

			Action<T> newAction = (a) =>
			{
				action.Invoke((T)a);
			};

			eventDelegate = new EventDelegate<T>(newAction, action.Target);
			delegates.Add(eventDelegate);
		}

		/// <summary>
		/// You can only Receive one event per Instance. So second same T will be ignored and not registered.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		public static void OnReceiveEvent<T>(object target, Action<T> action)
		{
			EventDelegate<T> eventDelegate;
			if ((eventDelegate = Find<T>(target)) != null)
			{
				return;
			}

			Action<T> newAction = (a) =>
			{
				action.Invoke((T)a);
			};

			eventDelegate = new EventDelegate<T>(newAction, target);
			delegates.Add(eventDelegate);
		}

		public static void OnReceiveEventRemove<T>(object target)
		{
			SmartDelegate eventDelegate;
			if ((eventDelegate = Find<T>(target)) == null)
			{
				return;
			}

			if (!delegates.Contains(eventDelegate))
			{
				return;
			}

			delegates.Remove(eventDelegate);
		}

		public static void OnReceiveEventRemove<T>(Action<T> action)
		{
			SmartDelegate eventDelegate;
			if ((eventDelegate = Find<T>(action.Target)) == null)
			{
				return;
			}

			if (!delegates.Contains(eventDelegate))
			{
				return;
			}

			delegates.Remove(eventDelegate);
		}

		/// <summary>
		/// Send to All
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="evt"></param>
		public static void Broadcast<T>(T evt) 
		{
			foreach (var targets in FindAll<T>())
			{
				targets.Action.Invoke(evt);
			}
		}

		/// <summary>
		/// Send to target
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target">Target instance</param>
		/// <param name="action"></param>
		public static void Send<T>(object target, T action)
		{
			var targetEventDelegate = Find<T>(target);
			targetEventDelegate?.Action?.Invoke(action);
		}

		#region Find

		static List<EventDelegate<T>> FindAll<T>()
		{
			delegates.RemoveAll(x => x.TargetInstance == null);

			List<EventDelegate<T>> all = new List<EventDelegate<T>>();

			foreach (var del in delegates)
			{
				var genericDel = del as EventDelegate<T>;
				if (genericDel == null)
					continue;
				else
					all.Add(genericDel);

			}

			return all;
		}

		static EventDelegate<T> Find<T>(object target) 
		{
			delegates.RemoveAll(x => x.TargetInstance == null);

			foreach (var del in delegates)
			{
				var genericDel = del as EventDelegate<T>;
				if (genericDel == null)
					continue;
				else
					if(genericDel.TargetInstance == target)
						return genericDel;
			}

			return null;
		}

		#endregion
	}



}