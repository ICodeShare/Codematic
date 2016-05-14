using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Threading;
namespace Maticsoft.Utility
{
	public class Cache
	{
		protected Hashtable _Cache = new Hashtable();
		protected object _LockObj = new object();
		public int Count
		{
			get
			{
				return this._Cache.Count;
			}
		}
		public virtual object GetObject(object key)
		{
			if (this._Cache.ContainsKey(key))
			{
				return this._Cache[key];
			}
			return null;
		}
		public void SaveCache(object key, object value)
		{
			EventSaveCache eventSaveCache = new EventSaveCache(this.SetCache);
			eventSaveCache.BeginInvoke(key, value, new AsyncCallback(this.Results), null);
		}
		protected virtual void SetCache(object key, object value)
		{
			object lockObj;
			Monitor.Enter(lockObj = this._LockObj);
			try
			{
				if (!this._Cache.ContainsKey(key))
				{
					this._Cache.Add(key, value);
				}
			}
			finally
			{
				Monitor.Exit(lockObj);
			}
		}
		private void Results(IAsyncResult ar)
		{
			EventSaveCache eventSaveCache = (EventSaveCache)((AsyncResult)ar).AsyncDelegate;
			eventSaveCache.EndInvoke(ar);
		}
		public virtual void DelObject(object key)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = this._Cache.SyncRoot);
			try
			{
				this._Cache.Remove(key);
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		public virtual void Clear()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = this._Cache.SyncRoot);
			try
			{
				this._Cache.Clear();
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
	}
}
