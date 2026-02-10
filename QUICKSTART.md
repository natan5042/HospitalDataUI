# 🚀 Guide de Démarrage Rapide - Système d'Environnements

## ⚡ Démarrage en 5 minutes

### 1️⃣ Tester le système (MAINTENANT !)

1. **Ouvrez Unity** et votre projet HospitalDataUI
2. **Créez un GameObject vide** : Clic droit dans Hierarchy → Create Empty → Nommez-le "EnvironnementsTest"
3. **Ajoutez le script de test** : 
   - Sélectionnez "EnvironnementsTest"
   - Dans Inspector, cliquez "Add Component"
   - Tapez "EnvironnementsTestRunner" et ajoutez-le
4. **Testez** :
   - Lancez le mode Play ▶️
   - Cliquez sur le GameObject "EnvironnementsTest"
   - Dans Inspector, cliquez sur les boutons de test :
     - "Run All Tests" pour tout tester
     - "Test 3: Lister tous les environnements" pour voir vos environnements
     - "Afficher statistiques" pour les stats

✅ **Résultat attendu** : Vous devriez voir dans la Console :
```
6 environnements chargés
📍 Forêt Virtuelle (env-forest)
📍 Hôpital Moderne (env-hospital)
...
```

### 2️⃣ Créer l'interface UI (Pour l'utilisateur final)

#### Option A : Interface simple (Recommandé pour test)

1. **Créez un bouton de test** :
   ```
   Hierarchy → Create → UI → Button - TextMeshPro
   ```
   
2. **Ajoutez le script EnvironnementSelectionUI** :
   - Créez un GameObject vide : "EnvironnementSelector"
   - Add Component → EnvironnementSelectionUI
   
3. **Pour le moment, testez via code** :
   - Créez un script simple qui appelle `ShowForm()` sur le bouton

#### Option B : Interface complète (Pour production)

Suivez les instructions détaillées dans [ENVIRONNEMENTS_README.md](ENVIRONNEMENTS_README.md)

### 3️⃣ Utiliser dans votre code

#### Récupérer tous les environnements
```csharp
var environnements = HospitalDataService.Instance.Environnements.GetAll();
foreach (var env in environnements)
{
    Debug.Log($"{env.NomEnvironnement} - {env.Description}");
}
```

#### Récupérer un environnement spécifique
```csharp
var foret = HospitalDataService.Instance.Environnements.GetById("env-forest");
if (foret != null)
{
    Debug.Log($"Trouvé : {foret.NomEnvironnement}");
}
```

#### Créer une configuration pour une session
```csharp
var config = new ConfigurationEnvironnement
{
    IdEnvironnement = "env-forest",
    PositionDepart = "assis",
    NiveauDifficulte = "moyen",
    NiveauAssistance = 2,
    Duree = 600
};

// Utilisez cette config pour démarrer votre session VR
```

## 📦 Fichiers créés

| Fichier | Description | Emplacement |
|---------|-------------|-------------|
| `environnements.json` | 6 environnements de test | `Assets/HospitalData/StreamingAssets/` |
| `EnvironnementsManager.cs` | Gestionnaire de données | `Assets/HospitalData/Storage/` |
| `EnvironnementSelectionUI.cs` | Interface de sélection | `Assets/Scripts/HospitalUI/` |
| `EnvironnementsListUI.cs` | Liste des environnements | `Assets/Scripts/HospitalUI/` |
| `SessionLauncher.cs` | Helper pour lancer sessions | `Assets/Scripts/HospitalUI/` |
| `EnvironnementsTestRunner.cs` | Script de test | `Assets/Scripts/HospitalUI/` |

## 🎮 Les 6 environnements de test

| Nom | ID | Idéal pour |
|-----|----|-----------| 
| 🌲 Forêt Virtuelle | env-forest | Relaxation, extérieur calme |
| 🏥 Hôpital Moderne | env-hospital | Simulation réaliste |
| 🏙️ Centre Ville | env-city | Attention, distractions |
| 🏖️ Plage Relaxante | env-beach | Apaisement, réduction stress |
| 🚪 Salle Neutre | env-room | Concentration maximale |
| 🔧 Environnement Test | env-test | Tests rapides (60s) |

## 🔧 Vérification rapide

### ✅ Checklist avant utilisation

- [ ] Les fichiers JSON sont dans `Assets/HospitalData/StreamingAssets/`
- [ ] Le fichier `environnements.json` existe et contient 6 environnements
- [ ] Les scripts sont dans `Assets/Scripts/HospitalUI/` et `Assets/HospitalData/Storage/`
- [ ] Aucune erreur de compilation
- [ ] Le test "Run All Tests" fonctionne en mode Play

### 🐛 Problèmes courants

**Problème** : "Aucun environnement chargé"
- ✅ **Solution** : Vérifiez que `environnements.json` est dans le bon dossier

**Problème** : "Environnement non trouvé"
- ✅ **Solution** : Vérifiez l'ID (sensible à la casse) : `env-forest` pas `ENV-FOREST`

**Problème** : "Erreur de sérialisation JSON"
- ✅ **Solution** : Vérifiez la syntaxe JSON avec un validateur en ligne

## 🎯 Prochaines étapes

1. ✅ **Testez le système** (fait dans l'étape 1)
2. 🔄 **Créez votre interface UI** personnalisée
3. 🎮 **Intégrez avec votre système VR** :
   - Modifier `SessionLauncher.cs` → `StartVREnvironment()`
   - Charger la scène VR appropriée
   - Appliquer la configuration (position, difficulté, etc.)
4. 📊 **Enregistrez les résultats** des sessions
5. 🎨 **Personnalisez** : Ajoutez vos propres environnements

## 💡 Exemples d'utilisation

### Scénario 1 : Session simple
```csharp
// 1. Récupérer un environnement
var env = HospitalDataService.Instance.Environnements.GetById("env-forest");

// 2. Créer la configuration
var config = new ConfigurationEnvironnement
{
    IdEnvironnement = env.IdEnvironnement,
    PositionDepart = "assis",
    NiveauDifficulte = "facile",
    NiveauAssistance = 3,
    Duree = 300
};

// 3. Créer la session
var session = new SessionJson
{
    IDpatient = "patient-001",
    EnvironnementUtilise = config.IdEnvironnement,
    PositionDepart = config.PositionDepart,
    niveauDifficulte = config.NiveauDifficulte,
    NiveauAssistance_moyen = config.NiveauAssistance,
    duree = config.Duree,
    DateDebut = DateTime.UtcNow
};

// 4. Sauvegarder
HospitalDataService.Instance.Sessions.Add(session);
```

### Scénario 2 : Lister pour un dropdown
```csharp
var dropdown = GetComponent<TMP_Dropdown>();
dropdown.ClearOptions();

var environnements = HospitalDataService.Instance.Environnements.GetAll();
var options = environnements
    .Select(e => new TMP_Dropdown.OptionData(e.NomEnvironnement))
    .ToList();
    
dropdown.AddOptions(options);
```

## 📞 Support

- 📖 Documentation complète : `ENVIRONNEMENTS_README.md`
- 🧪 Tests automatiques : Utilisez `EnvironnementsTestRunner`
- 💬 Questions ? Regardez les commentaires dans les scripts

## 🎉 C'est tout !

Votre système d'environnements est prêt à être utilisé. Commencez par les tests, puis construisez votre interface UI selon vos besoins.

**Bon développement ! 🚀**
