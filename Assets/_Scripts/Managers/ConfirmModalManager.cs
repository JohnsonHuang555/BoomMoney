using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmModalManager : StaticInstance<ConfirmModalManager>
{
    [SerializeField] GameObject confirmModal;

    public void ShowModal()
    {
        confirmModal.SetActive(true);
    }
}
