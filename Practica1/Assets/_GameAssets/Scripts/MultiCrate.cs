using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCrate : MonoBehaviour
{
    public EBehaviour CurrentBehaviour;
    public enum EBehaviour
    {
        life = 0,
        Granada = 1,
        AKM = 2,
        Pistola = 3
    }

    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        mat.SetColor("_Color", CurrentBehaviour > 0 ? new Color(0.4f, 0.4f, 1) : new Color(1, 0.2f, 0));
        if (CurrentBehaviour > 0)
        {
            int indexChild = (int)CurrentBehaviour - 1;
            transform.GetChild(indexChild).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
