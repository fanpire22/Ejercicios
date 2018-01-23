using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShooterCharacter : Damageable
{
    //Array de armas disponibles
    [SerializeField] private WeaponBase[] _weapons;

    [SerializeField] GameObject _pauseMenu;
    [SerializeField] Text _hudAmmo;
    [SerializeField] Image _icono;


    //Arma seleccionada actual
    private WeaponBase _currentW;

    //Indice actual del arma en uso
    private int _currentI = 0;

    protected override void Start()
    {
        base.Start();
        _currentW = _weapons[_currentI];
        UpdateAmmo();
    }

    private void UpdateAmmo()
    {

        _hudAmmo.text = string.Format("{0}\n{1}", _currentW.GetCurrentAmmo(), _currentW.GetMaxAmmo());
    }

    private void Update()
    {
        if (MainMenu.GamePause) return;
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        //Hemos movido lo suficiente la rueda como para elegir un arma
        if (Mathf.Abs(mouseWheel) > 0.075f) SelectWeapon(mouseWheel);

        //Disparamos
        if (Input.GetButton("Fire1") && _currentW)
        {
            _currentW.Shoot();
            UpdateAmmo();
        }

        //Acción secundaria
        if (Input.GetButton("Fire2") && _currentW) _currentW.SecondAction();

        //Pausa (escape)
        if (Input.GetButton("Pause")) ShowMenu(true);
    }

    private void OnGUI()
    {
        if (MainMenu.GamePause) return;
        //obtenemos la tecla pulsada en código ascii, y le restamos el valor a partir del que empezamos a contar el valor 1
        int ascii = Event.current.character;
        ascii = ascii - 49;

        //Hemos pulsado un número
        if (ascii >= 0 && ascii <= 9) SelectWeaponAtIndex(ascii, 0);
    }

    /// <summary>
    /// Elegimos el arma ubicada en un índice concreto
    /// </summary>
    /// <param name="index">número del array específico.</param>
    private void SelectWeaponAtIndex(int index, float direction)
    {
        //Estamos eligiendo un arma no válida, salimos de la función
        if (index >= _weapons.Length)
            return;

        

        if (direction == 0)
            //intentamos elegir un arma que no tenemos por su número
            if (!_weapons[index].bInventory) return;
            else if (direction > 0)
            {
                //intentamos elegir un arma que no tenemos en dirección ascendente
                if (!_weapons[index].bInventory)
                {
                    SelectWeaponAtIndex(index + 1, direction);
                }
            }
            else
            {
                //intentamos elegir un arma que no tenemos en dirección descendente
                if (!_weapons[index].bInventory)
                {
                    if (index < 1)
                    { SelectWeaponAtIndex(_weapons.Length - 1, direction); }
                    else
                    { SelectWeaponAtIndex(index - 1, direction); };
                }
            }

        //Desactivamos el arma sacada
        _currentW.gameObject.SetActive(false);

        //Seleccionamos el arma correspondiente a nuestro array y la activamos
        _currentW = _weapons[index];
        _currentW.gameObject.SetActive(true);
        _icono.sprite = _currentW.imagen;

        _currentI = index;
        UpdateAmmo();
    }

    /// <summary>
    /// Elegimos el arma siguiente o anterior dependiendo de la rueda del ratón
    /// </summary>
    /// <param name="direction"></param>
    private void SelectWeapon(float direction)
    {
        if (direction > 0)
        {
            //Escogemos la siguiente arma. Lo hacemos con el módulo, ya que si llegamos al valor de nuestro largo, 
            //el resto daría un cero, y si no llegamos, nos quedamos con el nuevo valor
            _currentI = ++_currentI % _weapons.Length;
        }
        else
        {
            //Escogemos el arma anterior
            if (--_currentI < 0)
                _currentI = _weapons.Length - 1;
        }

        SelectWeaponAtIndex(_currentI, direction);

    }

    /// <summary>
    /// Mostramos u ocultamos el menú de pausa
    /// <param name="IsVisible">Determina si se muestra la ventana de pausa u ocultarla</param>
    /// </summary>
    public void ShowMenu(bool IsVisible)
    {
        MainMenu.GamePause = IsVisible;
        Time.timeScale = IsVisible ? 0 : 1;
        Cursor.visible = IsVisible;
        Cursor.lockState = IsVisible ? CursorLockMode.None : CursorLockMode.Locked;
        _pauseMenu.SetActive(IsVisible);
    }

    /// <summary>
    /// Función que nos devuelve al menú principal
    /// </summary>
    public void ReturnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Se nos ha muerto el personaje: volvemos al menú principal
    /// </summary>
    protected override void OnDead()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ReturnMainMenu();
    }

    public bool AddAmmo(int index, int amount)
    {
        bool haCambiado = false;
        if (_weapons[index].bInventory)
        {
            haCambiado = _weapons[index].AddAmmo(amount);
            UpdateAmmo();
        }
        return haCambiado;
    }

    public void AddWeapon(int index)
    {
        _weapons[index].bInventory = true;
    }

}
