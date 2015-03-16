﻿using UnityEngine;
using System.Collections;
using System;
using UdpKit;

public class BoltInit : MonoBehaviour
{
		public static bool hasPickedTeamOne = false;
		public static bool hasPickedTeamTwo = false;
		bool isServer;
		bool isClient;

		enum State
		{
				SelectMode,
				SelectTeam,
				SelectMap,
				EnterServerIp,
				StartServer,
				StartClient,
				Started,
		}

		State state;

		string map;
		string serverAddress = ""; // = "127.0.0.1";//"169.254.81.104";//"127.0.0.1";//"169.254.185.152";

		int serverPort = 27000;
     
		void Awake ()
		{
				serverPort = BoltRuntimeSettings.instance.debugStartPort;
		}

		void OnGUI ()
		{
				Rect tex = new Rect (10, 10, 140, 75);
				Rect area = new Rect (10, 90, Screen.width - 20, Screen.height - 100);

				GUI.Box (tex, Resources.Load ("BoltLogo") as Texture2D);
				GUILayout.BeginArea (area);

				switch (state) {
				case State.SelectMode:
						State_SelectMode ();
						break;
				case State.SelectMap:
						State_SelectMap ();
						break;
				case State.EnterServerIp:
						State_EnterServerIp ();
						break;
				case State.SelectTeam:
						State_SelectTeam ();
						break;
				case State.StartClient:
						State_StartClient ();
						break;
				case State.StartServer:
						State_StartServer ();
						break;
				}

				GUILayout.EndArea ();
		}

		private void State_EnterServerIp ()
		{
				GUILayout.BeginHorizontal ();

				GUILayout.Label ("Server IP: ");
				serverAddress = GUILayout.TextField (serverAddress);

				if (GUILayout.Button ("Connect")) {
						state = State.SelectTeam;
						//state = State.StartClient;
				}

				GUILayout.EndHorizontal ();
		}

		void State_SelectTeam ()
		{
				if (ExpandButton ("Team one")) {

						hasPickedTeamOne = true;
						if (isClient == true) {
								state = State.StartClient;
							
						} else if (isServer == true) {
								state = State.StartServer;
								
						}
				}
				if (ExpandButton ("Team two")) {
					
						hasPickedTeamTwo = true;
						if (isClient == true) {
								state = State.StartClient;
							
						} else if (isServer == true) {
								state = State.StartServer;
						
						}
				}
		}

		void State_SelectMode ()
		{
				if (ExpandButton ("Server")) {
						isServer = true;
						state = State.SelectMap;
				}
				if (ExpandButton ("Client")) {
						isClient = true;
						state = State.EnterServerIp;
				}
		}

		void State_SelectMap ()
		{
				foreach (string value in BoltScenes.AllScenes) {
						if (Application.loadedLevelName != value) {
								if (ExpandButton (value)) {
										map = value;
										state = State.SelectTeam;
										//state = State.StartServer;
								}
						}
				}
		}

		void State_StartServer ()
		{
				BoltLauncher.StartServer (new UdpEndPoint (UdpIPv4Address.Any, (ushort)serverPort));
				BoltNetwork.LoadScene (map);
				state = State.Started;
		}

		void State_StartClient ()
		{
				BoltLauncher.StartClient (UdpEndPoint.Any);
				BoltNetwork.Connect (new UdpEndPoint (UdpIPv4Address.Parse (serverAddress), (ushort)serverPort));
				state = State.Started;
		}

		bool ExpandButton (string text)
		{
				return GUILayout.Button (text, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true));
		}
	
}
