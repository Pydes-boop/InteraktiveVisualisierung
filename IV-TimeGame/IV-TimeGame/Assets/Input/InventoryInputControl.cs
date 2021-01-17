using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public abstract class InventoryInputControl : MonoBehaviour
{
    public abstract IObservable<Unit> Up { get; }
    public abstract IObservable<Unit> Down { get; }
    public abstract IObservable<Unit> CloseTextBox { get; }
}
