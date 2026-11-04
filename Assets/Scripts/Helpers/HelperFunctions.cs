namespace Assets.Scripts.Helpers
{
    using Unity.Mathematics;
    using UnityEngine;

    public class HelperFunctions : MonoBehaviour
    {
        public static bool TryInstantiate<T>(
    T original,
    out T instance,
    string name = "",
    Vector3 pos = default,
    Quaternion rot = default,
    Transform parent = null
) where T : Object
        {
            instance = null;

            if (original == null)
            {
                Debug.LogError($"Tried to instantiate {name} but obj was null. Check ContentManager.");
                return false;
            }
            
            if (rot == default)
                rot = Quaternion.identity;

            instance = Instantiate(original, pos, rot, parent);

            return true;
        }
    }
}