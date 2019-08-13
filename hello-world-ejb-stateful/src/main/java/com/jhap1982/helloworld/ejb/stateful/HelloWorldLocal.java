package com.jhap1982.helloworld.ejb.stateful;

import javax.ejb.Local;

/**
 * Local interface of Template EJB.
 * 
 * @author jhap1982
 */
@Local
public interface HelloWorldLocal {

	public void increment();
	
	public int getCounter();

}