using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Andification.Runtime.View {
	public class AttackableEntityInWorldUI : MonoBehaviour {
		[SerializeField] Slider _healthBar = null;

		Behaviours.Entities.IAttackableEntity _target = null;

		void Start() {
			_healthBar.minValue = 0;
			_healthBar.gameObject.SetActive(false);

			Transform element = transform;
			do {
				_target = element.GetComponent<Behaviours.Entities.IAttackableEntity>();
				element = element.parent;
			} while(_target == null && element != transform.root);

			if(_target == null) {
				Debug.LogError("FATAL: No AttackableEntity found!!");
				Destroy(gameObject); //very hardcore destroyes everything so no borken UI is flying around
			}
		}

		private void FixedUpdate() {
			if(_target.CurrentHealth.value >= _target.MaxHealth) {
				_healthBar.gameObject.SetActive(false);
				return;
			}

			_healthBar.maxValue = _target.MaxHealth;
			_healthBar.value = _target.CurrentHealth.value;
			_healthBar.gameObject.SetActive(true);
		}
	}
}

