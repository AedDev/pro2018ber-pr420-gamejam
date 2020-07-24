using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMoveAccessor : MonoBehaviour
{
#pragma warning disable CS0414

	Material _material;

	int _displacement = 1;
	int scale = 6;

	void Start()
	{
		_material = GetComponent<SpriteRenderer>()?.material;
	}

	void Update()
	{
		_displacement = (int)(Mathf.Sin(Time.time)*64f);
		SetDisplacement(_displacement);
	}

	public void SetDisplacement(int displacement) => _material.SetInt("_Displacement", displacement);
}
