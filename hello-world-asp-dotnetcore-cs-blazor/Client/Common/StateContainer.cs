namespace hello_world_asp_dotnetcore_cs_blazor.Client.Common;

public class StateContainer
{
	private string visionLineaVistaActual = string.Empty;

	
	public string VisionLineaVistaActual
	{
		get => visionLineaVistaActual;
		set
		{
			visionLineaVistaActual = value;
			NotifyStateChanged();
		}
	}
	
	public event Action OnChange;

	private void NotifyStateChanged() => OnChange?.Invoke();

}