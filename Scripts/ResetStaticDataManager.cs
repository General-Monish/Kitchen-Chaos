using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    public static void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
