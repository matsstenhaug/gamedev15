﻿using UnityEngine;

public class PlayerCam : BoltSingletonPrefab<PlayerCam>
{
//		// damp velocity of camera
//		Vector3 _velocity;
		float zoom = 100.0f;
		// camera target
		Transform _target;

//  // if we are aiming or not
//  bool _aiming = false;
//
//  // current camera distance
//  float _distance = 0f;
//
//  // accumulated time for aiming transition
//  float _aimingAcc = 0f;

		[SerializeField]
		Transform
				cam;
//
//  [SerializeField]
//  float height = 2.3f;
//
//  [SerializeField]
//  float offset = 0.75f;
//
//  [SerializeField]
//  float aimingDistance = 1f;
//
//  [SerializeField]
//  float runningDistance = 3f;
//
//  [SerializeField]
//  float runningSmoothTime = 0.1f;
//
//  [SerializeField]
//  Transform dummyRig;
//
//  [SerializeField]
//  Transform dummyTarget;

		public new Camera camera {
				get { return cam.camera; }
		}

//  public System.Func<int> getHealth;
//  public System.Func<bool> getAiming;
//  public System.Func<float> getPitch;

		void Awake ()
		{
				DontDestroyOnLoad (gameObject);
//    _distance = runningDistance;
		}

//  void LateUpdate() {
//    UpdateCamera(true);
//  }

		void Update ()//Camera (bool allowSmoothing)
		{
//    if (_target) {
//      GrayscaleEffect ge = GetComponentInChildren<GrayscaleEffect>();
//
//      var h = getHealth != null ? getHealth() : 100;
//      var a = getAiming != null ? getAiming() : false;
//      var p = getPitch != null ? getPitch() : 0f;
//
//      if (h >= 85) {
//        ge.ramp = 0f;
//      }
//      else {
//        ge.ramp = 1f - ((h / 85f));
//      }
//
//      Screen.lockCursor = true;
//      Screen.showCursor = false;
//
//      if (_aiming) {
//        if (a == false) {
//          _aiming = false;
//          _aimingAcc = 0f;
//        }
//      }
//      else {
//        if (a) {
//          _aiming = true;
//          _aimingAcc = 0f;
//        }
//      }
//
////      _aimingAcc += Time.deltaTime;
//
////      if (_aiming) {
////        _distance = Mathf.Lerp(_distance, aimingDistance, _aimingAcc / 0.4f);
////      }
////      else {
////        _distance = Mathf.Lerp(_distance, runningDistance, _aimingAcc / 0.4f);
////      }
//
				Vector3 pos;
				Quaternion rot;
//
//      CalculateCameraTransform(_target, p, _distance, out pos, out rot);
//
//      if (!_aiming || allowSmoothing) {
//        pos = Vector3.SmoothDamp(transform.position, pos, ref _velocity, runningSmoothTime);
//      }
//
            if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
						zoom -= 20.0f;
			            
				}
				if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
						zoom += 20.0f;
				}
//      transform.position = pos;
//      transform.rotation = rot;
//
				//transform.localRotation = Quaternion.identity;
				if (_target != null) {		
						Vector3 camPos = new Vector3 ();
						camPos.z = _target.position.z - 100; 
						camPos.x = _target.position.x;
						if (zoom < 50.0f) {
								zoom = 50.0f;
						}
						if (zoom > 200.0f) {
								zoom = 200.0f;
						}
                        camPos.y = 120;//_target.position.y + zoom;
		
						transform.position = camPos;
						transform.LookAt (_target.position);
				}
		}

		public void SetTarget (BoltEntity entity)
		{
				_target = entity.transform;
				// UpdateCamera(false);
		}

//  public void CalculateCameraAimTransform(Transform target, float pitch, out Vector3 pos, out Quaternion rot) {
//    CalculateCameraTransform(target, pitch, aimingDistance, out pos, out rot);
//  }

//  public void CalculateCameraTransform(Transform target, float pitch, float distance, out Vector3 pos, out Quaternion rot) {
//
//    // copy transform to dummy
//    dummyTarget.position = target.position;
//    dummyTarget.rotation = target.rotation;
//
//    // move position to where we want it
//    dummyTarget.position += new Vector3(0, height, 0);
//    dummyTarget.position += dummyTarget.right * offset;
//
//    // clamp and calculate pitch rotation
//    Quaternion pitchRotation = Quaternion.Euler(pitch, 0, 0);
//
//    pos = dummyTarget.position;
//    pos += (-dummyTarget.forward * distance);
//
//    pos = dummyTarget.InverseTransformPoint(pos);
//    pos = pitchRotation * pos;
//    pos = dummyTarget.TransformPoint(pos);
//
//    // calculate look-rotation by setting position and looking at target
//    dummyRig.position = pos;
//    dummyRig.LookAt(dummyTarget.position);
//
//    rot = dummyRig.rotation;
//  }
}