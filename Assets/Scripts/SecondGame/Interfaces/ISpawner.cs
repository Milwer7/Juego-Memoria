using UnityEngine;

/// <summary>
/// Spawner Interface implemented by every spawner of the second game.
/// Calls the mandatory Spawn method for spawning childs
/// and the ClearChilds method to despawn its childs.
/// </summary>
public interface ISpawner
{
    void Spawn();
    void ClearChilds();
}