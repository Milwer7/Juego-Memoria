using Meta.WitAi.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private PointsManager Manager;
    private int PointsModifier;
    [SerializeField]
    private AudioClip ObstacleCollisionSound;
    public void AttachManager(PointsManager manager)
    {
        Manager = manager;
    }
    public void SetPointsModifier(int modifier)
    {
        PointsModifier = modifier;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with:" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLISION WORKING");
            Manager.ModifyPoints(PointsModifier);
            SoundFXManager.instance.PlaySoundFXClip(ObstacleCollisionSound, transform, 0.8f);
            Destroy(this.gameObject);
        }
    }
}
