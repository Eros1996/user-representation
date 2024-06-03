using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class AnimateOnInput : MonoBehaviour
{
    private Animator m_Animator;
    private XRInputModalityManager m_InputModalityManager;
    private ActionBasedController m_LeftControllerAction, m_RightControllerAction;
    
    private static readonly int VrikTriggerL = Animator.StringToHash("VRIK_TriggerL");
    private static readonly int VrikGripL = Animator.StringToHash("VRIK_GripL");
    private static readonly int VrikTriggerR = Animator.StringToHash("VRIK_TriggerR");
    private static readonly int VrikGripR = Animator.StringToHash("VRIK_GripR");

    private void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        var xrRig = GameObject.Find("XR Origin (XR Rig)");
        m_InputModalityManager = xrRig.GetComponent<XRInputModalityManager>();
        m_LeftControllerAction = m_InputModalityManager.leftController.GetComponent<ActionBasedController>();
        m_RightControllerAction = m_InputModalityManager.rightController.GetComponent<ActionBasedController>();
    }
    
    private void LateUpdate()
    {
        m_Animator.SetFloat(VrikTriggerL, m_LeftControllerAction.activateActionValue.action.ReadValue<float>());
        m_Animator.SetFloat(VrikGripL, m_LeftControllerAction.selectActionValue.action.ReadValue<float>());
        m_Animator.SetFloat(VrikTriggerR, m_RightControllerAction.activateActionValue.action.ReadValue<float>());
        m_Animator.SetFloat(VrikGripR, m_RightControllerAction.selectActionValue.action.ReadValue<float>());
    }

    private void OnEnable()
    {
        //m_Animator.writeDefaultValuesOnDisable = true;
    }
    
    private void OnDisable()
    {
        //m_Animator.WriteDefaultValues();
    }
}
