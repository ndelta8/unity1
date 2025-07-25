using UnityEngine;
using UnityEditor;

public class TagChildren : MonoBehaviour
{
    [MenuItem("Tools/Tag All Children")]
    static void TagAllChildren()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("���� ������Ʈ�� �����ϼ���.");
            return;
        }

        string tagToApply = Selection.activeGameObject.tag;

        foreach (Transform child in Selection.activeGameObject.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.tag = tagToApply;
        }

        Debug.Log("��� �ڽĿ��� �±� '" + tagToApply + "' ���� �Ϸ�");
    }
}
