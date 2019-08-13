package com.jhap1982.helloworld.ejb.stateful;

import javax.ejb.Stateful;

/**
 * Hello world stateful EJB implementation.
 * 
 * @author jhap1982
 */
@Stateful
public class HelloWorldEJBStateful implements HelloWorldLocal, HelloWorldRemote {

	@Override
	public String helloWorld(String input) {
		return input;
	}	

}