using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackInfo : MonoBehaviour {

    [SerializeField] Text txtName;
    [SerializeField] Text txtPP;
    
    public void UpdateAttack(string Name, string MaxPP, string MinPP)
    {
        txtName.text = Name;
        txtPP.text = string.Format("{0} / {1}", MinPP, MaxPP);
    }

}
