/**
 * Tipos de Blazor:
 * 1. Blazor Server: La lógica de la aplicación se ejecuta en el servidor. Los cambios en la interfaz de usuario se comunican a través de una conexión SignalR.
*  2. Blazor WebAssembly: La lógica de la aplicación se ejecuta en el navegador del cliente mediante WebAssembly. No se necesita conexión constante con el servidor.
*  
*  Componentes:
*  - Blazor se basa en componentes reutilizables que encapsulan UI y lógica.
*  - Los componentes se pueden anidar y reutilizar, y están escritos en Razor (una combinación de C# y HTML).
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
* 
* Miscelaneo:
* En Blazor, el cascading es un mecanismo que permite pasar datos o servicios de un componente padre a sus componentes hijos sin
* necesidad de pasar explícitamente los valores a través de parámetros ([Parameter]). Se logra usando el componente especial CascadingValue.
* CascadingParameter en los hijos.
* 
* Tipo por Valor y tipo por referencia:
* - Tipos por valor: Se almacenan en la pila (stack). Cuando se asignan o pasan como parámetro, se copia su valor.
* - Tipos por referencia: Se almacenan en el montón (heap). Cuando se asignan o pasan como parámetro, se copia la referencia al objeto, no el objeto en sí.
* 
* Valor: int, double, float, decimal, bool, char, byte, sbyte, short, ushort, long, ulong, struct, enum, Tuple<>	
* Referencia: object, string, class, array, interface, delegate, dynamic
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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