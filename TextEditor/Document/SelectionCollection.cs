using System;
using System.Collections;
namespace LTP.TextEditor.Document
{
	[Serializable]
	public class SelectionCollection : CollectionBase
	{
		public class ISelectionEnumerator : IEnumerator
		{
			private IEnumerator baseEnumerator;
			private IEnumerable temp;
			public ISelection Current
			{
				get
				{
					return (ISelection)this.baseEnumerator.Current;
				}
			}
			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}
			public ISelectionEnumerator(SelectionCollection mappings)
			{
				this.temp = mappings;
				this.baseEnumerator = this.temp.GetEnumerator();
			}
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}
			bool IEnumerator.MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}
			public void Reset()
			{
				this.baseEnumerator.Reset();
			}
			void IEnumerator.Reset()
			{
				this.baseEnumerator.Reset();
			}
		}
		public ISelection this[int index]
		{
			get
			{
				return (ISelection)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}
		public SelectionCollection()
		{
		}
		public SelectionCollection(SelectionCollection value)
		{
			this.AddRange(value);
		}
		public SelectionCollection(ISelection[] value)
		{
			this.AddRange(value);
		}
		public int Add(ISelection value)
		{
			return base.List.Add(value);
		}
		public void AddRange(ISelection[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}
		public void AddRange(SelectionCollection value)
		{
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}
		public bool Contains(ISelection value)
		{
			return base.List.Contains(value);
		}
		public void CopyTo(ISelection[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
		public int IndexOf(ISelection value)
		{
			return base.List.IndexOf(value);
		}
		public void Insert(int index, ISelection value)
		{
			base.List.Insert(index, value);
		}
		public new SelectionCollection.ISelectionEnumerator GetEnumerator()
		{
			return new SelectionCollection.ISelectionEnumerator(this);
		}
		public void Remove(ISelection value)
		{
			base.List.Remove(value);
		}
	}
}
