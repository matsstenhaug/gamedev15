﻿using UnityEngine;
using System.Collections;

public class Boomnana : MonoBehaviour
{

	public GameObject owner;
	//private GameObject thisObj;
	//private float spawnTime;
//	float lateralspeed; 
	bool movingBack = false;
	public Vector3 endpoint;
	// Use this for initialization
	void Start ()
	{
	
	}

	public void spawn (GameObject owner, GameObject boomnana, Vector3 start,/* Vector3 direction,*/Vector3 end)
	{
		this.owner = owner;
		//this.thisObj = boomnana;
		transform.position = start;
		/* if (end != null)
        {
            endpoint = end;
        }*/
		/*else
        {
        endpoint = new Vector3(start.x + direction.x, start.y + direction.y, start.z + direction.z);
        }*/
		endpoint = end;
		movingBack = false; 
		//spawnTime = Time.time * 1000;
//		Vector2 v2 = new Vector2 (rigidbody.velocity.x, rigidbody.velocity.z);
//		lateralspeed = v2.magnitude;
	}

	// Update is called once per frame
	void Update ()
	{ 
		if (transform.position == endpoint) {
			movingBack = true;
		}
		if (movingBack) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
			transform.position = Vector3.MoveTowards (transform.position, owner.gameObject.transform.position, 15.0f);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, endpoint, 15.0f);	
		}
	}

	void OnTriggerEnter (Collider coll)
	{
		IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
		if (coll.gameObject.tag == "player") {
			while (entities.MoveNext()) {
//				if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
				if (entities.Current.GetType () == typeof (BoltEntity)) {
					BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
					// Create Event and use the be, if it is the one that is colliding.
					if (be.gameObject == coll.gameObject) { // Check for enemy, deal full damage
						// CHECKS IF IT HIT ITSELF
						if (coll.gameObject == owner && movingBack) {// STUN THE OWNER
							var evnt = BoomEvent.Create(Bolt.GlobalTargets.Everyone);
							GameObject go = GameObject.Find ("HUD");
							HUDScript hs = go.GetComponent<HUDScript> ();
							Debug.Log (hs);
							float damageDealt = Mathf.Floor(coll.gameObject.GetComponent<PlayerStats>().hp * 0.20f);
                            hs.announcementText.text = "";
							evnt.TargEnt = be;
							evnt.Damage = damageDealt;
                            Destroy(this.gameObject);
                            evnt.Send();
                            
						} else { // CHECK IF FRIENDLY OR FOE
							if (coll.gameObject.GetComponent<PlayerStats> ().teamNumber != owner.GetComponentInParent<PlayerStats> ().teamNumber) {
								// deal full damage!!!
								var evnt = BoomEvent.Create(Bolt.GlobalTargets.Everyone);
								GameObject go = GameObject.Find ("Canvas");
								HUDScript hs = go.GetComponentInChildren<HUDScript> ();
								float damageDealt = Mathf.Floor(coll.gameObject.GetComponent<PlayerStats>().hp * 0.60f);
                                hs.announcementText.text = "" + damageDealt;// 85% of target health//this.owner.GetComponent<PlayerStats> ().boomNanaDamage;
								evnt.TargEnt = be;
                                evnt.Damage = damageDealt;
                                evnt.Send();
								Destroy (this.gameObject);
							}
						}
						//  Debug.Log("BoltEntity.gameObject matches coll.gameObject");
                        if (!(coll.gameObject == owner && !movingBack)) { // Ensure that the collision when just thrown doesn't trigger the effect
                            GameObject scorch = (GameObject)Instantiate(Resources.Load("Prefabs/VFX_Scorch_Projector"));
                            scorch.transform.position = new Vector3(transform.position.x, 70, transform.position.z);
                            Destroy(scorch, 15f);
                        }
					}
				}
			}
		}
	}

}
