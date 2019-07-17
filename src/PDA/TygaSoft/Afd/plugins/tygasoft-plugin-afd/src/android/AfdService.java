package com.tygasoft.services;

import java.io.IOException;
import java.util.LinkedHashMap;
import java.util.Map;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;
import org.xmlpull.v1.XmlPullParserException;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.tygasoft.model.ResResultInfo;

public class AfdService implements IAfd{
	private static final String NameSpace = "http://TygaSoft.Services.PdaService";
    //private static final String Url = "http://120.26.198.137/Afd/Services/PdaService.svc";
	private static final String Url = "http://www.tygaweb.com/Afd/Services/PdaService.svc";
    private static final String SoapAction = NameSpace + "/IPda/";
    
	public String GetHelloWord() {
		return GetResult("GetHelloWord",null);
    }
	
	public String ValidateUser(String username,String password)
	{
		LinkedHashMap<String, Object> items = new LinkedHashMap<String, Object>();
		items.put("username", username);
		items.put("password", password);

		return GetResult("ValidateUser",items);
	}
	
	public String GetOrderProcessInfo(String Id)
	{
		LinkedHashMap<String, Object> items = new LinkedHashMap<String, Object>();
		items.put("Id", Id);

		return GetResult("GetOrderProcessInfo",items);
	}

	public String SaveOrderScan(String jsonField) {
		Gson gson = new Gson();
		JsonObject jsonObj = gson.fromJson(jsonField, JsonObject.class);
		LinkedHashMap<String, Object> items = new LinkedHashMap<String, Object>();
		items.put("orderCode", jsonObj.get("OrderCode").getAsString());
		items.put("customerCode", jsonObj.get("CustomerCode").getAsString());
		items.put("orderStep", jsonObj.get("OrderStep").getAsString());
		items.put("loginId", jsonObj.get("LoginId").getAsString());
		items.put("deviceId", jsonObj.get("DeviceId").getAsString());
		items.put("latlng", jsonObj.get("Latlng").getAsString());

		return GetResult("SaveOrderScan",items);
    }
	
	private String GetResult(String method,LinkedHashMap<String, Object> items)
	{
		SoapObject soapObject = new SoapObject(NameSpace, method);
		if(items != null && items.size()>0){
			for (Map.Entry<String, Object> entry : items.entrySet()) {
				soapObject.addProperty(entry.getKey(), entry.getValue().toString());
			}
		}
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
        envelope.bodyOut = soapObject;
        envelope.dotNet = true;
        envelope.setOutputSoapObject(soapObject);
        
        HttpTransportSE trans = new HttpTransportSE(Url);
        trans.debug = false;
        SoapObject result = null;
        String resText = "";
        try {
            trans.call(SoapAction+method, envelope);
            result = (SoapObject)envelope.bodyIn;
            System.out.println("Call Successful!");
        } catch (IOException e) {
        	resText = e.getMessage();
            System.out.println("IOException");
            e.printStackTrace();
        } catch (XmlPullParserException e) {
        	resText = e.getMessage();
            System.out.println("XmlPullParserException");
            e.printStackTrace();
        }
        
        if(!resText.equals("")) {
        	Gson gson = new Gson();
        	return gson.toJson(new ResResultInfo(1001,resText,null));
        }
        return result.getProperty(0).toString();
	}  
}
