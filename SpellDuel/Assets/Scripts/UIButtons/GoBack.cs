using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    public GameObject ob;
    public List<GameObject> rubbish;
    public void Return()
    {
        foreach (var button in rubbish)
        {
            Destroy(button);
        }
        rubbish.Clear();
        ob.SetActive(true);
    }
}
