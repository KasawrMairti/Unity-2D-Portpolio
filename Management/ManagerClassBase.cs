using UnityEngine;
using UnityProjectStartupFramework;

public abstract class ManagerClassBase<T> : MonoBehaviour, IManagerClass 
	where T : class, IManagerClass
{
	public static T Instance => GameManager.GetManagerClass<T>();

	public abstract void InitializeManagerClass();
}
