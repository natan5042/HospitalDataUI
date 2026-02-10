using UnityEngine;
using System;
using Hospital.Data.Models;
using Hospital.Data.Storage;

/// <summary>
/// Helper pour démarrer une session VR avec une configuration d'environnement
/// Simplifie le processus de création et lancement de sessions
/// </summary>
public class SessionLauncher : MonoBehaviour
{
    [Header("Configuration")]
    public string PatientId = "patient-001";
    public string SupervisorId = "";

    [Header("Références UI")]
    public EnvironnementSelectionUI EnvironnementUI;

    /// <summary>
    /// Démarre une nouvelle session avec la configuration d'environnement actuelle
    /// </summary>
    public void StartNewSession()
    {
        if (EnvironnementUI == null)
        {
            Debug.LogError("EnvironnementSelectionUI non assigné !");
            return;
        }

        var config = EnvironnementUI.GetConfiguration();
        if (config == null)
        {
            Debug.LogError("Aucune configuration d'environnement sélectionnée !");
            return;
        }

        // Vérifier que le patient existe
        var patient = HospitalDataService.Instance.Patients.GetAll();
        bool patientExists = false;
        foreach (var p in patient)
        {
            if (p.IDpatient == PatientId)
            {
                patientExists = true;
                break;
            }
        }

        if (!patientExists)
        {
            Debug.LogError($"Patient {PatientId} n'existe pas !");
            return;
        }

        try
        {
            // Créer la session
            var session = CreateSession(config);
            
            // Enregistrer la session
            var savedSession = HospitalDataService.Instance.Sessions.Add(session);
            
            Debug.Log($"Session créée avec succès ! Environnement: {config.IdEnvironnement}");
            
            // TODO: Ici vous pouvez charger la scène VR ou démarrer le jeu
            // SceneManager.LoadScene("VRScene");
            // ou
            // StartVREnvironment(config);
            
            OnSessionStarted(savedSession);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erreur lors de la création de la session : {ex.Message}");
        }
    }

    /// <summary>
    /// Crée un objet SessionJson à partir de la configuration
    /// </summary>
    private SessionJson CreateSession(ConfigurationEnvironnement config)
    {
        return new SessionJson
        {
            IDpatient = PatientId,
            EnvironnementUtilise = config.IdEnvironnement,
            PositionDepart = config.PositionDepart,
            niveauDifficulte = config.NiveauDifficulte,
            NiveauAssistance_moyen = config.NiveauAssistance,
            duree = config.Duree,
            DateDebut = DateTime.UtcNow,
            IdSuperviseur = SupervisorId,
            
            // Valeurs par défaut (à remplir pendant/après la session)
            ObjectifsAtteints = "",
            ObjectifsManques = "",
            ScoreTotal = 0,
            TempsReaction = 0.0,
            PrecisionPointage = 0.0,
            Commentaire = $"Session démarrée à {DateTime.Now:HH:mm:ss}"
        };
    }

    /// <summary>
    /// Appelé quand une session démarre (à override ou connecter à un event)
    /// </summary>
    protected virtual void OnSessionStarted(SessionJson session)
    {
        Debug.Log($"=== Session démarrée ===");
        Debug.Log($"Patient: {session.IDpatient}");
        Debug.Log($"Environnement: {session.EnvironnementUtilise}");
        Debug.Log($"Position: {session.PositionDepart}");
        Debug.Log($"Difficulté: {session.niveauDifficulte}");
        Debug.Log($"Assistance: {session.NiveauAssistance_moyen}");
        Debug.Log($"Durée prévue: {session.duree}s");
        Debug.Log($"=======================");
    }

    /// <summary>
    /// Définit le patient pour la prochaine session
    /// </summary>
    public void SetPatient(string patientId)
    {
        PatientId = patientId;
    }

    /// <summary>
    /// Définit le superviseur pour la prochaine session
    /// </summary>
    public void SetSupervisor(string supervisorId)
    {
        SupervisorId = supervisorId;
    }

    /// <summary>
    /// Exemple de méthode pour démarrer l'environnement VR
    /// À implémenter selon votre système VR
    /// </summary>
    public void StartVREnvironment(ConfigurationEnvironnement config)
    {
        var env = HospitalDataService.Instance.Environnements.GetById(config.IdEnvironnement);
        if (env == null)
        {
            Debug.LogError($"Environnement {config.IdEnvironnement} introuvable !");
            return;
        }

        Debug.Log($"Démarrage de l'environnement VR: {env.NomEnvironnement}");
        
        // TODO: Implémenter le chargement de l'environnement VR
        // Exemples :
        // - Charger une scène Unity spécifique
        // - Configurer les paramètres de difficulté
        // - Positionner le joueur selon PositionDepart
        // - Activer les assistances selon NiveauAssistance
        // - Démarrer un timer de durée
    }

    /// <summary>
    /// Exemple de méthode pour terminer une session et sauvegarder les résultats
    /// </summary>
    public void EndSession(int scoreTotal, float tempsReaction, float precisionPointage, 
                          string objectifsAtteints, string objectifsManques, string commentaire)
    {
        // Cette méthode serait appelée à la fin de la session VR
        // pour mettre à jour les résultats
        
        Debug.Log($"Session terminée - Score: {scoreTotal}");
        
        // TODO: Mettre à jour la session dans la base de données
        // Vous devrez implémenter une méthode pour trouver et mettre à jour
        // la session en cours
    }
}
