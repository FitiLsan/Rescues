namespace Rescues
{
    public enum UpdateType
    {
        None            = 0,
        Fixed           = 1,
        Update          = 2,
        Late            = 3,
        #if UNITY_EDITOR
        Gizmos          = 4
        #endif
    }
}
