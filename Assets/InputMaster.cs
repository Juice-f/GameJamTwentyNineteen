// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;

[Serializable]
public class InputMaster : InputActionAssetReference {
    public InputMaster () { }
    public InputMaster (InputActionAsset asset) : base (asset) { }
    private bool m_Initialized;
    private void Initialize () {
        // Hej
        m_Hej = asset.GetActionMap ("Hej");
        m_Hej_Move = m_Hej.GetAction ("Move");
        m_Initialized = true;
    }
    private void Uninitialize () {
        m_Hej = null;
        m_Hej_Move = null;
        m_Initialized = false;
    }
    public void SetAsset (InputActionAsset newAsset) {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize ();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions () {
        SetAsset (ScriptableObject.Instantiate (asset));
    }
    // Hej
    private InputActionMap m_Hej;
    private InputAction m_Hej_Move;
    public struct HejActions {
        private InputMaster m_Wrapper;
        public HejActions (InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move { get { return m_Wrapper.m_Hej_Move; } }
        public InputActionMap Get () { return m_Wrapper.m_Hej; }
        public void Enable () { Get ().Enable (); }
        public void Disable () { Get ().Disable (); }
        public bool enabled { get { return Get ().enabled; } }
        public InputActionMap Clone () { return Get ().Clone (); }
        public static implicit operator InputActionMap (HejActions set) { return set.Get (); }
    }
    public HejActions @Hej {
        get {
            if (!m_Initialized) Initialize (); <<
            <<<<< HEAD : Assets / InputMaster.cs
            return new HejActions (this);
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
        ==
        == == =
        return new TestingActions (this);
}
}
// WORk
private InputActionMap m_WORk;
public struct WORkActions {
    private InputMaster m_Wrapper;
    public WORkActions (InputMaster wrapper) { m_Wrapper = wrapper; }
    public InputActionMap Get () { return m_Wrapper.m_WORk; }
    public void Enable () { Get ().Enable (); }
    public void Disable () { Get ().Disable (); }
    public bool enabled { get { return Get ().enabled; } }
    public InputActionMap Clone () { return Get ().Clone (); }
    public static implicit operator InputActionMap (WORkActions set) { return set.Get (); }
}
public WORkActions @WORk
    >>
    >>>>> d94eb3dd86cb6c18300962e3abc374ba7e1fc2da : Assets / Input / InputMaster.cs {
        get { <<
            <<<<< HEAD : Assets / InputMaster.cs
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex ("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex]; ==
            == == =
            if (!m_Initialized) Initialize ();
            return new WORkActions (this); >>
            >>>>> d94eb3dd86cb6c18300962e3abc374ba7e1fc2da : Assets / Input / InputMaster.cs
        }
    }
private int m_KeyboardandmouseSchemeIndex = -1;
public InputControlScheme KeyboardandmouseScheme {
    get

    {
        if (m_KeyboardandmouseSchemeIndex == -1) m_KeyboardandmouseSchemeIndex = asset.GetControlSchemeIndex ("Keyboard and mouse");
        return asset.controlSchemes[m_KeyboardandmouseSchemeIndex];
    }
}
}