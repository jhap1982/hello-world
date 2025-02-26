using hello_world_asp_dotnetcore_cs_blazor.Client.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace hello_world_asp_dotnetcore_cs_blazor.Client.Pages
{
	/// <summary>
	/// [Constructor] → 
	/// SetParametersAsync → 
	/// OnInitialized/OnInitializedAsync → 
	/// OnParametersSet/OnParametersSetAsync → 
	/// ShouldRender → 
	/// OnAfterRender/OnAfterRenderAsync → 
	/// Dispose
	/// </summary>
	public partial class Counter : ComponentBase, IDisposable
	{
		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		[Inject]
		protected StateContainer StateContainer { get; set; }


		// Blazor localization. E.g.: Localizer["str_test"]
		[Inject]
		public IStringLocalizer<Resource.Resource> Localizer { get; set; }

		private DotNetObjectReference<Counter>? DotNetHelper;

		private bool IsDisposed;

		private int CurrentCount { get; set; } = 0;
		private string BrowserName { get; set; } = string.Empty;
		private string InfoInterop { get; set; } = string.Empty;

		/// <summary>
		/// Mecanismo en Blazor para manejar eventos y permitir la comunicación entre componentes. 
		/// Proporciona una forma eficiente de invocar métodos cuando ocurren eventos específicos
		/// </summary>
		[Parameter] 
		public EventCallback OnButtonClick { get; set; }

		/// <summary>
		/// Para cargar y ejecutar JavaScript como un módulo en Blazor (mymodule.js).
		/// </summary>
		private IJSObjectReference _module;

		public Counter()
		{
			IsDisposed = false;
		}

		~Counter()
		{
			Dispose(false);
		}


		/// <summary>
		/// Este método se llama primero y permite manejar parámetros asíncronamente. 
		/// Puede anularse para aplicar lógica personalizada antes de que los parámetros se establezcan.
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public override Task SetParametersAsync(ParameterView parameters)
		{
			Console.WriteLine("Counter.razor -> SetParametersAsync()");

			return base.SetParametersAsync(parameters);
		}

		/// <summary>
		/// Se llama una vez que el componente se inicializa. Es un buen lugar para colocar la lógica de inicialización que no depende de parámetros. 
		/// OnInitializedAsync es la versión asincrónica y permite ejecutar código asíncrono durante la inicialización
		/// </summary>
		/// <returns></returns>
		protected override async Task OnInitializedAsync()
		{
			Console.WriteLine("Counter.razor -> OnInitializedAsync()");

			_module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/mymodule.js");

			StateContainer.OnChange += StateHasChanged;

			await base.OnInitializedAsync();
		}

		/// <summary>
		/// Se llama cada vez que se establecen parámetros para el componente. Esto puede ocurrir múltiples veces durante la vida del componente, 
		/// por ejemplo, cuando el usuario navega a una nueva URL que pasa diferentes parámetros. OnParametersSetAsync permite manejar lógica asíncrona.
		/// </summary>
		/// <returns></returns>
		protected override Task OnParametersSetAsync()
		{
			Console.WriteLine("Counter.razor -> OnParametersSetAsync()");

			return base.OnParametersSetAsync();
		}

		/// <summary>
		/// Se llama después de que Blazor ha renderizado el componente. 
		/// Es útil para lógica que necesita acceder al DOM, como la configuración de bibliotecas de JavaScript. 
		/// OnAfterRenderAsync permite ejecutar lógica asíncrona después del renderizado. 
		/// La propiedad firstRender permite distinguir entre el primer renderizado y los renderizados subsiguientes.
		/// </summary>
		/// <param name="firstRender"></param>
		/// <returns></returns>
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			Console.WriteLine($"Counter.razor -> OnAfterRenderAsync({firstRender})");

			if (firstRender)
			{
				DotNetHelper = DotNetObjectReference.Create(this);

				await JSRuntime.InvokeVoidAsync("registrarDotNetHelper", DotNetHelper);
			}

			await base.OnAfterRenderAsync(firstRender);
		}

		/// <summary>
		/// Este método se puede sobrescribir para controlar si el componente debe renderizarse. 
		/// Retorna un valor booleano indicando si se debe proceder con la renderización. Útil para optimizar el rendimiento.
		/// </summary>
		/// <returns></returns>
		protected override bool ShouldRender()
		{
			return base.ShouldRender();
		}

		/// <summary>
		/// Se llama cuando el componente se elimina del DOM. Es el lugar para liberar recursos, cancelar operaciones pendientes, o limpiar el estado.
		/// </summary>
		public void Dispose()
		{
			// https://learn.microsoft.com/es-es/dotnet/fundamentals/code-analysis/quality-rules/ca1063
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed)
			{
				return;
			}

			if (disposing)
			{
				// Release resources
				_module?.DisposeAsync();

				// Unsuscribe action
				StateContainer.OnChange -= StateHasChanged;
			}

			IsDisposed = true;
		}

		private void IncrementCount()
		{
			CurrentCount++;
		}

		/// <summary>
		/// Invocar Funciones del Módulo Javascript
		/// </summary>
		protected async Task ShowAlert()
		{
			await _module.InvokeVoidAsync("showAlert", "Hello from Blazor!");
		}

		protected async Task Calculate()
		{
			var result = await _module.InvokeAsync<int>("addNumbers", 5, 3);
			Console.WriteLine($"Result of addition: {result}");
		}

		/// <summary>
		/// Interop: JS -> C#
		/// </summary>
		/// <returns></returns>
		private async Task GetBrowser()
		{
			BrowserName = await JSRuntime.InvokeAsync<string>("getBrowserName", "N/A");
		}
		
		[JSInvokable]
		public async Task CallCSharpFromJavascript(string infoInterop = "Llamado desde C#")
		{
			InfoInterop = infoInterop;

			StateHasChanged();

			await Task.CompletedTask;
		}

	}
}