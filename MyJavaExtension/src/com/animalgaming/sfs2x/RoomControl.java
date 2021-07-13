package com.animalgaming.sfs2x;

//import java.awt.List;
import java.util.List;

//import com.smartfoxserver.v2.entities.variables.*;

import com.smartfoxserver.v2.entities.*;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;
import com.smartfoxserver.v2.annotations.*;

@Instantiation(Instantiation.InstantiationMode.SINGLE_INSTANCE)
public class RoomControl extends BaseClientRequestHandler {
	
	User adminUser;
	List<Room> roomList;
	
	List<User> playerList;
	List<User> userList;
	int turn = 0;
	
	@Override
	public void handleClientRequest(User player, ISFSObject params) {
		// TODO Auto-generated method stub
		
		int cmd = params.getInt("signal");
		ISFSObject rtn;
		Room r;
		int ind;
		
		switch (cmd) {
		case 0:	//admin user
			System.out.println("name: "+player.getName()+".");
			if (player.getName().equals("Admin")) {
				
				System.out.println("dentro");
				adminUser = player;
				Zone z = adminUser.getZone();
				roomList = z.getRoomList();
				System.out.println("zName:"+z.getName()+"rlistsize: " +roomList.size());
				
				rtn = new SFSObject();
				rtn.putInt("signal", 0);
				SendGameStart(player, rtn);
			}
			break;
		case 1: //Room full, game start
			ind = params.getInt("roomIndex");
			r = roomList.get(ind);
			System.out.println("Sending game roomindex " + ind);
			System.out.println("Sending game start to room " + r.getName());
			playerList =  r.getPlayersList();
			userList = r.getUserList();
			System.out.println("Room " + r.getName() + " userCount: " + playerList.size());
			
			rtn = new SFSObject();
			rtn.putInt("signal", 1);
			turn = 1;
			rtn.putInt("turn", turn);
			SendGameStart(playerList, rtn);
			break;
		case 2: //Change turn
			ChangeTurn();
			
			rtn = new SFSObject();
			rtn.putInt("signal", 2);
			rtn.putInt("turn", turn);
			SendGameStart(userList, rtn);
			break;
		case 3:
			rtn = new SFSObject();
			rtn.putInt("signal", 3);
			SendGameStart(playerList, rtn);
			break;
		case 4:
			rtn = new SFSObject();
			rtn.putInt("signal", 4);
			SendGameStart(playerList, rtn);
			break;
		case 5:
			rtn = new SFSObject();
			rtn.putInt("signal", 5);
			SendGameStart(playerList, rtn);
			break;
		case 6:
			rtn = new SFSObject();
			rtn.putInt("signal", 6);
			SendGameStart(playerList, rtn);
			break;
		}
	}
	
	public void SendGameStart(User player ,ISFSObject params) {
		
		MyExtension parentEx = (MyExtension)getParentExtension();
		parentEx.send("code", params, player);
		
	}

	public void SendGameStart(List<User> player ,ISFSObject params) {
		
		MyExtension parentEx = (MyExtension)getParentExtension();
		parentEx.send("code", params, player);
		
	}
	
	private void ChangeTurn() {
		if (turn == 1) {
			turn = 2;
		} else {
			turn = 1;
		}
	}

}
