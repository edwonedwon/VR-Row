using System;
using System.Threading;
using UnityEngine;
using WebSocketSharp.Server;

namespace WebSocketSharp.Unity.Editor
{
	public class Chat : WebSocketService
	{
		static int _num = 0;

		string _name;
		string _prefix;

		public Chat ()
			: this (null)
		{
		}

		public Chat (string prefix)
		{
			_prefix = prefix ?? "anon#";
		}

		string GetName ()
		{
			return Context.QueryString ["name"] ?? (_prefix + GetNum ());
		}

		int GetNum ()
		{
			return Interlocked.Increment (ref _num);
		}

		protected override void OnOpen ()
		{
			_name = GetName ();
		}

		protected override void OnMessage (MessageEventArgs e)
		{
			Sessions.Broadcast (String.Format ("{0}: {1}", _name, e.Data));
		}

		protected override void OnClose (CloseEventArgs e)
		{
			Sessions.Broadcast (String.Format ("{0} got logged off...", _name));
		}
	}
}
