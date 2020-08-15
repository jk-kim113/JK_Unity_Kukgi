using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    public abstract void ExitButton();

    public virtual void ChangePhase(bool isNext)
    {

    }
}
