package com.jhap1982.helloworld.ejb.stateful;

import javax.ejb.Remote;

/**
 * Remote interface of Hello World EJB.
 * 
 * @author jhap1982
 */
@Remote
public interface HelloWorldRemote {

	public String helloWorld(String input);
	
}