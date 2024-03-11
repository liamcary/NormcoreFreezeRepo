using Normal.Realtime;
using UnityEngine;

public class BasicAvatarSpawner : MonoBehaviour
{
	[SerializeField] Realtime _realtime;
	[SerializeField] GameObject _avatarPrefab;

	void Awake()
	{
		_realtime.didConnectToRoom += HandleDidConnectToRoom;
	}

	void OnDestroy()
	{
		_realtime.didConnectToRoom -= HandleDidConnectToRoom;
	}

	void HandleDidConnectToRoom(Realtime realtime)
	{
		SpawnAvatar();
	}

	[ContextMenu("Spawn Avatar")]
	void SpawnAvatar()
	{
		Debug.Log("Spawning avatar");

		Realtime.Instantiate(_avatarPrefab.name, new Realtime.InstantiateOptions {
			ownedByClient = true,
			preventOwnershipTakeover = true,
			destroyWhenOwnerLeaves = true,
			destroyWhenLastClientLeaves = true,
			useInstance = _realtime
		});
	}
}
