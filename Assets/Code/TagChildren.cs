using UnityEngine;
using UnityEditor;

public class TagChildren : MonoBehaviour
{
    [MenuItem("Tools/Tag All Children")]
    static void TagAllChildren()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("먼저 오브젝트를 선택하세요.");
            return;
        }

        string tagToApply = Selection.activeGameObject.tag;

        foreach (Transform child in Selection.activeGameObject.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.tag = tagToApply;
        }

        Debug.Log("모든 자식에게 태그 '" + tagToApply + "' 적용 완료");
    }
}
