﻿using UnityEngine;
using System.Collections;

public class RotateGO : MonoBehaviour
{

	public enum Axis{
		X,
		Y,
		Z,
	}

	public Axis rotateAround = Axis.Y;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(rotateAround) 
		{
			case Axis.Y:
				gameObject.transform.RotateAround (transform.position, transform.right, Time.deltaTime * 90f);
				break;
			case Axis.X:
				gameObject.transform.RotateAround (transform.position, transform.up, Time.deltaTime * 90f);
				break;
			case Axis.Z:
				gameObject.transform.RotateAround (transform.position, transform.forward, Time.deltaTime * 90f);
				break;
			default:
				gameObject.transform.RotateAround (transform.position, transform.right, Time.deltaTime * 90f);
				break;
		}
	}
}
