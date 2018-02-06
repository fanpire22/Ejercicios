using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackInfo : MonoBehaviour {

    [SerializeField] Text txtName;
    [SerializeField] Text txtAttack;
    
    public void UpdateAttack(string Name, string Attack)
    {
        txtName.text = Name;
        txtAttack.text = string.Format(Attack);
    }

}
