package com.tygasoft.services;

public interface IAfd {
	
	String GetHelloWord();
	
	String ValidateUser(String username, String password);
	
	String GetOrderProcessInfo(String Id);

	String SaveOrderScan(String jsonField);
}
