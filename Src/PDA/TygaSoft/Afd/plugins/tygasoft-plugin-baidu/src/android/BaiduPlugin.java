package com.tygasoft.plugins;

import org.apache.cordova.CallbackContext;
import org.apache.cordova.CordovaPlugin;
import org.apache.cordova.PluginResult;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;

import com.baidu.location.BDLocation;
import com.baidu.location.BDLocationListener;
import com.baidu.location.LocationClient;
import com.baidu.location.LocationClientOption;
import com.baidu.location.LocationClientOption.LocationMode;

public class BaiduPlugin extends CordovaPlugin {
	public CallbackContext mContext = null;
	public LocationClient mLocationClient = null;

	@Override
    public boolean execute(String action, JSONArray args, final CallbackContext callbackContext) throws JSONException {
		mContext = callbackContext;
		if (action.equals("GetHelloWord")) {
			mContext.success(args.getString(0));
            return true;
        }
		else if(action.equals("GetCurrentPosition")){
			SetMyPluginResult(PluginResult.Status.NO_RESULT,null,"");
			
			if(mLocationClient == null){
			    mLocationClient = new LocationClient(GetMyActivity()); 
				mLocationClient.registerLocationListener(myListener);
				InitLocation();
			}
			mLocationClient.start();
			return true;
		}
		else if(action.equals("Stop")){
			mLocationClient.stop();
			return true;
		}
		return false;
	}
	
	private Activity GetMyActivity() {
    	return this.cordova.getActivity();
    }
	
	private void SetMyPluginResult(org.apache.cordova.PluginResult.Status status,JSONObject data,String msg)
	{
		PluginResult pluginResult = null;
		if(data == null) pluginResult = new PluginResult(status);
		else {
			if(!msg.equals("")) pluginResult = new PluginResult(status,msg);
			else pluginResult = new PluginResult(status,data);
		}
        pluginResult.setKeepCallback(true);
        mContext.sendPluginResult(pluginResult);
	}
	
	private void InitLocation(){
	    LocationClientOption option = new LocationClientOption();
	    option.setLocationMode(LocationMode.Hight_Accuracy);
	    option.setCoorType("bd09ll");
	    option.setScanSpan(1000);
	    option.setIsNeedAddress(false);
	    option.setOpenGps(true);
	    option.setLocationNotify(true);
	    option.setIsNeedLocationDescribe(false);
	    option.setIsNeedLocationPoiList(false);
	    option.setIgnoreKillProcess(false);
	    option.SetIgnoreCacheException(false);
	    option.setEnableSimulateGps(false);
	 
	    mLocationClient.setLocOption(option);
	}
	
	public BDLocationListener myListener = new BDLocationListener() {
		@Override
	    public void onReceiveLocation(BDLocation location) {
	 
			JSONObject json = new JSONObject();
			try {
				json.put("Latitude", location.getLatitude());
				json.put("Longitude", location.getLongitude());
				json.put("LocType", location.getLocType());
				
				SetMyPluginResult(PluginResult.Status.OK, json,"");
			} catch (JSONException e) {
				SetMyPluginResult(PluginResult.Status.ERROR, json,e.getMessage());
			}
			finally{
				mLocationClient.stop();
			}
	    }
		
		@Override
        public void onConnectHotSpotMessage(String arg0, int arg1) {
            // TODO Auto-generated method stub
        }
	};
}
