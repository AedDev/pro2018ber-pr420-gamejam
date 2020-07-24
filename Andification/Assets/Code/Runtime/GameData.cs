using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

namespace Andification.Runtime {
	public class GameData : Singleton<GameData> {

		public Observable<bool> InWave { get; private set; }
		public Observable<int> MaxLive { get; private set; }
		public Observable<int> CurrentLive { get; private set; }
		public Observable<int> CurrentMoney { get; private set; }
		public Observable<int> NextWaveEnemyCount { get; private set; }
		public Observable<int> CurrentEnemyCount { get; private set; }

	}
}

