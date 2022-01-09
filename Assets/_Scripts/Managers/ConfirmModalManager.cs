using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmModalManager : StaticInstance<ConfirmModalManager>
{
    [SerializeField] GameObject confirmModal;

    public string description;

    public void ShowModal(AA aa)
    {
        description = aa.description;
        confirmModal.SetActive(true);
    }
}

public struct AA
{
    public string description;
}
