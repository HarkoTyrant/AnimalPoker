package com.animalgaming.sfs2x;

//import com.smartfoxserver.v2.entities.SFSRoomEvents;
import com.smartfoxserver.v2.extensions.SFSExtension;
//import com.smartfoxserver.v2.components.login.LoginAssistantComponent;
import com.smartfoxserver.v2.components.signup.SignUpAssistantComponent;
//import com.smartfoxserver.v2.core.SFSEventType;

public class MyExtension extends SFSExtension {
	
	//private LoginAssistantComponent lac;
	private SignUpAssistantComponent suac;
	
	@Override
	public void init() {
		
		/*lac = new LoginAssistantComponent(this);
		
		lac.getConfig().loginTable = "users";
		lac.getConfig().userNameField = "username";
		lac.getConfig().passwordField = "password";*/
		
		suac = new SignUpAssistantComponent();
		this.addRequestHandler(SignUpAssistantComponent.COMMAND_PREFIX, suac);

		this.addRequestHandler("math", MathHandler.class);
		
		//this.addRequestHandler("getUsers", GetUserRequest.class);
		
		this.addRequestHandler("code", RoomControl.class);
		//this.addEventHandler(SFSEventType.USER_JOIN_ROOM, algo );
		
	}

	@Override
	public void destroy() {
		super.destroy();
	}

}
