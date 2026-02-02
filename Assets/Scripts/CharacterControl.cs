using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : ResettableBehaviour
{
    public GameObject FrontFace;
    public GameObject BackFace;
    public void CharacterPartBegin() { 
        this.gameObject.SetActive(true);
        Reset();
    }

    public override void Reset()
    {
        if(FrontFace)
            FrontFace.SetActive(true);
        if(BackFace)
            BackFace.SetActive(false);
    }
}
