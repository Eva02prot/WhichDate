using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameControllor : MonoBehaviour
{
    private bool StageOnePass = false;
    private bool StageTwoPass = false;
    private bool StageThreePass = false;

    public GameObject WinFace;
    public GameObject WinSlogan;

    private GameDataHandle mGameData;

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
    }
    void Update()
    {
        if (!mGameData.isDirty) return;

        foreach (var kv in mGameData.mIndexMap) {
            ClothChecker(kv.Key, kv.Value);
        }

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

    private void ClothChecker(string name, int index) {
        switch (name)
        {
            case "Root_Hair":
                if (index == 4)
                    StageOnePass = true;
                else
                    StageOnePass = false;
                break;
            case "Root_Body":
                if (index == 3)
                    StageOnePass = StageOnePass && true;
                else
                    StageOnePass = false;
                break;
            case "Root_Neck":
                if (index == 2)
                    StageOnePass = StageOnePass && true;
                else
                    StageOnePass = false;
                break;
            default:
                break;
        }
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
}
