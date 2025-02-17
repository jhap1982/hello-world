using hello_world_asp_dotnetcore_cs_blazor.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace hello_world_asp_dotnetcore_cs_blazor.Client.Pages
{
	public partial class FetchData
	{
		[Inject]
		private HttpClient Http { get; set; }

		private WeatherForecast[]? forecasts;

		protected override async Task OnInitializedAsync()
		{
			forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
		}
	}
}