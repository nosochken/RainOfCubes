using TMPro;
using UnityEngine;

public class DisplaySpawnerStatistic<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TextMeshProUGUI _createdObjectsAmountText;
    [SerializeField] private TextMeshProUGUI _spawnedObjectsAmountText;
	[SerializeField] private TextMeshProUGUI _activeObjectsAmounText;
	
	private void OnEnable()
	{
		_spawner.CreatedObjectsAmountHasChanged += UpdateCreatedObjectsAmount;
		_spawner.SpawnedObjectsAmountHasChanged += UpdateSpawnedObjectsAmount;
		_spawner.ActiveObjectsAmountHasChanged += UpdateActiveObjectsAmount;
	}
	
	private void OnDisable()
	{
		_spawner.CreatedObjectsAmountHasChanged -= UpdateCreatedObjectsAmount;
		_spawner.SpawnedObjectsAmountHasChanged -= UpdateSpawnedObjectsAmount;
		_spawner.ActiveObjectsAmountHasChanged -= UpdateActiveObjectsAmount;
	}
	
	private void Start()
	{
		int startTime = 0;
		
		Display(_createdObjectsAmountText, startTime);
		Display(_spawnedObjectsAmountText, startTime);
		Display(_activeObjectsAmounText, startTime);
	}
	
	private void UpdateCreatedObjectsAmount(int amount) => Display(_createdObjectsAmountText, amount);
	private void UpdateSpawnedObjectsAmount(int amount) => Display(_spawnedObjectsAmountText, amount);
	private void UpdateActiveObjectsAmount(int amount) => Display(_activeObjectsAmounText, amount);
	
	private void Display(TextMeshProUGUI text, int amount)
	{
		text.text = amount.ToString();
	}
}