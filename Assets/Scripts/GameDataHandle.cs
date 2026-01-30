using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameDataHandle : MonoBehaviour
{
    public readonly Dictionary<string, int> mIndexMap = new Dictionary<string, int>(4);

    public bool isDirty = false;


    [ContextMenu("Print Stored Data")]
    private void PrintStoredData()
    {
        var sb = new StringBuilder(256);

        sb.AppendLine("==== SpriteSwitchStore Data ====");
        sb.AppendLine($"runtimeCacheCount = {mIndexMap.Count}");

        foreach (var kv in mIndexMap)
        {
            sb.AppendLine($"{kv.Key} = {kv.Value}");
        }

        if (mIndexMap.Count == 0)
        {
            sb.AppendLine("(empty) 说明当前运行缓存里还没有任何 key 被 Get/Set 过。");
        }

        sb.AppendLine("================================");

        Debug.Log(sb.ToString(), this);
    }
}
