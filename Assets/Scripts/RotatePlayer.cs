using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {

	public float _sensitivity;
	public Vector3 _mouseReference;
	public Vector3 _mouseOffset;
	public Vector3 _rotation;
	public bool _isRotating;

	void Start ()
	{
		_sensitivity = 0.4f;
		_rotation = Vector3.zero;
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0)){
			// rotating flag
			_isRotating = true;

			// store mouse
			_mouseReference = Input.mousePosition;
		}
		if(Input.GetMouseButtonUp(0)){
			// rotating flag
			_isRotating = false;
		}
		if(_isRotating)
		{
			// offset
			_mouseOffset = (Input.mousePosition - _mouseReference);

			// apply rotation
			_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

			// rotate
			transform.Rotate(_rotation);

			// store mouse
			_mouseReference = Input.mousePosition;
		}
	}

	void OnMouseDown()
	{

	}

	void OnMouseUp()
	{
		
	}
}
