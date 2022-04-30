using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    public void SetText(float amountToShow) {
        text.text = amountToShow.ToString();
    }

    public void DestroyOnAnimEnd() {
        Destroy(this.gameObject);
    }
}
