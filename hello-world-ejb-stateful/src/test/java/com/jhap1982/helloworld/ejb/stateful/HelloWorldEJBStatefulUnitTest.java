package com.jhap1982.helloworld.ejb.stateful;

import org.junit.Assert;
import org.junit.jupiter.api.Test;

/**
 * HelloWorldEJBStateful unit test class.
 * 
 * @author jhap1982
 */
public class HelloWorldEJBStatefulUnitTest {

	
	@Test
	public void testHelloWorldEJBStatefulHelloWorld() {
		HelloWorldEJBStateful helloWorldEJBStateful = new HelloWorldEJBStateful();
		
		String input = "Hello world!";
		
		Assert.assertEquals(input, helloWorldEJBStateful.helloWorld(input));
	}

}