package com.tygasoft.plugins;

import org.apache.cordova.CallbackContext;
import org.apache.cordova.CordovaPlugin;
import org.json.JSONArray;
import org.json.JSONException;

import com.tygasoft.services.AfdService;

public class AfdPlugin extends CordovaPlugin {
	
	@Override
    public boolean execute(String action, JSONArray args, final CallbackContext callbackContext) throws JSONException {
		if (action.equals("GetHelloWord")) {
            callbackContext.success(args.getString(0));
            return true;
        }
		else if (action.equals("ValidateUser")) {
			String username = args.getString(0);
			String password = args.getString(1);
			AfdService client = new AfdService();
			callbackContext.success(client.ValidateUser(username,password));
            return true;
        }
		else if (action.equals("GetOrderProcessInfo")) {
			String Id = args.getString(0);
			AfdService client = new AfdService();
			callbackContext.success(client.GetOrderProcessInfo(Id));
            return true;
        }
		else if (action.equals("SaveOrderScan")) {
			String jsonField = args.getString(0);
			AfdService client = new AfdService();
			callbackContext.success(client.SaveOrderScan(jsonField));
            return true;
        }
        
        return false;
    }
}
