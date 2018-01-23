using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCrate : MonoBehaviour
{
    public EBehaviour CurrentBehaviour;

    private  int[] Restore = new int[] { 50, 96, 280, 2 }; //Van en orden: Vida, Granadas, AKM, Pistola

    public enum EBehaviour
    {
        life = 0,
        Gun = 1,
        AKM = 2,
        Grenade = 3
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
        ShooterCharacter player = other.GetComponent<ShooterCharacter>();
        if (!player) return;
        int index = (int)CurrentBehaviour;
        if (CurrentBehaviour == EBehaviour.life)
        {
            if(player.Heal(Restore[index])) Destroy(gameObject);
        }
        else
        {
            if(player.AddAmmo(index - 1, Restore[index])) Destroy(gameObject); 
        }
        
    }
}
