﻿using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{

	//Get Max HP from server, and give each player a divided hp
	// Use StatSplitter.cs

	//Stats
	public float maxHealth;
	float playerHp;
    public int teamNumber;
    public string playerName;
	float movementSpeed;
	//Damages 
	public float tailSlapDamage;
	public float boomNanaDamage;
	public float aoeTickDamageFactor;
	//Ranges
	public float boomnanaRange;
	public float jumpHeight;
	//Timers
	public float ccDuration;
	public float buffDuration;
	public float hp;
	public float aoeDuration;
	public float coconutEffectDuration;
	public float channeledTime;
	//AOE TICKTIME
	public float tickTime;
	//Resouces
	public int cprBananas; 
	//Cooldowns
	float globalCooldownCounter;
	public float tailSlapCooldown;
	public float boomNanaCooldown;
	public float cprCooldown;
	public float aoeCooldown;
	public float ccCooldown;
	public float buffCooldown;
	//BuffAttributes
	public float buffCostFactor = 0.05f;
	public float buffDamageFactor = 1.2f;
	//BuffedStuff
	public bool buffed = false;
	public bool trapBeeHiveBuffed = false;
	public bool trapAntNestBuffed = false;
	public bool IsInCoconutArea = false;
	public bool hasCoconutEffect = false;
	public bool stoppedInCoconutConsume = false;
	public bool canPickUpCoconut = true;
	//public bool canPickUpCoconut = true;
	//BASE/RESPAWN POS
	public Vector3 respawnPosition;


	// Use this for initialization
	void Start ()
	{
		try{ 
		GameObject go = GameObject.Find ("Canvas");
		HUDScript hs = go.GetComponentInChildren<HUDScript> ();
		hs.a1Time = tailSlapCooldown;
		hs.a2Time = boomNanaCooldown;
		hs.a3Time = aoeCooldown;
		hs.a4Time = ccCooldown;
		hs.a6Time = cprCooldown;
		} catch {
		}
	}

	public void makeTheStatChange ()
	{
		var evnt = StatStartEvent.Create(Bolt.GlobalTargets.Everyone);
		//IEnumerator enumer = BoltNetwork.entities.GetEnumerator();
		//while(enumer.MoveNext()){
		//    if(enumer.Current.GetType().IsInstanceOfType(new BoltEntity())){
		//        if(((BoltEntity)enumer.Current as BoltEntity).gameObject == this.gameObject){
		//            evnt.TargEnt = (BoltEntity)enumer.Current as BoltEntity;
		//        }
		//    }
        //}
        evnt.Send();
	}

	public IEnumerator getEntities ()
	{
		return BoltNetwork.entities.GetEnumerator ();
	}

	public void setSplitStats (float mhp, float boom, float tail, float aoe)
	{
		float currentHpFactor = hp / maxHealth;

		maxHealth = mhp;
		boomNanaDamage = boom;
		tailSlapDamage = tail;
		aoeTickDamageFactor = aoe;

		hp = maxHealth * currentHpFactor;
	}

	void updateHp ()
	{

	}

	void setMovementSpeed ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public float getHealth ()
	{
		return hp;
	}

	public float getMaxHealth ()
	{
		return maxHealth;
	}


}
