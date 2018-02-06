using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenuElement : MenuElement {

    private int IndexInMenu;
    private CombatManager CombatManager;

    public void Initialize(int index, CombatManager cm)
    {
        IndexInMenu = index;
        CombatManager = cm;
    }

    //Nos han seleccionado
	public void OnEnter()
    {
        CombatManager.OnSkillUsed(IndexInMenu);
    }
}
