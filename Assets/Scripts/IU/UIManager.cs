using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private PauseMenu _pauseMenu;

    [SerializeField]
    private PlayerControls _inputActions;

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        _inputActions.UI.Enable();
        _inputActions.UI.PauseMenu.performed += PauseMenu_performed;
    }

    private void OnDisable()
    {
        _inputActions.UI.PauseMenu.performed -= PauseMenu_performed;
        _inputActions.UI.Disable();
    }

    private void PauseMenu_performed(InputAction.CallbackContext obj)
    {
        _pauseMenu.gameObject.SetActive(true);
    }

    #endregion

}
