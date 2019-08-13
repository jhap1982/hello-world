package com.jhap1982.helloworld.ejb.stateful;

import javax.ejb.Stateful;

/**
 * Hello world stateful EJB implementation.
 * 
 * @author jhap1982
 */
@Stateful
public class HelloWorldEJBStateful implements HelloWorldLocal, HelloWorldRemote {

	private int counter;
	
	@Override
	public void increment() {
		counter++;
	}
	
	@Override
	public int getCounter() {
		return counter;
	}

}