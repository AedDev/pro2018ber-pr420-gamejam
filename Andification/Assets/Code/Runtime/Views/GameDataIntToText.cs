using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AsserTOOLres;

namespace Andification.Runtime.View {
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class GameDataIntToText : MonoBehaviour {
		TextMeshProUGUI _reference = null;

		enum observerType{
			Live,
			Money,
			NextEnemyCount,
			CurrentEnemyCount,
		} 
		[SerializeField] observerType _observerType = observerType.Live;

		private void Start() {
			_reference = GetComponent<TextMeshProUGUI>();

			switch(_observerType) {
			case observerType.Live:
				GameData.s_instance.CurrentLive.OnValueChangeWithState += OnValueChange;
				break;
			case observerType.Money:
				GameData.s_instance.CurrentMoney.OnValueChangeWithState += OnValueChange;
				break;
			case observerType.NextEnemyCount:
				GameData.s_instance.NextWaveEnemyCount.OnValueChangeWithState += OnValueChange;
				break;
			case observerType.CurrentEnemyCount:
				GameData.s_instance.CurrentEnemyCount.OnValueChangeWithState += OnValueChange;
				break;
			default:
				break;
			}
		}

		private void OnDestroy() {
			if(GameData.Exists()) {
				switch(_observerType) {
				case observerType.Live:
					GameData.s_instance.CurrentLive.OnValueChangeWithState -= OnValueChange;
					break;
				case observerType.Money:
					GameData.s_instance.CurrentMoney.OnValueChangeWithState -= OnValueChange;
					break;
				case observerType.NextEnemyCount:
					GameData.s_instance.NextWaveEnemyCount.OnValueChangeWithState -= OnValueChange;
					break;
				case observerType.CurrentEnemyCount:
					GameData.s_instance.CurrentEnemyCount.OnValueChangeWithState -= OnValueChange;
					break;
				default:
					break;
				}
			}
		}

		void OnValueChange(Observable<int> value) {
			_reference.text = value.value.ToString();
		}
	}
}

