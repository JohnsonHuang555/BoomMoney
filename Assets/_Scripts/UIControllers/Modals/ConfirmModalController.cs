using UnityEngine.UIElements;

public class ConfirmModalController : StaticInstance<ConfirmModalController>
{
    public Label modalBody;
    public Button confirmButton;
    public Button cancelButton;

    private void Start()
    {
        var description = ConfirmModalManager.Instance.description;
        var root = GetComponent<UIDocument>().rootVisualElement;
        modalBody = root.Q<Label>("body");
        modalBody.text = description;

        confirmButton = root.Q<Button>("confirm");
        cancelButton = root.Q<Button>("cancel");

        confirmButton.RegisterCallback<ClickEvent>(ev => ConfirmPressed());
        cancelButton.RegisterCallback<ClickEvent>(ev => CancelPressed());
    }

    void ConfirmPressed()
    {

    }

    void CancelPressed()
    {
        ConfirmModalManager.Instance.HideModal();
    }
}

public enum ConfirmType
{
    PutBoom = 0,
}
