using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Hospital.Data.Models;
using Hospital.Data.Storage;

/// <summary>
/// Affiche la liste des environnements disponibles dans une interface scrollable
/// Permet de visualiser tous les environnements et leurs caractéristiques
/// </summary>
public class EnvironnementsListUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject Panel;
    public Transform ContentParent;
    public GameObject EnvironnementItemPrefab;
    public Button RefreshButton;
    public Button CloseButton;
    public TextMeshProUGUI TitleText;

    private List<GameObject> _instantiatedItems = new();

    void Awake()
    {
        if (Panel) Panel.SetActive(false);

        if (RefreshButton != null)
        {
            RefreshButton.onClick.AddListener(RefreshList);
        }

        if (CloseButton != null)
        {
            CloseButton.onClick.AddListener(HidePanel);
        }
    }

    /// <summary>
    /// Affiche le panel et charge la liste des environnements
    /// </summary>
    public void ShowPanel()
    {
        if (Panel == null) return;
        Panel.SetActive(true);
        RefreshList();
        MainMenuButtonsController.Instance?.OnMenuOpened();
    }

    /// <summary>
    /// Masque le panel
    /// </summary>
    public void HidePanel()
    {
        if (Panel == null) return;
        Panel.SetActive(false);
        MainMenuButtonsController.Instance?.OnMenuClosed();
    }

    /// <summary>
    /// Rafraîchit la liste des environnements
    /// </summary>
    public void RefreshList()
    {
        ClearList();

        try
        {
            var environnements = HospitalDataService.Instance.Environnements.GetAll();
            
            if (TitleText != null)
            {
                TitleText.text = $"Environnements disponibles ({environnements.Count})";
            }

            foreach (var env in environnements)
            {
                CreateEnvironnementItem(env);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Erreur lors du chargement des environnements: {ex.Message}");
        }
    }

    /// <summary>
    /// Crée un élément d'interface pour un environnement
    /// </summary>
    private void CreateEnvironnementItem(EnvironnementJson env)
    {
        if (ContentParent == null) return;

        GameObject item;

        if (EnvironnementItemPrefab != null)
        {
            item = Instantiate(EnvironnementItemPrefab, ContentParent);
        }
        else
        {
            // Créer un item simple si aucun prefab n'est fourni
            item = new GameObject($"Env_{env.IdEnvironnement}");
            item.transform.SetParent(ContentParent, false);
            
            var layout = item.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.spacing = 5;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            layout.childControlHeight = true;

            var bg = item.AddComponent<Image>();
            bg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        }

        // Remplir les informations
        var texts = item.GetComponentsInChildren<TextMeshProUGUI>();
        
        if (texts.Length > 0)
        {
            // Titre
            texts[0].text = $"<b>{env.NomEnvironnement}</b>";
            texts[0].fontSize = 18;
            texts[0].color = Color.white;
        }

        if (texts.Length > 1)
        {
            // Description
            texts[1].text = env.Description;
            texts[1].fontSize = 14;
            texts[1].color = new Color(0.8f, 0.8f, 0.8f);
        }

        if (texts.Length > 2)
        {
            // Détails
            string details = $"ID: {env.IdEnvironnement}\n" +
                           $"Positions: {string.Join(", ", env.PositionsDisponibles)}\n" +
                           $"Difficultés: {string.Join(", ", env.NiveauxDifficulte)}\n" +
                           $"Durée par défaut: {env.DureeDefaut}s";
            texts[2].text = details;
            texts[2].fontSize = 12;
            texts[2].color = new Color(0.7f, 0.7f, 0.7f);
        }

        // Si aucun TextMeshPro n'existe, créer le contenu de base
        if (texts.Length == 0)
        {
            CreateSimpleEnvironnementDisplay(item, env);
        }

        _instantiatedItems.Add(item);
    }

    /// <summary>
    /// Crée un affichage simple pour un environnement
    /// </summary>
    private void CreateSimpleEnvironnementDisplay(GameObject parent, EnvironnementJson env)
    {
        // Titre
        var titleObj = new GameObject("Title");
        titleObj.transform.SetParent(parent.transform, false);
        var titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = $"<b>{env.NomEnvironnement}</b>";
        titleText.fontSize = 18;
        titleText.color = Color.white;

        // Description
        var descObj = new GameObject("Description");
        descObj.transform.SetParent(parent.transform, false);
        var descText = descObj.AddComponent<TextMeshProUGUI>();
        descText.text = env.Description;
        descText.fontSize = 14;
        descText.color = new Color(0.8f, 0.8f, 0.8f);

        // Détails
        var detailsObj = new GameObject("Details");
        detailsObj.transform.SetParent(parent.transform, false);
        var detailsText = detailsObj.AddComponent<TextMeshProUGUI>();
        string details = $"ID: {env.IdEnvironnement}\n" +
                       $"Positions: {string.Join(", ", env.PositionsDisponibles)}\n" +
                       $"Difficultés: {string.Join(", ", env.NiveauxDifficulte)}\n" +
                       $"Durée: {env.DureeDefaut}s";
        detailsText.text = details;
        detailsText.fontSize = 12;
        detailsText.color = new Color(0.7f, 0.7f, 0.7f);
    }

    /// <summary>
    /// Nettoie la liste affichée
    /// </summary>
    private void ClearList()
    {
        foreach (var item in _instantiatedItems)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        _instantiatedItems.Clear();
    }

    private void OnDestroy()
    {
        ClearList();
    }
}
