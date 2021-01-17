using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public interface IInventorySignals 
{
    IObservable<Unit> Up { get; }

    IObservable<Unit> Down { get; }

    IObservable<Unit> CloseTextBox { get; }
}
