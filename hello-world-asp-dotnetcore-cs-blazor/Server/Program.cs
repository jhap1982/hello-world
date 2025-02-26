

using hello_world_asp_dotnetcore_cs_blazor.Client.Common;


/**
 * Tipos de Blazor / Arquitectura:
 * 1. Blazor Server: La lógica de la aplicación se ejecuta en el servidor. Los cambios en la interfaz de usuario se comunican a través de una conexión SignalR.
*  2. Blazor WebAssembly: La lógica de la aplicación se ejecuta en el navegador del cliente mediante WebAssembly. No se necesita conexión constante con el servidor.
*  3. Blazor Hybrid: Combina Blazor con otras tecnologías (ej. MAUI) para aplicaciones multiplataforma.
*  4. Blazor Auto: Arquitectura adaptativa que optimiza entre Server y WebAssembly según el entorno.
*  
*  Componentes:
*  - Blazor se basa en componentes reutilizables que encapsulan UI y lógica.
*  - Los componentes se pueden anidar y reutilizar, y están escritos en Razor (una combinación de C# y HTML).
*  
*  Comunicacion:
*  - Llamadas a servicios web y APIs mediante HTTP.
*  - Uso de HttpClient para gestionar solicitudes HTTP.
*  
*  Data Binding:
*  - Permite la sincronización automática de datos entre el modelo y la vista.
*  - Soporta binding unidireccional y bidireccional.
*  
*  Servicios e Inyección de Dependencias (DI):
*  - Blazor utiliza DI para la gestión de servicios, similar a ASP.NET Core.
*  - Los servicios se pueden registrar y consumir en los componentes.
*  
*  Routing:
*  - Blazor tiene un sistema de enrutamiento que permite navegar entre componentes.
*  - Soporta rutas definidas en el propio componente o centralmente en el archivo App.razor.
*  
*  Interoperabilidad con JavaScript:
*  - Aunque Blazor permite desarrollar con C#, también se puede interoperar con librerías y código JavaScript cuando sea necesario.
*  @inject IJSRuntime JSRuntime
*  
*  Estado y Ciclo de Vida:
*  
*  Estado: Blazor maneja el estado del componente y el usuario. Los cambios en el estado desencadenan una actualización de la interfaz de usuario. 
*  Cuando un componente cambia su estado interno, se llama al método StateHasChanged para notificar al framework Blazor que debe volver a renderizar el componente. 
*  En la mayoría de los casos, Blazor lo hace automáticamente, pero en algunos casos, puede ser necesario llamarlo manualmente. Además, 
*  Blazor permite almacenar el estado en servicios, especialmente útiles para compartir datos entre componentes.
*       Persistencia del Estado: En Blazor Server, es común necesitar persistir el estado entre conexiones SignalR. 
*       Esto se puede hacer utilizando almacenamiento temporal o persistente, como bases de datos o almacenamiento en sesión.
*       
*  Métodos del Ciclo de Vida: Blazor proporciona una serie de métodos de ciclo de vida que permiten a los desarrolladores ejecutar 
*  lógica en diferentes momentos del ciclo de vida de un componente:
*  
*  - SetParametersAsync: Este método se llama primero y permite manejar parámetros asíncronamente. 
*                        Puede anularse para aplicar lógica personalizada antes de que los parámetros se establezcan.
*                        
*  - OnInitialized / OnInitializedAsync: Se llama una vez que el componente se inicializa. Es un buen lugar para colocar la lógica de inicialización que no depende de parámetros. 
*                                        OnInitializedAsync es la versión asincrónica y permite ejecutar código asíncrono durante la inicialización.
*
*  - OnParametersSet / OnParametersSetAsync: Se llama cada vez que se establecen parámetros para el componente. 
*                                            Esto puede ocurrir múltiples veces durante la vida del componente, por ejemplo, cuando el usuario navega a una 
*                                            nueva URL que pasa diferentes parámetros. OnParametersSetAsync permite manejar lógica asíncrona.
*                                            
* - OnAfterRender / OnAfterRenderAsync: Se llama después de que Blazor ha renderizado el componente. Es útil para lógica que necesita acceder al DOM, 
*                                       como la configuración de bibliotecas de JavaScript. OnAfterRenderAsync permite ejecutar lógica asíncrona después del renderizado. 
*                                       La propiedad firstRender permite distinguir entre el primer renderizado y los renderizados subsiguientes.
*                                       
* - ShouldRender: Este método se puede sobrescribir para controlar si el componente debe renderizarse. 
*                 Retorna un valor booleano indicando si se debe proceder con la renderización. Útil para optimizar el rendimiento.
*                 
* - Dispose: Se llama cuando el componente se elimina del DOM. Es el lugar para liberar recursos, cancelar operaciones pendientes, o limpiar el estado.
* 
* Ciclo: 
*	[Constructor] → 
*	SetParametersAsync → 
*	OnInitialized/OnInitializedAsync → 
*	OnParametersSet/OnParametersSetAsync → 
*	ShouldRender → 
*	OnAfterRender/OnAfterRenderAsync → 
*	Dispose
* 
* - State Management
* El manejo del estado en Blazor es esencial para mantener y compartir datos de forma eficiente. Aquí tienes los diferentes tipos:
* 
*	- State Management Local: 
*		Componente Local: El estado se maneja dentro de un solo componente.
*			Ventajas: Sencillez y rapidez de implementación.
*			Desventajas: Dificultad para compartir estado entre componentes.
*	- State Management en Componentes Padres e Hijos:
*		Componente Padre: El estado se maneja en el componente padre y se pasa a los hijos mediante parámetros.
*			Ventajas: Compartición de estado entre componentes relacionados.
*			Desventajas: Puede volverse complejo con componentes profundamente anidados.
*	- State Management con Inyección de Dependencias:
*		Servicio de Estado: Utilización de servicios singleton para manejar el estado que se inyecta en los componentes.
*			Ventajas: Estado compartido y persistente a lo largo de la aplicación.
*			Desventajas: Necesidad de configuración adicional y gestión de dependencias.
*	- State Management con Contenedores de Estado (State Containers):
*		StateContainer: Clase que contiene el estado y métodos para actualizarlo, inyectada en los componentes.
*			Ventajas: Flexibilidad y encapsulamiento del estado.
*			Desventajas: Puede agregar complejidad en la implementación.
*	- State Management Persistente:
*		LocalStorage/SessionStorage: Guardar estado en el almacenamiento local o de sesión del navegador.
*			Ventajas: Persistencia del estado entre recargas de la página.
*			Desventajas: Seguridad y tamaño del almacenamiento limitado.
*		LocalStorage/SessionStorage: Guardar estado en el almacenamiento local o de sesión del navegador.
*			Ventajas: Persistencia del estado entre recargas de la página.
*			Desventajas: Seguridad y tamaño del almacenamiento limitado.
*	State Management en Aplicaciones Blazor Server:
*		Circuit State Management: Manejo del estado del circuito que persiste durante la conexión del cliente.
*			Ventajas: Estado persistente mientras dura la conexión.
*			Desventajas: Dependencia de la conexión del cliente.

 
* - Miscelaneo:
* En Blazor, el cascading es un mecanismo que permite pasar datos o servicios de un componente padre a sus componentes hijos sin
* necesidad de pasar explícitamente los valores a través de parámetros ([Parameter]). Se logra usando el componente especial CascadingValue.
* CascadingParameter en los hijos.
* 
* - Tipo por Valor y tipo por referencia:
*	- Tipos por valor: Se almacenan en la pila (stack). Cuando se asignan o pasan como parámetro, se copia su valor.
*		Valor: int, double, float, decimal, bool, char, byte, sbyte, short, ushort, long, ulong, struct, enum, Tuple<>	
*	- Tipos por referencia: Se almacenan en el montón (heap). Cuando se asignan o pasan como parámetro, se copia la referencia al objeto, no el objeto en sí.
*		Referencia: object, string, class, array, interface, delegate, dynamic
*  
* - In Blazor Server, how does the application maintain a real-time connection between the server and the client:
*  - Transport: Choose between WebSockets, Server-Sent Events, and Long Polling. You can modify the transport by configuring SignalR in Startup.cs
*  - MessagePack: Optimize message serialization by using MessagePack instead of the default JSON in SignalR communications. 
*  - Circuit options: Configure circuit settings such as the maximum number of circuits and circuit disposal intervals:
*  - Reconnection: Manage how the app reconnects after a lost connection:
*  
* - How can you enable server-side pre-rendering in a Blazor WebAssembly application:
*	Host.cshtml
*		<component type="typeof(App)" render-mode="ServerPrerendered" />
*		
* - RenderTreeBuilder in the Blazor component rendering process and explain how the component lifecycle is impacted by its use:
*	It is responsible for generating the virtual representation of the component’s DOM, called the render tree. 
*	The render tree is a tree-like data structure that efficiently describes the UI elements and their relationships, 
*	making it easier for Blazor to update the actual DOM in the browser.
* 
* - JavaScript isolation in Blazor WebAssembly: is a feature that allows you to include and execute JavaScript files as part of a specific scoped area, 
*	such as a Razor component, without affecting other components or the global JavaScript context. 
*	This isolation promotes better modularity, maintainability, and security in your application, as it reduces the chances of conflicts 
*	and unintended side effects between different parts of your app.
*	E.g: export JS module
*
* - Blazor’s component virtualization feature: optimize the loading and unloading of items
*	<Virtualize Items="items" Context="item" ItemSize="50">
*		<div>@item.Name</div>
*	</Virtualize>
* So, you can properly handle large datasets efficiently by reducing the memory usage and DOM updates with the Virtualize component.	
*	
* - Custom AuthenticationStateProvider in Blazor:
*	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
*	{
*		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
*		{
*			// Your authentication logic here.
*		}
*	}
*	...
*	// to notify subscribers whenever the authentication state changes. This method takes a task representing the new AuthenticationState	
*	private void NotifyStateChanged()
*	{
*		var authState = GetAuthenticationStateAsync();
*		NotifyAuthenticationStateChanged(authState);
*	}
*	
*	// Register
*	services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
* 
* - How can you optimize the application’s startup performance, and what steps can be taken to reduce the initial download size
*	- Linker configuration (csproj):
*		<PropertyGroup>
*		  <PublishTrimmed>true</PublishTrimmed>
*		  <TrimMode>Link</TrimMode>
*		</PropertyGroup>
*		[!] However, aggressive trimming may cause runtime errors if the linker removes required dependencies. Test your app thoroughly after making changes.
*	
*	- Precompress content: <BlazorEnableCompression>true</BlazorEnableCompression> 
*		Eg: (e.g., .br, .gz files)
*	
*	- Lazy-load assemblies: Split your application into smaller projects and use the OnNavigateAsync method to lazy-load these assemblies as your users navigate between pages. 
*		csproj:
*		<ItremGroup>
*			<BlazorWebAssemblyLazyLoad Include="Radzen.Blazor.dll"/>
*		</ItremGroup>
*		
*	App.razor:
*	@inject Microsoft.AspNetCore.Components.WebAssembly.Services.LazyAssemblyLoader AssemblyLoader
*	
*	<CascadingAutenticationState>
*		<Router ...
*				AddittionalAssemblies="lazyLoadedAssemblies"
*				OnNavigateAsync="OnNavigation">
*		</Router>
*	</CascadingAutenticationState>
*	
*	@code {
*		private List<System.Reflection.Assembly> lazyLoadedAssemblies = new List<System.Reflection.Assembly>();
*		
*		private async Task OnNavigation(NavigationContext context) 
*		{
*			if (context.path == "settings") // Setting razor page
*			{
*				var assemblies = await AssemblyLoader.LoadAssembliesAsync(new[] "Radzen.Blazor.dll" });
*				lazyLoadedAssemblies.AddRange(assemblies);
*			}
*		}
*	}
*	
*	- Reduce external dependencies: Evaluate the external dependencies used in your project, remove any unnecessary ones, and use lighter alternatives when possible.
*	- Optimize images and resources: Compress and optimize images, fonts, and other static resources. 
*	- Server-side rendering (SSR): Although not directly related to reducing the initial download size, SSR can improve the perceived startup performance by sending pre-rendered HTML to the client.
*	
* - How does using a CascadingValue component in Blazor help manage the state of your application, and what are some common use cases for it:
*		<CascadingValue Value="mySharedValue">
*			<ChildComponent />
*		</CascadingValue>
*
*	child component:
*		@inherits ComponentBase
*		[CascadingParameter] private string mySharedValue { get; set; }
*
*		<p>Shared Value: @mySharedValue</p>
*
*
* - RenderMode.Server and RenderMode.ServerPrerendered:
*	 RenderMode.Server: With this render mode, Blazor components are rendered *entirely* on the server, and user interactions with the 
*		components are handled through a real-time connection (e.g., SignalR). 
*	 RenderMode.ServerPrerendered: In this render mode, the components on the hosting page are pre-rendered on the server, 
*		generating an initial HTML markup that is sent to the client. 	
*		
* - How do you implement and use a custom CSS isolation file in a Blazor component and what happens when multiple components have conflicting styles:
*	Creating MyComponent.razor.css.
*		If you need to override the styles or apply global styles, you can create a non-isolated CSS file without the .razor prefix and include it in your application’s index.html (Blazor WebAssembly) 
*		or _Host.cshtml (Blazor Server) file.
*		
* - What are the differences between state management in a Blazor Server application and a Blazor WebAssembly application
*	In a *Blazor Server* application, the application state is maintained on the server *for each active user session*. 
*		This is because the components are executed on the server, and the generated UI updates are sent to the client via *SignalR*. 
*		Implications:
*		[!] Scalability issues may arise as the application needs to track user sessions and maintain state for each connected user.
*		[!] User session state can be lost if the server is restarted or crashes.
*
*	In a Blazor WebAssembly application, the application state is maintained on the client-side, as the components are executed directly in the user’s browser. 
*		Implications:
*		[!] State is stored within the browser’s memory, so if the browser is closed or refreshed, the state can be lost.
*		[!] Storing sensitive data in the client-side state can expose security risks, as an attacker could potentially access and manipulate this data.
*		
* - Mediator pattern in a Blazor application: behavioral design pattern that promotes loose coupling between components by having them communicate through a central mediator object 
*	rather than directly with each other. In a Blazor application, using the Mediator pattern can be beneficial for decoupling components, 
*	centralizing interactions, and facilitating easier communication. 
*	
* -  How do you handle Blazor component disposal and what are the key considerations to ensure proper cleanup of resources:
*		public void Dispose()
*		{
*			// Release your resources here, such as:
*			// - Unsubscribe from event handlers
*			// - Cancel timers or other recurring tasks
*			// - Release unmanaged resources or objects implementing IDisposable
*		}
*
* - Blazor forms and validation, how can you create custom validation attributes and implement the IValidatableObject interface for complex validation logic:
*	Creating custom validation attributes in a Blazor application involves inheriting from the *ValidationAttribute* class and overriding the *IsValid* method. 
*	This method will contain the custom validation logic and will return a *ValidationResult* object based on whether the input value passes or fails the validation.
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<StateContainer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

/*app.UseEndpoints(endpoints =>
{
	endpoints.MapRazorPages();
	endpoints.MapControllers();
	endpoints.Map("api/{**slug}", HandleApiFallback);
	endpoints.MapFallbackToFile("{**slug}", "index.html");
});*/

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();


Task HandleApiFallback(HttpContext context)
{
	context.Response.StatusCode = StatusCodes.Status404NotFound;
	return Task.CompletedTask;
}