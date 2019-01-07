using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] inScene = FindObjectsOfType<T>();
                if (inScene.Length >= 1)
                    instance = inScene[0];
                for (int i = 1; i < inScene.Length; i++)
                    Destroy(inScene[i].gameObject);
                if (instance == null)
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = typeof(T).ToString();
                    instance = gameObj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}