using Andification.Runtime.Data.ScriptableObjects.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime {
	public class LevelManager : MonoBehaviour {
		[Header("LevelData")]
		[SerializeField] MapData _map;

		[Header("Prefabs")]
		[SerializeField] Sprite _onlyWalkable;
		[SerializeField] Sprite _buildable;
		[SerializeField] Sprite _baricade;
		[SerializeField] GameObject _spawner;
		[SerializeField] Sprite _target;
		[SerializeField] GameObject _towerSlot;


		void Start() {
			GameData.s_instance.LevelGrid = new GridSystem.WorldGrid();
			GameData.s_instance.LevelGrid.GridDataReference = _map.gridData;

			foreach(var it in GameData.s_instance.LevelGrid.GridDataReference.CellData) {
				if(it.ContentType == GridSystem.GridContentType.Nothing) {
					continue;
				}
				if(it.ContentType == GridSystem.GridContentType.Spawner) {
					var spawner = Instantiate(_spawner);
					//TODO: init Index
					continue;
				}
				if(it.ContentType == GridSystem.GridContentType.Target) {
					GameData.s_instance.TargetPosition = it.CellPosition;
				}

				var element = new GameObject().AddComponent<SpriteRenderer>();
				it.Content = element;

				element.sprite = GetCellSprite(it);
			}

			GameData.s_instance.Waves = _map.waves;

			foreach(var it in _map.towerPositions) {
				var element = Instantiate(_towerSlot);
				element.transform.position = new Vector3(it.x, it.y, 0);
			}

			GameData.s_instance.LevelGrid.cellChanged += HandleCellChange;
		}

		void HandleCellChange(object sender, GridSystem.WorldGridCell cell) {
			(cell.Content as SpriteRenderer).sprite = GetCellSprite(cell);
		}

		Sprite GetCellSprite(GridSystem.WorldGridCell cell) {
			if(cell.ContentType == GridSystem.GridContentType.Baricade) {
				return _baricade;
			}
			if(cell.Buildable) {
				return _buildable;
			}
			if(cell.Walkable) {
				return _onlyWalkable;
			}
			return null;
		}
	}
}

