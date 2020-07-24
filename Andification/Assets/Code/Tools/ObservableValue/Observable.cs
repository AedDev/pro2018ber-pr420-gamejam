using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
	public class Observable<T> {
		#region ===== ===== CALLBACK ===== =====

		public System.Action OnValueChange;
		public System.Action<Observable<T>> OnValueChangeWithState;

		#endregion
		#region ===== ===== API ===== =====

		public T value {
			get {
				return _backingValue;
			}
			set {
				if(value.Equals(_backingValue))
					return;
				_backingValue = value;
				OnValueChange?.Invoke();
				OnValueChangeWithState?.Invoke(this);
			}
		}

		#endregion
		#region ===== ===== CORE ===== =====

		T _backingValue;

		#endregion
	}
}
