using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMicro : MonoBehaviour
{
    public string dev;
    public void SelectMicro()
    {
        Globs.mainMicro = dev;
        print(Globs.mainMicro);
    }
}
