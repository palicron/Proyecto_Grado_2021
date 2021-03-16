using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheckPointUpdate : MonoBehaviour
{
    [SerializeField]
    bool OnUse = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            GameManager.CheckPoint = this.transform.position;
            if(OnUse)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
