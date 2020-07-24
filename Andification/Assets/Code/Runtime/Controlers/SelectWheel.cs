using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Controler {
	public class SelectWheel : MonoBehaviour {
		[System.Serializable]
		struct selectionData {
			public GameObject selector;
			public Sprite thumbnail;
		}
		[SerializeField] selectionData[] _selections = null;
		[SerializeField] Sprite _divider = null;
		[SerializeField] GameObject _holder = null;

		bool _isActive = false;
		bool _waitForClick = true;
		Vector2Int _starPos = Vector2Int.zero;
		float _cellsize = 0;

		void Start() {
			InputAdapter.s_instance.OnInteractStart += HandleActivate;

			_holder = Instantiate(_holder);
			_holder.transform.parent = transform;
			_holder.SetActive(false);
			_cellsize = 2 * Mathf.PI / _selections.Length;
			float distance = 1;

			for(int i = 0; i < _selections.Length; i++) {
				float angle = _cellsize * i;

				var divider = new GameObject().AddComponent<SpriteRenderer>();
				divider.sprite = _divider;
				divider.transform.Rotate(Vector3.left, Mathf.Rad2Deg * angle);
				divider.transform.position = new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance, 0);

				angle -= _cellsize / 2;

				var element = new GameObject().AddComponent<SpriteRenderer>();

				element.sprite = _selections[i].thumbnail;
				element.transform.parent = _holder.transform;
				element.transform.position = new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance, 0);
			}
		}

		private void OnDestroy() {
			if(InputAdapter.Exists()) {
				InputAdapter.s_instance.OnInteractStart -= HandleActivate;
				if(_isActive) {
					InputAdapter.s_instance.OnInteractStop -= HandleReleace;
				}
			}
		}

		void HandleActivate(Vector2Int screenPos) {
			if(_isActive)
				return;

			_starPos = screenPos;
			var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
			//TODO: get wether ther ist a towerspot or not
			Activate(worldPos);

			_waitForClick = true;
			InputAdapter.s_instance.OnInteractStop += HandleReleace;
		}

		void HandleReleace(Vector2Int screenPos, bool isClick) {
			if(_waitForClick && isClick) {
				_waitForClick = false;
				return;
			}

			Selected(Mathf.Atan2(screenPos.y - _starPos.y, screenPos.x - _starPos.x));
			InputAdapter.s_instance.OnInteractStop -= HandleReleace;
		}

		void Activate(Vector2 worldPos) {
			_isActive = true;
			_holder.SetActive(true);
		}

		void Selected(float angle) {
			_isActive = false;
			_holder.SetActive(false);
			var element = _selections[(int)(angle / _cellsize)];

			//TODO: execute selection
		}
	}
}

