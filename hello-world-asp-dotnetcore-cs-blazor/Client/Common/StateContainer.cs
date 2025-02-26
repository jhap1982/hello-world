namespace hello_world_asp_dotnetcore_cs_blazor.Client.Common;

public class StateContainer
{
	private string _someGlobalValue = string.Empty;

	
	public string SomeGlobalValue
	{
		get => _someGlobalValue;
		set
		{
			_someGlobalValue = value;
			NotifyStateChanged();
		}
	}
	
	public event Action OnChange;

	private void NotifyStateChanged() => OnChange?.Invoke();

}