using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//using Sfs2X;
//using Sfs2X.Core;
//using Sfs2X.Requests;
using Sfs2X.Entities;

public class MasterController : MonoBehaviour {

	PokerGame pkG;

	public Text RoomStatusText;

	// Use this for initialization
	void Start () {
		
		Connection.Start();
		RoomStatusText.text = "---O-O---\n----#----";
		Connection.SetMasterController (this);
	}
	
	void FixedUpdate() {
		//sfs.ProcessEvents();
		Connection.ConUpdate();
	}

	void OnApplicationQuit() {
		/*if(sfs.IsConnected){
			sfs.Disconnect();
		}*/
		Connection.AppQuit();
	}

	public void RoomStatusChanged(Room r, bool full) {
		if (!full) {
			RoomStatusText.text = r.Name + ": " + (r.UserCount) + "/" + r.MaxUsers + "\nWaiting...";
		} else {
			RoomStatusText.text = r.Name + ": " + (r.UserCount) + "/" + r.MaxUsers + "\nPlaying";
		}
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/
}
