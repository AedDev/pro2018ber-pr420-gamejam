using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Andification.Runtime.View {
	public class ShowInWave : MonoBehaviour {
		[SerializeField] bool doShowInWave = true;

		private void Start() {
			GameData.s_instance.InWave.OnValueChangeWithState += OnSwitch;
		}

		private void OnDestroy() {
			if(GameData.Exists())
				GameData.s_instance.InWave.OnValueChangeWithState -= OnSwitch;
		}

		void OnSwitch(Observable<bool> InWave) {
			gameObject.SetActive(InWave.value == doShowInWave);
		}
	}
}

