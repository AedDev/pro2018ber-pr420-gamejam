using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;
using Andification.Runtime.Data;

namespace Andification.Runtime {

	[System.Serializable]
	public struct SpawnerData {
		public EnemyUnitConfiguration enemy;
		public int count;
	}

	[System.Serializable]
	public struct WaveData {
		public SpawnerData[] spawners;
	}

	public class GameData : Singleton<GameData> {

		public Observable<bool> InWave { get; private set; }
		public Observable<int> MaxLive { get; private set; }
		public Observable<int> CurrentLive { get; private set; }
		public Observable<int> CurrentMoney { get; private set; }
		public Observable<int> MaxEnemyCount { get; private set; }
		public Observable<int> CurrentEnemyCount { get; private set; }

		public GridSystem.WorldGrid LevelGrid;

		public Observable<int> CurrentWave { get; private set; }
		public WaveData[] Waves;

	}
}

