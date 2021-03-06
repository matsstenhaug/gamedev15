﻿using UnityEngine;
using System.Collections;

public class CCScript : MonoBehaviour
{

	//Get Input KeyCode
	//Get stun Duration from player
	//Check if the colliding actor is a player, and stun them
	//set Available to false;
	//Update check timer for cooldown.

	bool available = true;
	float lastUsed;
	PlayerStats ps;
	TestPlayerBehaviour tpb;
	public bool CCUsedInHidingGrass;
	bool inUse = false;

	// Use this for initialization
	void Start ()
	{
		ps = this.gameObject.GetComponentInParent<PlayerStats> ();
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
	}

	// Update is called once per frame
	void Update ()
	{

		ps = gameObject.GetComponentInParent<PlayerStats> ();
		tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
//		if (ps.teamNumber == 1) {
//			if (AntNest.playerOneIsBuffed == false) {
//				if (ps.trapAntNestBuffed == true) {
//					float ccDurationBuffed = ps.ccDuration / AntNest.playerBuffCcDuration;
//					ps.ccDuration = ccDurationBuffed;
//					ps.trapAntNestBuffed = false;
//				}
//			}
//		}
//		if (ps.teamNumber == 2) {
//			if (AntNest.playerTwoIsBuffed == false) {
//				if (ps.trapAntNestBuffed == true) {
//					float ccDurationBuffed = ps.ccDuration / AntNest.playerBuffCcDuration;
//					ps.ccDuration = ccDurationBuffed;
//					ps.trapAntNestBuffed = false;
//				}
//			}
//		}

		if (Time.time - lastUsed >= ps.ccCooldown) {
			available = true;
		}
		if (Input.GetKeyDown (tpb.ccKey) && available && !GetComponentInParent<StateController>().isDead && !GetComponentInParent<StateController>().isChanneling) {
			inUse = true;
			var evnt = CCAnimEvent.Create(Bolt.GlobalTargets.Everyone);
            evnt.TargEnt = GetComponentInParent<TestPlayerBehaviour>().entity;
            evnt.Send();
//			GetComponentInParent<TestPlayerBehaviour> ().gameObject.transform.FindChild ("fish_forFS_withanimation").gameObject.GetComponent<FishAnimScript> ()
//			.playAnimation ();
//			GetComponentInParent<TestPlayerBehaviour> ().animation.Play ("M_FS");
		}


	}

	void OnTriggerStay (Collider coll)
	{
		//Debug.Log(coll.tag);
		if (coll.gameObject.tag == "grass") {
			if (Input.GetKeyDown (tpb.ccKey) && available) {
				CCUsedInHidingGrass = true;
				Debug.Log (CCUsedInHidingGrass.ToString ());
			}
		}
		if (inUse) {
			available = false;
			lastUsed = Time.time;
			IEnumerator entities = BoltNetwork.entities.GetEnumerator ();
			if (coll.gameObject.tag == "player") {
				TestPlayerBehaviour tt = this.gameObject.GetComponentInParent<TestPlayerBehaviour> ();
				GameObject player = tt.gameObject;

				while (entities.MoveNext()) {
					if (entities.Current.GetType ().IsInstanceOfType (new BoltEntity ())) {
						BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
						// Create Event and use the be, if it is the one that is colliding.
						//if (be.gameObject != this.gameObject.GetComponentInParent<TestPlayerBehaviour>().player.gameObject)
						//{
						if (be.gameObject == coll.gameObject && !be.isOwner) { // Check for enemy, deal full damage


							if (coll.gameObject.GetComponent<PlayerStats> ().teamNumber != ps.teamNumber && coll.gameObject != player) {
								// deal full damage!!!
								var evnt = CCEvent.Create(Bolt.GlobalTargets.Everyone);
								GameObject go = GameObject.Find ("Canvas");
								HUDScript hs = go.GetComponentInChildren<HUDScript> ();

                                hs.announcementText.text = "Stunning!";
								Debug.Log ("TeamNumber: " + be.gameObject.GetComponent<PlayerStats> ().teamNumber + ", ps.teamNumber = " + ps.teamNumber);
								evnt.TargEnt = be;//coll.gameObject.GetComponent<TestPlayerBehaviour> ().entity;
								evnt.Duration = ps.ccDuration;
                                Debug.Log(ps.ccDuration + " = Duration");
                                evnt.Send();

								//gameObject.GetComponentInParent<StateController>().stun(this.gameObject, 0);
							}
							if (coll.gameObject == player) {
								Debug.Log ("Casting player found as Collider");
							}
							//else
							//{ // check for friendly player, deal 50% dmg.
							//    // deal half damage!!!
							//    using (var evnt = CCEvent.Create(Bolt.GlobalTargets.Everyone))
							//    {
							//        evnt.TargEnt = be;
							//        evnt.Duration = this.gameObject.GetComponentInParent<PlayerStats>().ccDuration / 2;
							//    }
							//}


						}
					}

					//}
				}
			}
			inUse = false;
		}





		// Debug.Log(coll.tag);
		//if (coll.gameObject.tag == "player")
		//{
		//    Debug.Log("Found player");
		//    tpb = this.gameObject.GetComponentInParent<TestPlayerBehaviour>();
		//    if (Input.GetKeyDown(tpb.ccKey) && available)
		//    {
		//        Debug.Log("CC BEING CAST");
		//        IEnumerator entities = BoltNetwork.entities.GetEnumerator();



		//        while (entities.MoveNext())
		//        {
		//            if (entities.Current.GetType().IsInstanceOfType(new BoltEntity()))
		//            {
		//                BoltEntity be = (BoltEntity)entities.Current as BoltEntity;
		//                // Create Event and use the be, if it is the one that is colliding.
		//                if (be.gameObject == coll.gameObject)
		//                { // Check for enemy, deal full damage
		//                    if (coll.gameObject.GetComponent<PlayerStats>().teamNumber != this.gameObject.GetComponentInParent<PlayerStats>().teamNumber)
		//                    {
		//                        // deal full damage!!!
		//                        using (var evnt = CCEvent.Create(Bolt.GlobalTargets.Everyone))
		//                        {
		//                            evnt.TargEnt = be;
		//                            evnt.Duration = this.gameObject.GetComponentInParent<PlayerStats>().ccDuration;
		//                            Debug.Log(this.gameObject.GetComponentInParent<PlayerStats>().ccDuration + " = Duration");
		//                        }
		//                    }
		//                    else
		//                    { // check for friendly player, deal 50% dmg.
		//                        // deal half damage!!!
		//                        using (var evnt = CCEvent.Create(Bolt.GlobalTargets.Everyone))
		//                        {
		//                            evnt.TargEnt = be;
		//                            evnt.Duration = this.gameObject.GetComponentInParent<PlayerStats>().ccDuration / 2;
		//                        }
		//                    }
		//                }
		//            }
		//        }
		//    }
		//    available = false;
		//    lastUsed = Time.time;
		//}
	}
}

