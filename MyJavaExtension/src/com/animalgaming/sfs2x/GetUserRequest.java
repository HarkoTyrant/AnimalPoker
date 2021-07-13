package com.animalgaming.sfs2x;

import java.sql.SQLException;

import com.smartfoxserver.v2.db.IDBManager;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSArray;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;
import com.smartfoxserver.v2.extensions.ExtensionLogLevel;

public class GetUserRequest extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User player, ISFSObject params) {
		IDBManager dbManager = getParentExtension().getParentZone().getDBManager();
		String sql = "SELECT * FROM users";
		
		try {
			// Obtain a resultset
			ISFSArray res = dbManager.executeQuery(sql, new Object[] {});
			
			// Populate the response parameters
			ISFSObject response = new SFSObject();
			response.putSFSArray("users", res);
			
			// Send back to requester
			MyExtension parentEx = (MyExtension)getParentExtension();
			parentEx.send("getusers", response, player);
			
		} catch (SQLException e) {
			trace(ExtensionLogLevel.WARN, "SQL Failed: " + e.toString());
		}
	}

}
