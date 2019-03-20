// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class InputMaster : InputActionAssetReference
{
    public InputMaster()
    {
    }
    public InputMaster(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Testing
        m_Testing = asset.GetActionMap("Testing");
        m_Testing_Movement = m_Testing.GetAction("Movement");
        // WORk
        m_WORk = asset.GetActionMap("WORk");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Testing = null;
        m_Testing_Movement = null;
        m_WORk = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Testing
    private InputActionMap m_Testing;
    private InputAction m_Testing_Movement;
    public struct TestingActions
    {
        private InputMaster m_Wrapper;
        public TestingActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Testing_Movement; } }
        public InputActionMap Get() { return m_Wrapper.m_Testing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(TestingActions set) { return set.Get(); }
    }
    public TestingActions @Testing
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new TestingActions(this);
        }
    }
    // WORk
    private InputActionMap m_WORk;
    public struct WORkActions
    {
        private InputMaster m_Wrapper;
        public WORkActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_WORk; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(WORkActions set) { return set.Get(); }
    }
    public WORkActions @WORk
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new WORkActions(this);
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get

        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
}
