using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmModalController : StaticInstance<ConfirmModalController>
{
    private void Start()
    {
        var description = ConfirmModalManager.Instance.description;
        Debug.Log("start... " + description);
    }
}
