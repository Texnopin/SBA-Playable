using UnityEditor;
using UnityEngine;

namespace _Project.Code.Editor
{
    public class ReplaceWithPrefab : MonoBehaviour
    {
        [MenuItem("Tools/Replace Objects with Prefab")]
        static void Replace()
        {
            GameObject prefab = Selection.activeObject as GameObject;
            if (prefab == null)
            {
                Debug.LogError("Выбери префаб в Project!");
                return;
            }

            GameObject[] objects = GameObject.FindGameObjectsWithTag("ReplaceMe"); // Или искать по имени

            foreach (GameObject obj in objects)
            {
                Vector3 position = obj.transform.position;
                Quaternion rotation = obj.transform.rotation;
                Transform parent = obj.transform.parent; // Сохраняем родителя, если есть

                GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                newObj.transform.SetPositionAndRotation(position, rotation);
                newObj.transform.parent = parent; // Восстанавливаем родителя

                DestroyImmediate(obj);
            }

            Debug.Log($"Заменено {objects.Length} объектов!");
        }
    }
}