using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Controler {
	public class EnterWave : MonoBehaviour {
		public void Execute() {
			GameData.s_instance.InWave.value = true;
		}
	}
}

