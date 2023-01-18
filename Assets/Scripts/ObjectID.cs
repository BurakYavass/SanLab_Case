using UnityEngine;

public class ObjectID : MonoBehaviour
{
    // GameObject ID
    public enum ObjectType
    {
        DragItem,
        DropPlace,
        RotateItem,
    }

    public ObjectType Type;
}
