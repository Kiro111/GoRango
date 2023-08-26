using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
    Image healtBar;
    public float maxHealt = 1;
    public float HP;

    private void Start()
    {
        healtBar = GetComponent<Image>();
        HP = maxHealt;
    }

    private void Update()
    {
        healtBar.fillAmount = HP / maxHealt;
    }
}
