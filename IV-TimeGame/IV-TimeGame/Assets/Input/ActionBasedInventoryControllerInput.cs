using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;
using UniRx.Triggers;

public class ActionBasedInventoryControllerInput : InventoryInputControl
{

    public override IObservable<Unit> Up => _up;
    private Subject<Unit> _up;
    public override IObservable<Unit> Down => _down;
    private Subject<Unit> _down;

    public override IObservable<Unit> CloseTextBox=> _closetextbox;
    private Subject<Unit> _closetextbox;

    public MenuControls _controls;

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    protected void Awake()
    {
        _controls = new MenuControls();
        // Jump:
        _up = new Subject<Unit>().AddTo(this);
        _controls.Inventory.Up.performed += context => _up.OnNext(Unit.Default);

        //ToggleMenu
       _down = new Subject<Unit>().AddTo(this);
        _controls.Inventory.Down.performed += context => _down.OnNext(Unit.Default);

        _closetextbox = new Subject<Unit>().AddTo(this);
        _controls.Inventory.CloseTextBox.performed += context => _closetextbox.OnNext(Unit.Default);
    }
}
