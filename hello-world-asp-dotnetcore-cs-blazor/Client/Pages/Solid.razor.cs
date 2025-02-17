namespace hello_world_asp_dotnetcore_cs_blazor.Client.Pages
{
	/*
		<li><b>S</b> - Single Responsibility Principle (SRP)</li>
		<li><b>O</b> - Open/Closed Principle(OCP)</li>
		<li><b>L</b> - Liskov Substitution Principle(LSP)</li>
		<li><b>I</b> - Interface Segregation Principle(ISP)</li>
		<li><b>D</b> - Dependency Inversion Principle(DIP)</li>
	*/
	
	// SRP - Single Responsibility Principle
	// Una clase debe tener una única razón para cambiar.
	public class ReportGenerator
	{
		public string GenerateReport() => "Generando Reporte";
	}

	// OCP - Open/Closed Principle
	// Una clase debe estar abierta a la extensión pero cerrada a la modificación.
	public abstract class Shape
	{
		public abstract double Area();
	}

	public class Circle : Shape
	{
		public double Radius { get; set; }
		public override double Area() => Math.PI * Radius * Radius;
	}

	// LSP - Liskov Substitution Principle
	// Los objetos de una clase derivada deben poder reemplazar objetos de su clase base sin afectar la funcionalidad.
	public class Bird
	{
		public virtual string Fly() => "Puedo volar";
	}

	public class Penguin : Bird
	{
		public override string Fly() => "No puedo volar";
	}

	// ISP - Interface Segregation Principle
	// Una interfaz debe tener solo los métodos necesarios para la funcionalidad específica de la clase que la implementa.
	public interface IWorker
	{
		void Work();
	}

	public interface IEater
	{
		void Eat();
	}

	public class Human : IWorker, IEater
	{
		public void Work() => Console.WriteLine("Trabajando");
		public void Eat() => Console.WriteLine("Comiendo");
	}

	// DIP - Dependency Inversion Principle
	// Las clases de alto nivel no deben depender de clases de bajo nivel, sino de abstracciones.
	public interface ILogger
	{
		void Log(string message);
	}

	public class ConsoleLogger : ILogger
	{
		public void Log(string message) => Console.WriteLine(message);
	}

	public class Service
	{
		private readonly ILogger _logger;
		public Service(ILogger logger)
		{
			_logger = logger;
		}
		public void Execute() => _logger.Log("Ejecutando servicio");
	}
}
