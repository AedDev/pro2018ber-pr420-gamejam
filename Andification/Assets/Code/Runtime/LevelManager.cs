using Andification.Runtime.Data.ScriptableObjects.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime {
	public class LevelManager : MonoBehaviour {
		[Header("LevelData")]
		[SerializeField] GridData _grid;
		[SerializeField] Vector2[] _towerPositions;
		[SerializeField] WaveData[] _waves;

		[Header("Prefabs")]
		[SerializeField] Sprite _onlyWalkable;
		[SerializeField] Sprite _buildable;
		[SerializeField] Sprite _baricade;
		[SerializeField] Sprite _spawner;
		[SerializeField] Sprite _target;
		[SerializeField] GameObject _towerSlot;
		

		void Start() {
			GameData.s_instance.LevelGrid = new GridSystem.WorldGrid();
			GameData.s_instance.LevelGrid.GridDataReference = _grid;
			//TODO: initialice all tiles
			
			GameData.s_instance.Waves = _waves;

			foreach(var it in _towerPositions) {
				var element = Instantiate(_towerSlot);
				element.transform.position = new Vector3(it.x, it.y, 0);
			}

			GameData.s_instance.LevelGrid.cellChanged += HandleCellChange; 
		}

		void HandleCellChange(object sender, GridSystem.WorldGridCell cell) {
			if(cell.ContentType == GridSystem.GridContentType.Baricade) {
				(cell.Content as SpriteRenderer).sprite = _baricade;
				return;
			}
		}
	}
}

