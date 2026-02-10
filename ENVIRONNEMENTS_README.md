# Système de Gestion d'Environnements VR

Ce système permet de gérer les environnements virtuels disponibles pour les sessions de rééducation.

## 📁 Fichiers créés

### Modèles de données
- **JsonRecords.cs** - Ajout de 3 nouvelles classes :
  - `EnvironnementJson` : Représente un environnement avec ses configurations
  - `EnvironnementEnvelope` : Conteneur pour la sérialisation JSON
  - `ConfigurationEnvironnement` : Configuration choisie pour une session

### Gestionnaire de données
- **EnvironnementsManager.cs** : Gestionnaire pour charger/sauvegarder les environnements
  - Méthodes : `GetAll()`, `GetById()`, `Add()`, `Update()`, `Remove()`, `Reload()`

### Fichier de données
- **environnements.json** : Contient 6 environnements de test :
  1. **Forêt Virtuelle** (env-forest)
  2. **Hôpital Moderne** (env-hospital)
  3. **Centre Ville** (env-city)
  4. **Plage Relaxante** (env-beach)
  5. **Salle Neutre** (env-room)
  6. **Environnement Test** (env-test)

### Interfaces utilisateur
- **EnvironnementSelectionUI.cs** : Interface pour choisir et configurer un environnement
- **EnvironnementsListUI.cs** : Interface pour visualiser tous les environnements disponibles

## 🎮 Comment utiliser

### 1. Dans Unity Editor

#### Créer l'interface de sélection d'environnement

1. **Créer un Canvas** si vous n'en avez pas déjà un
2. **Créer un Panel** pour le formulaire de sélection
3. **Ajouter le script** `EnvironnementSelectionUI` sur un GameObject
4. **Ajouter les éléments UI** suivants dans le Panel :
   - 1 Dropdown (TMP) pour la sélection d'environnement
   - 1 Dropdown (TMP) pour la position de départ
   - 1 Dropdown (TMP) pour le niveau de difficulté
   - 1 InputField (TMP) pour la durée
   - 1 Slider pour le niveau d'assistance (0-5)
   - 1 TextMeshProUGUI pour afficher la description
   - 1 TextMeshProUGUI pour le label du niveau d'assistance
   - 1 TextMeshProUGUI pour les messages de statut
   - 2 Buttons : "Démarrer" et "Annuler"

5. **Assigner les références** dans l'Inspector du script `EnvironnementSelectionUI`

6. **Créer un bouton** dans votre menu principal qui appelle `ShowForm()` sur le script

#### Créer l'interface de liste des environnements

1. **Créer un Panel** pour la liste
2. **Ajouter un ScrollView** dans ce panel
3. **Ajouter le script** `EnvironnementsListUI` sur un GameObject
4. **Assigner les références** dans l'Inspector
5. **Créer un bouton** qui appelle `ShowPanel()` pour afficher la liste

### 2. Dans le code

#### Accéder aux environnements

```csharp
using Hospital.Data.Storage;
using Hospital.Data.Models;

// Récupérer tous les environnements
var environnements = HospitalDataService.Instance.Environnements.GetAll();

// Récupérer un environnement spécifique
var foret = HospitalDataService.Instance.Environnements.GetById("env-forest");

// Ajouter un nouvel environnement
var nouvelEnv = new EnvironnementJson
{
    IdEnvironnement = "env-custom",
    NomEnvironnement = "Mon Environnement",
    Description = "Description personnalisée",
    PositionsDisponibles = new List<string> { "assis", "debout" },
    NiveauxDifficulte = new List<string> { "facile", "moyen" },
    DureeDefaut = 300
};
HospitalDataService.Instance.Environnements.Add(nouvelEnv);
```

#### Récupérer la configuration choisie

```csharp
// Depuis un autre script, récupérer la configuration
var selectionUI = FindObjectOfType<EnvironnementSelectionUI>();
var config = selectionUI.GetConfiguration();

if (config != null)
{
    Debug.Log($"Environnement: {config.IdEnvironnement}");
    Debug.Log($"Position: {config.PositionDepart}");
    Debug.Log($"Difficulté: {config.NiveauDifficulte}");
    Debug.Log($"Assistance: {config.NiveauAssistance}");
    Debug.Log($"Durée: {config.Duree}s");
}
```

#### Créer une session avec l'environnement configuré

```csharp
public void StartVRSession(ConfigurationEnvironnement config, string patientId)
{
    var session = new SessionJson
    {
        IDpatient = patientId,
        EnvironnementUtilise = config.IdEnvironnement,
        PositionDepart = config.PositionDepart,
        niveauDifficulte = config.NiveauDifficulte,
        NiveauAssistance_moyen = config.NiveauAssistance,
        duree = config.Duree,
        DateDebut = DateTime.UtcNow,
        // ... autres champs
    };
    
    HospitalDataService.Instance.Sessions.Add(session);
}
```

## 📋 Structure des données

### Environnement (EnvironnementJson)

```json
{
  "IdEnvironnement": "env-forest",
  "NomEnvironnement": "Forêt Virtuelle",
  "Description": "Un environnement forestier calme...",
  "PositionsDisponibles": ["assis", "debout", "assise-centre"],
  "NiveauxDifficulte": ["facile", "moyen", "difficile"],
  "DureeDefaut": 600,
  "ImagePath": "Environments/forest.png"
}
```

### Configuration (ConfigurationEnvironnement)

```json
{
  "IdEnvironnement": "env-forest",
  "PositionDepart": "assis",
  "NiveauDifficulte": "moyen",
  "NiveauAssistance": 2,
  "Duree": 600
}
```

## 🎯 Environnements de test inclus

| ID | Nom | Positions | Difficultés | Durée |
|----|-----|-----------|-------------|-------|
| env-forest | Forêt Virtuelle | assis, debout, assise-centre | facile, moyen, difficile | 600s |
| env-hospital | Hôpital Moderne | assis, debout, allonge | facile, moyen, difficile | 900s |
| env-city | Centre Ville | assis, debout | moyen, difficile, expert | 720s |
| env-beach | Plage Relaxante | assis, debout, assise-centre | facile, moyen | 480s |
| env-room | Salle Neutre | assis, debout, allonge, assise-centre | facile, moyen, difficile | 300s |
| env-test | Environnement Test | assis, debout | facile | 60s |

## 🔧 Personnalisation

### Ajouter un nouvel environnement

Vous pouvez ajouter directement dans le fichier `environnements.json` ou via le code :

```csharp
var env = new EnvironnementJson
{
    IdEnvironnement = "env-montagne",
    NomEnvironnement = "Montagne Enneigée",
    Description = "Environnement montagnard pour exercices en altitude",
    PositionsDisponibles = new List<string> { "assis", "debout" },
    NiveauxDifficulte = new List<string> { "moyen", "difficile", "expert" },
    DureeDefaut = 900,
    ImagePath = "Environments/mountain.png"
};

HospitalDataService.Instance.Environnements.Add(env);
```

### Positions disponibles

- `assis` : Position assise standard
- `debout` : Position debout
- `allonge` : Position allongée
- `assise-centre` : Position assise au centre

### Niveaux de difficulté

- `facile` : Pour débutants ou récupération douce
- `moyen` : Difficulté standard
- `difficile` : Challenge avancé
- `expert` : Niveau expert (uniquement Centre Ville)

## 🚀 Intégration avec les sessions

Le système est intégré avec le gestionnaire de sessions existant. Chaque session peut maintenant référencer :
- L'environnement utilisé via `EnvironnementUtilise`
- La position de départ via `PositionDepart`
- Le niveau de difficulté via `niveauDifficulte`
- Le niveau d'assistance via `NiveauAssistance_moyen`

## 📝 Notes

- Les fichiers JSON sont stockés dans `Assets/HospitalData/StreamingAssets/`
- Le système utilise le même pattern que les patients et superviseurs
- Thread-safe grâce au `ReaderWriterLockSlim`
- Les données sont chargées en lazy loading (au premier accès)

## ✅ Tests

Pour tester le système :

1. Lancez Unity
2. Ouvrez la scène principale
3. Cliquez sur le bouton "Sélectionner Environnement"
4. Choisissez un environnement dans le dropdown
5. Configurez les paramètres (position, difficulté, durée, assistance)
6. Cliquez sur "Démarrer"
7. La configuration s'affiche dans la zone de statut

Pour voir la liste des environnements :
1. Cliquez sur "Liste des Environnements"
2. Tous les environnements s'affichent avec leurs détails
