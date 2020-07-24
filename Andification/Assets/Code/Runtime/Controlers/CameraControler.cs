using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andification.Runtime.Controler {
	[RequireComponent(typeof(Camera))]
	public class CameraControler : MonoBehaviour {
		[SerializeField] float panSpeed = 1;
		[SerializeField] float zoomSpeed = 1;

		Camera _camera = null;

		private void Start() {
			_camera = GetComponent<Camera>();
			InputAdapter.s_instance.OnMove += OnPan;
			InputAdapter.s_instance.OnZoom += OnZoom;
		}

		void OnPan(Vector2 delta) {
			delta *= panSpeed;
			transform.Translate(delta.x, delta.y, 0);
		}

		void OnZoom(float value) {
			_camera.orthographicSize += value * zoomSpeed;
		}
	}
}

