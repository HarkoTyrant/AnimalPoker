package com.animalgaming.sfs2x;

import com.smartfoxserver.v2.extensions.SFSExtension;
import com.smartfoxserver.v2.components.signup.SignUpAssistantComponent;

public class SignupExtension extends SFSExtension {
	
	private SignUpAssistantComponent suac;
	
	@Override
	public void init() {
		
		suac = new SignUpAssistantComponent();
		addRequestHandler(SignUpAssistantComponent.COMMAND_PREFIX, suac);
		
	}
	
	@Override
	public void destroy() {
		super.destroy();
	}

}
