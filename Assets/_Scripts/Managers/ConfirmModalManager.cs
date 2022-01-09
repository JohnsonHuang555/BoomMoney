using UnityEngine;

public class ConfirmModalManager : StaticInstance<ConfirmModalManager>
{
    [SerializeField] GameObject confirmModal;

    public string description;
    public ConfirmType confirmType;

    public void ShowModal(ConfirmModalParams cp)
    {
        description = cp.description;
        confirmType = cp.confirmType;
        confirmModal.SetActive(true);
    }

    public void HideModal()
    {
        confirmModal.SetActive(false);
    }
}

public struct ConfirmModalParams
{
    public string description;
    public ConfirmType confirmType;
}
