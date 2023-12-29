using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool IsFirstupdate = true;


    private void Update()
    {
        if (IsFirstupdate)
        {
            IsFirstupdate = false;

            LoaderUI.LoaderCallback();
        }
    }
}
