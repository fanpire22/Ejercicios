using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    [Header("User Interface")]
    [SerializeField] MenuElement[] MainOptions;
    [SerializeField] MenuElement[] CombatOptions;
    [SerializeField] GameObject AttackMenu;
    [SerializeField] GameObject MainMenu;
    [SerializeField] AttackInfo atkInfo;
    
    //opción de la ventana derecha
    int currentMainOptionIndex;
    //opción de la ventana izquierda
    int currentCombatOptionIndex;
    //ventana actual elegida
    int currentMenu;

    private void Start()
    {
        UpdateWindowsIndex();
        //PopulateAttacks("SdwBall", "Payback", "Curse", "Hypnosis");
        AttackMenu.SetActive(true);
        PopulateAttacks("SdwBall", "Payback");
        AttackMenu.SetActive(false);
    }

    void Update()
    {

        // Controlamos qué tecla se ha pulsado

        bool bUp = Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.Keypad8);

        bool bDown = Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.Keypad2);

        bool bLeft = Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.Keypad4);

        bool bRight = Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.Keypad6);

        bool bEnter = Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Joystick1Button1);

        bool bEscape = Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.Joystick1Button2);

        //dependiendo de la dirección en la que tenemos que movernos,
        //elegimos el elemento de la lista
        if (bUp)
        {
            switch (currentMenu)
            {
                case 0:
                    //Menú principal
                    if (--currentMainOptionIndex < 0) currentMainOptionIndex = MainOptions.Length - 1;
                    break;
                case 1:
                    //Menú de combate
                    if (--currentMainOptionIndex < 0) currentMainOptionIndex = CombatOptions.Length - 1;
                    break;
                default:
                    //Asumimos que estamos en el Menú principal
                    if (--currentMainOptionIndex < 0) currentMainOptionIndex = MainOptions.Length - 1;
                    break;
            }
            UpdateWindowsIndex();
        }
        else if (bDown)
        {
            switch (currentMenu)
            {
                case 0:
                    //Menú principal
                    currentMainOptionIndex = (++currentMainOptionIndex) % MainOptions.Length;
                    break;
                case 1:
                    //Menú de combate
                    currentCombatOptionIndex = (++currentCombatOptionIndex) % CombatOptions.Length;
                    break;
                default:
                    //Asumimos que estamos en el Menú principal
                    currentMainOptionIndex = (++currentMainOptionIndex) % MainOptions.Length;
                    break;
            }
            UpdateWindowsIndex();
        }
        else if (bLeft)
        {
            switch (currentMenu)
            {
                case 0:
                    //Menú principal
                    currentMainOptionIndex -= 2;
                    if (currentMainOptionIndex < 0) currentMainOptionIndex = MainOptions.Length + currentMainOptionIndex;
                    break;
                case 1:
                    //Menú de combate
                    currentCombatOptionIndex -= 2;
                    if (currentCombatOptionIndex < 0) currentCombatOptionIndex = MainOptions.Length + currentCombatOptionIndex;
                    break;
                default:
                    //Asumimos que estamos en el Menú principal
                    currentMainOptionIndex -= 2;
                    if (currentMainOptionIndex < 0) currentMainOptionIndex = MainOptions.Length + currentMainOptionIndex;
                    break;
            }
            UpdateWindowsIndex();
        }
        else if (bRight)
        {
            switch (currentMenu)
            {
                case 0:
                    //Menú principal
                    currentMainOptionIndex = (currentMainOptionIndex + 2) % MainOptions.Length;
                    break;
                case 1:
                    //Menú de combate
                    currentCombatOptionIndex = (currentCombatOptionIndex + 2) % CombatOptions.Length;
                    break;
                default:
                    //Asumimos que estamos en el Menú principal
                    currentMainOptionIndex = (currentMainOptionIndex + 2) % MainOptions.Length;
                    break;
            }
            UpdateWindowsIndex();
        }
        else if (bEnter)
        {
            switch (currentMenu)
            {
                case 0:
                    //Menú principal
                    switch (currentMainOptionIndex)
                    {
                        case 0:
                            //Hemos elegido luchar
                            AttackMenu.SetActive(true);
                            MainMenu.SetActive(false);

                            atkInfo.transform.gameObject.SetActive(true);
                            currentMenu = 1;
                            currentCombatOptionIndex = 0;
                            UpdateWindowsIndex();
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    //Menú de combate

                    break;
                default:
                    //Asumimos que estamos en el Menú principal

                    break;
            }
        }
        else if (bEscape)
        {
            if (currentMenu > 0)
            {
                --currentMenu;
                AttackMenu.SetActive(false);
                atkInfo.transform.gameObject.SetActive(false);

                MainMenu.SetActive(true);
            }
        }
    }

    void UpdateWindowsIndex()
    {
        for (int i = 0; i < MainOptions.Length; i++)
        {
            MainOptions[i].SetActive(i == currentMainOptionIndex);
        }

        if (AttackMenu.active)
        {
            for (int i = 0; i < CombatOptions.Length; i++)
            {
                CombatOptions[i].SetActive(i == currentCombatOptionIndex);
            }
        }
    }

    public void PopulateAttacks(params string[] AttackNames)
    {
       if(AttackNames.Length > CombatOptions.Length)
        {
            Debug.LogError("Se reciben más ataques de los recomendados. Se truncarán los resultados");
        }

       for (int i = 0; i < CombatOptions.Length; i++)
        {
            if(i < AttackNames.Length)
            {
                //Hay un ataque: lo rellenamos
                CombatOptions[i].SetName(AttackNames[i]);
            }
            else
            {
                //El array de ataques era menor que el de opciones. Ponemos un guión
                CombatOptions[i].SetName("-");
            }
        }

    }


}
