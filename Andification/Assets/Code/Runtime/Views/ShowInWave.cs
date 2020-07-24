using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.View {
	public class ShowInWave : MonoBehaviour {
		[SerializeField] bool doShowInWave = true;

		private void Start() {
			//TODO: bind Actions
		}

		private void OnDestroy() {
			//TODO: debind Actions
		}

		void OnSwitchToWave() {
			gameObject.SetActive(doShowInWave);
		}

		void OnSwitchToBuild() {
			gameObject.SetActive(!doShowInWave);
		}
	}
}

