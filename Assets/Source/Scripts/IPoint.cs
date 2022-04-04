using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoint
{
    bool IsBusy { get; }
    void Release();
    void Take();
}
