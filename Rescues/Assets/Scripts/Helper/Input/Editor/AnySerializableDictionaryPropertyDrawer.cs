using UnityEditor;
using SerializableDict;

namespace Rescues
{
    [CustomPropertyDrawer(typeof(GamepadInputSpriteDictionary))]
    public class AnySerializableDictionaryPropertyDrawer :
        SerializableDictionaryPropertyDrawer
    { }
}
