using UnityEngine;
using UnityEngine.UIElements;

public class ConfirmModalController : MonoBehaviour
{
    public Label modalBody;
    public Button confirmButton;
    public Button cancelButton;

    //private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    //private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    //private void OnStateChanged(GameState newState)
    //{
    //    Debug.Log("yoyoyo");
    //    if (newState == GameState.PlayerRoundTime)
    //    {
    //        var description = ConfirmModalManager.Instance.description;
    //        var root = GetComponent<UIDocument>().rootVisualElement;
    //        modalBody = root.Q<Label>("body");
    //        modalBody.text = description;

    //        confirmButton = root.Q<Button>("confirm");
    //        cancelButton = root.Q<Button>("cancel");

    //        confirmButton.RegisterCallback<ClickEvent>(ev => ConfirmPressed());
    //        cancelButton.RegisterCallback<ClickEvent>(ev => CancelPressed());
    //    }
    //}

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

    private void Update()
    {
        Debug.Log("yoyo");
    }

    void ConfirmPressed()
    {
        var confirmType = ConfirmModalManager.Instance.confirmType;
        switch (confirmType)
        {
            case ConfirmType.PutBoom:
                UnitManager.Instance.SpawnItem(ItemType.Bomb);
                CancelPressed();
                break;
        }
    }

    void CancelPressed()
    {
        ConfirmModalManager.Instance.HideModal();
        GameManager.Instance.ShowEndRoundButton();
    }
}

public enum ConfirmType
{
    PutBoom = 0,
}
