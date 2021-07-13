using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;


public static class Connection	 {

	static string ServerIP = "127.0.0.1";
	static int ServerPort = 9933;
	static string ZoneName = "Signup";
	static string UserName = "Admin";
	static string Password = "12345";
	
	static SmartFox sfs;
	static MasterController mc;

	//static int count;

	static List<Room> roomList;
	static int joinRoomCount;

	// Use this for initialization
	static public void Start () {
		//count = 0;
		//Debug.Log("estado: " + count);
		sfs = new SmartFox();
		sfs.ThreadSafeMode = true;
		
		sfs.AddEventListener (SFSEvent.CONNECTION, OnConnection);
		sfs.AddEventListener (SFSEvent.LOGIN, OnLogin);
		sfs.AddEventListener (SFSEvent.LOGIN_ERROR, OnLoginError);
		
		sfs.AddEventListener (SFSEvent.ROOM_JOIN, OnJoin);
		sfs.AddEventListener (SFSEvent.ROOM_JOIN_ERROR, OnJoinError);
		sfs.AddEventListener (SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
		sfs.AddEventListener (SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);

		sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtResponse);
				
		sfs.Connect (ServerIP, ServerPort);
	}
	static public void SetMasterController(MasterController mastercont) {
		mc = mastercont;
	}

	static void OnConnection (BaseEvent e) {
		if ((bool)e.Params["success"]) {
			Debug.Log ("Connected!");
			
			sfs.Send (new LoginRequest (UserName, Password, ZoneName));
		} else {
			Debug.Log ("Connection failed");
		}
	}
	
	static void OnLogin (BaseEvent e) {
		Debug.Log ("Logged in : " + e.Params["user"]);

		// Send Admin loged signal
		SendSignal(0);

		//Debug.Log(roomList.Count);
		Room r;
		joinRoomCount = 0;
		for (int n = 2; n < roomList.Count; n++) {
			r = sfs.GetRoomById(n);
			//Debug.Log("room: "+r.Name +", Users: "+r.UserCount + ", "+r.Capacity);
			joinRoomCount ++;
		}
		JoinRooms();
		
		//sfs.Send( new JoinRoomRequest("The Lobby") );
	}

	static void JoinRooms () {
		Room r = sfs.GetRoomById(roomList.Count-joinRoomCount);
		sfs.Send(new JoinRoomRequest(r.Name,"",-1,true));
		//Debug.Log("joiningRoom: "+r.Name);

	}
	
	static void OnLoginError (BaseEvent e) {
		Debug.Log ("Loggin error");
	}
	
	static void OnJoin (BaseEvent e) {
		Room r = (Room)e.Params["room"];
		//Debug.Log("join Room: "+r.Name);
		if(joinRoomCount > 0) {
			if(joinRoomCount==3)ChangeRoomStatus(r,false);
			joinRoomCount--;
			JoinRooms ();
		}
	}
	
	static void OnJoinError (BaseEvent e) {
	}

	static void OnUserEnterRoom (BaseEvent e) {

		Room room = (Room)e.Params["room"];
		User user = (User)e.Params["user"];

		if (room.UserCount == room.MaxUsers) {
			ChangeRoomStatus(room, true);
			int roomInd = roomList.IndexOf(room);
			Debug.Log ("room index: "+roomInd);
			SendSignal(1,roomInd);
		} else {
			ChangeRoomStatus(room, false);
		}
		Debug.Log("User: " + user.Name + " has just joined Room: " + room.Name);
	}

	static void OnExtResponse (BaseEvent ext ) {
		
		string cmd = (string)ext.Params["cmd"];
		ISFSObject objIn = (ISFSObject)ext.Params["params"];
		
		Debug.Log("Extension response: "+cmd);
		if ( cmd == "math" ) {
			Debug.Log ("Sum: " + objIn.GetInt ("sum"));
		}
		if ( cmd == "code" ) {
			int signal = objIn.GetInt ("signal");
			Debug.Log ("Señal: " + signal);
			switch (signal) {
			case 2: // Turn changed
				Debug.Log("turn changed : "+ objIn.GetInt ("turn"));
				break;
			}
		}
		
	}
	
	static void SendSignal(int signalNum, int roomIndex = -1) {

		ISFSObject resObj = SFSObject.NewInstance();

		switch (signalNum) {
		case 0: // Send Admin logued signal
			resObj.PutInt("signal",0);
			sfs.Send(new ExtensionRequest("code",resObj));
			break;

		case 1: //Room full, start game
			resObj.PutInt("signal",1);
			resObj.PutInt("roomIndex",roomIndex);
			sfs.Send(new ExtensionRequest("code",resObj));
			break;

		}
		
		roomList = sfs.GetRoomListFromGroup("default");
	}

	static void ChangeRoomStatus(Room r, bool full) {
		mc.RoomStatusChanged(r,full);
	}

	static void OnUserExitRoom (BaseEvent e) {
	}
	
	static public void ConUpdate() {
		sfs.ProcessEvents();
	}

	static public void AppQuit(){
	//static void OnApplicationQuit() {
		if(sfs.IsConnected){
			sfs.Disconnect();
		}
	}

	/*static public void test() {
		count ++;
		Debug.Log("mas: " + count);
	}*/
	
	// Update is called once per frame
	//static public void Update () {
	
	//}
}
