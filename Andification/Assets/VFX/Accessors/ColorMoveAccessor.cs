﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMoveAccessor : MonoBehaviour
{
	Material _material;

	int _displacement = 1;
	int scale = 6;
	Color color;

	// Start is called before the first frame update
	void Start()
	{
		_material = GetComponent<SpriteRenderer>()?.material;
	}

	// Update is called once per frame
	void Update()
	{
		_displacement = (int)(Mathf.Sin(Time.time)*64f);
		SetDisplacement(_displacement);
	}

	public void SetDisplacement(int displacement) => _material.SetInt("_Displacement", displacement);
}