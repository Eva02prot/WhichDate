using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTwoReset : ResettableBehaviour
{
    public GameObject PencilBox;
    public GameObject Diary;
    public GameObject PencilBoxButton;
    public GameObject DiaryButton;
    public GameObject BGTouchGroup;

    public override void Reset()
    {
        PencilBox?.SetActive(false);
        Diary?.SetActive(false);
        PencilBoxButton?.SetActive(true);
        DiaryButton?.SetActive(true);
        BGTouchGroup?.SetActive(false);
    }
}
