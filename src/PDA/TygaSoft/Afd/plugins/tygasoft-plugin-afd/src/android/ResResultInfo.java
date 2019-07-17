package com.tygasoft.model;

public class ResResultInfo {
	public ResResultInfo(int resCode,String msg,Object data){
		this.ResCode = resCode;
		this.Msg = msg;
		this.Data = data;
	}
    public int ResCode;
    public String Msg;
    public Object Data;
}
