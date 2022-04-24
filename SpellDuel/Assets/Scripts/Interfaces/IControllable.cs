using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable<T>
{
    void Controll(T t);
}
