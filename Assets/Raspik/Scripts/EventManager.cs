using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnOptionsChange();
    public static event OnOptionsChange onOptionsChange;
    public static void OptionsChanged() {
        if (onOptionsChange != null) {
            onOptionsChange();
        }
    }
}
