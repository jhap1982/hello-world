package com.jhap1982.helloworld;

import org.junit.jupiter.api.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * MainApp unit test class.
 * 
 * @author jhap1982
 */
public class MainAppUnitTest {

	private static final Logger logger = LoggerFactory.getLogger(MainAppUnitTest.class);
	
	@Test
	public void testMainAppUnitTest() {
		String[] args = {};
		
		MainApp.main(args);
		
		logger.debug("Main test passed");
	}

}