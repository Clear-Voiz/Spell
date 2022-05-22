using UnityEngine;

public abstract class View : MonoBehaviour
{
    public bool IsInitialized { get; private set;}

    public virtual void Initialize()
    {
        IsInitialized = true;
    }
}
