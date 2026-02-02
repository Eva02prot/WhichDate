using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteActivityManager : MonoBehaviour
{
    public void SwitchActivityByName(string name) {
        var targetTrans = transform.Find(name);

        if (targetTrans != null)
        {
            targetTrans.gameObject.SetActive(!targetTrans.gameObject.activeSelf);
        }
    }
}
