package com.jhap1982.helloworld.ejb.stateful;

import javax.ejb.Remote;

/**
 * Remote interface of Template EJB.
 * 
 * @author jhap1982
 */
@Remote
public interface HelloWorldRemote {

	public void increment();
	
	public int getCounter();
	
}