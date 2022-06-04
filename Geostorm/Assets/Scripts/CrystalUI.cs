using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalUI : MonoBehaviour
{
    public Text crystalCount;
    [SerializeField] public int crystalAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crystalCount.text = crystalAmount.ToString();
    }
}
