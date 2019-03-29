// GENERATED AUTOMATICALLY FROM 'Assets/Input/NewInput.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class NewInput : InputActionAssetReference
{
    public NewInput()
    {
    }
    public NewInput(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Test
        m_Test = asset.GetActionMap("Test");
        m_Test_Movement = m_Test.GetAction("Movement");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Test = null;
        m_Test_Movement = null;
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
    // Test
    private InputActionMap m_Test;
    private InputAction m_Test_Movement;
    public struct TestActions
    {
        private NewInput m_Wrapper;
        public TestActions(NewInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Test_Movement; } }
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
    }
    public TestActions @Test
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new TestActions(this);
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
