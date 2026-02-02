using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameControllor : MonoBehaviour
{
    public bool isDebug = false;
    public GameObject StageOne;
    public GameObject StageTwo;

    public GameObject WinFace;
    public GameObject WinSlogan;

    private GameDataHandle mGameData;

    private bool StageOnePass = false;
    private bool StageTwoPass = false;
    private bool StageThreePass = false;

    //[Header("Callbacks")]
    //[SerializeField] private UnityEvent onStageOnePass;

    public static GameControllor Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        mGameData = gameObject.GetComponent<GameDataHandle>();

        if (isDebug) return;
        GameStart();
    }
    void Update()
    {
        if (!mGameData.isDirty) return;

        var isPass = true;
        foreach (var kv in mGameData.mIndexMap) {
            isPass = isPass && ClothChecker(kv.Key, kv.Value);
        }
        StageOnePass  = isPass;

        if (StageOnePass)
        {
            WinFace.SetActive(true);
            WinSlogan.SetActive(true);
        }
        else {
            WinFace.SetActive(false);
            WinSlogan.SetActive(false);
        }

            mGameData.isDirty = false;
    }

    private void GameStart() {
        GameReset();

        if (StageOne)
            StageOne.SetActive(true);
        if (StageTwo)
            StageTwo.SetActive(false);
    }

    public void GameReset(bool includeInactive = true)
    {
        var items = Object.FindObjectsOfType<ResettableBehaviour>(includeInactive);

        for (int i = 0; i < items.Length; i++)
        {
            items[i].Reset();
        }
    }

    private bool ClothChecker(string name, int index) {
        var isPass = true;
        switch (name)
        {
            case "Root_Hair":
                if (index != 4)
                    isPass = false;
                break;
            case "Root_Body":
                if (index != 3)
                    isPass = false;
                break;
            case "Root_Neck":
                if (index != 2)
                    isPass = false;
                break;
            default:
                break;
        }
        return isPass;
    }

    public int GetIndex(string key, int defaultValue = 0)
    {
        if (mGameData == null) return defaultValue;

        if (string.IsNullOrEmpty(key))
            return defaultValue;

        if (mGameData.mIndexMap.TryGetValue(key, out int v))
            return v;

        return defaultValue;
    }

    public void SetIndex(string key, int index)
    {
        if (mGameData == null) return;

        if (string.IsNullOrEmpty(key))
            return;

        mGameData.mIndexMap[key] = index;
        mGameData.isDirty = true;
    }

    public void ToStageTwo()
    {
        if(!StageOnePass) 
            return;
        
        if(StageOne)
            StageOne.SetActive(false);
        if(StageTwo)
            StageTwo.SetActive(true);
    }
}
