using AsserTOOLres;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Controler {
	public class InputAdapter : Singleton<InputAdapter> {

		public System.Action<Vector2Int> OnStartInteract = null; //<! in Screenspace
		public System.Action<Vector2Int> OnStopInteract = null; //<! in Screenspace
		public System.Action<Vector2Int> OnClick = null; //<! in Screenspace
		public System.Action<Vector2Int> OnHoverStart = null; //<! in Screenspace
		public System.Action OnHoverStop = null;
		public System.Action<Vector2> OnMove = null; //<! in normalised
		public System.Action<float> OnZoom = null; //<! in relative value

		float _hoverTime = 1;
		float _timeToClick = 0.25f;
		float _lastClick = float.MinValue;

		bool _isHovering = false;
		Vector2Int _lastPosition;
		float _lastMoveTime = float.MinValue;

		// Update is called once per frame
		void Update() {
			HandleKeyboardAndMouse();
			HandleControler();
			HandleTouch();
		}

		void HandleKeyboardAndMouse() {
			OnMove?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
			OnZoom?.Invoke(Input.GetAxis("Mouse ScrollWheel"));

			Vector2Int currentMousePosition = new Vector2Int((int)Input.mousePosition.x, (int)Input.mousePosition.y);
			if(Input.GetMouseButtonDown(0)) {
				OnStartInteract?.Invoke(currentMousePosition);

				_lastClick = Time.time;
			}else if(Input.GetMouseButtonUp(0)) {
				OnStopInteract?.Invoke(currentMousePosition);

				if(Time.time <= _lastClick + _timeToClick) {
					OnClick?.Invoke(currentMousePosition);
				}
			}

			if(!Input.GetMouseButton(0)) {
				if(_lastPosition != currentMousePosition) {
					if(_isHovering) {
						_isHovering = false;
						OnHoverStop?.Invoke();
					}
					_lastMoveTime = Time.time;
				} else {
					if(!_isHovering && _lastMoveTime + _hoverTime > Time.time) {
						_isHovering = true;
						OnHoverStart?.Invoke(currentMousePosition);
					}
				}
			}
			_lastPosition = currentMousePosition;
		}

		void HandleControler() {
			return;
		}

		void HandleTouch() {
			if(Input.touchCount <= 0)
				return;
		}
	}
}

