using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (MainGameManager.instance.playing)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                MainGameManager.instance.CollectedBall();
                this.gameObject.SetActive(false);
            }
        }
    }
}
