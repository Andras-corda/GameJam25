using UnityEngine;

public class GestionPause : MonoBehaviour
{
    private bool estEnPause = false;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject HUDRef;
    [SerializeField] private GameObject EndMenu;

    private void Start()
    {
        Time.timeScale = 1f;
        estEnPause = false;

        PauseMenu.SetActive(false);
        HUDRef.SetActive(true);
        EndMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estEnPause)
            {
                ReprendreJeu();
            }
            else
            {
                MettreEnPause();
            }
        }
    }

    public void MettreEnPause()
    {
        Time.timeScale = 0f;
        estEnPause = true;

        PauseMenu.SetActive(true);
        HUDRef.SetActive(false);
        // Optionnel : Afficher un menu UI ici (ex: menuPause.SetActive(true);)
    }

    public void ReprendreJeu()
    {
        Time.timeScale = 1f;
        estEnPause = false;

        PauseMenu.SetActive(false);
        HUDRef.SetActive(true);
        // Optionnel : Masquer le menu UI ici (ex: menuPause.SetActive(false);)
    }

    public void ShowEndMenu()
    {
        EndMenu.SetActive(true);
        Time.timeScale = 0f;
        estEnPause = true;
    }
}
