package com.jhap1982.helloworld.ejb.stateful;

import javax.ejb.Local;

/**
 * Local interface of Hello World EJB.
 * 
 * @author jhap1982
 */
@Local
public interface HelloWorldLocal {

	public String helloWorld(String input);
	
}