# 🎯 Guide d'Implémentation Unity - Système d'Environnements
## Étapes complètes pour intégrer le système dans votre projet

---

## 📋 PARTIE 1 : VÉRIFICATION DES FICHIERS (5 minutes)

### Étape 1.1 : Vérifier les scripts C#
✅ Ouvrez Unity et vérifiez que ces fichiers existent et compilent sans erreur :

**Dans Assets/HospitalData/Models/** :
- ✓ `JsonRecords.cs` (modifié avec les nouvelles classes)

**Dans Assets/HospitalData/Storage/** :
- ✓ `EnvironnementsManager.cs` (nouveau)
- ✓ `HospitalDataService.cs` (modifié)

**Dans Assets/Scripts/HospitalUI/** :
- ✓ `EnvironnementSelectionUI.cs` (nouveau)
- ✓ `EnvironnementsListUI.cs` (nouveau)
- ✓ `SessionLauncher.cs` (nouveau)
- ✓ `EnvironnementsTestRunner.cs` (nouveau)

### Étape 1.2 : Vérifier le fichier JSON
✅ Vérifiez que ce fichier existe :

**Dans Assets/HospitalData/StreamingAssets/** :
- ✓ `environnements.json`

**ACTION** : Ouvrez le fichier et vérifiez qu'il contient 6 environnements

---

## 🧪 PARTIE 2 : TEST DU SYSTÈME (10 minutes)

### Étape 2.1 : Créer un GameObject de test

1. Dans Unity, **Hierarchy** → Clic droit → **Create Empty**
2. Nommez-le : `EnvironnementsTestRunner`
3. Sélectionnez ce GameObject
4. Dans **Inspector** → **Add Component**
5. Cherchez : `EnvironnementsTestRunner`
6. Cliquez pour l'ajouter

### Étape 2.2 : Lancer les tests

1. **Cliquez sur le bouton Play** ▶️ en haut de Unity
2. Sélectionnez le GameObject `EnvironnementsTestRunner`
3. Dans **Inspector**, vous verrez plusieurs boutons :
   - **Run All Tests**
   - **Test 1: Charger les environnements**
   - **Test 2: Récupérer un environnement par ID**
   - etc.

4. **Cliquez sur "Run All Tests"**

### Étape 2.3 : Vérifier les résultats dans la Console

Ouvrez la fenêtre **Console** (Window → General → Console)

✅ **Vous devriez voir** :
```
=== DÉBUT DES TESTS ENVIRONNEMENTS ===

--- Test 1: Chargement des environnements ---
✓ 6 environnements chargés

--- Test 2: Récupération par ID ---
✓ Environnement trouvé : Forêt Virtuelle
  Description : Un environnement forestier calme...

--- Test 3: Liste complète des environnements ---
📍 Forêt Virtuelle (env-forest)
📍 Hôpital Moderne (env-hospital)
...

=== FIN DES TESTS ===
```

❌ **Si vous voyez des erreurs** :
- Vérifiez que `environnements.json` est dans le bon dossier
- Vérifiez qu'il n'y a pas d'erreurs de compilation

5. **Arrêtez le mode Play** ⏹️

---

## 🎨 PARTIE 3 : CRÉER L'INTERFACE UI DE SÉLECTION (30 minutes)

### Étape 3.1 : Créer le Canvas principal (si vous n'en avez pas)

1. **Hierarchy** → Clic droit → **UI** → **Canvas**
2. Nommez-le : `MainCanvas`
3. Sélectionnez `MainCanvas`
4. Dans **Inspector** :
   - Canvas Scaler → UI Scale Mode : **Scale With Screen Size**
   - Reference Resolution : **1920 x 1080**

### Étape 3.2 : Créer le Panel du formulaire

1. Clic droit sur `MainCanvas` → **UI** → **Panel**
2. Nommez-le : `EnvironnementSelectionPanel`
3. Dans **Inspector** (RectTransform) :
   - Anchor Presets : Cliquez sur le carré en haut à gauche
   - Maintenez **Alt + Shift** et cliquez sur **Center** (milieu)
   - Width : `800`
   - Height : `600`
4. Dans **Inspector** (Image component) :
   - Color : Noir avec Alpha à `200` (semi-transparent)

### Étape 3.3 : Créer le titre

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Text - TextMeshPro**
2. Nommez-le : `TitleText`
3. Configuration :
   - Text : `Sélection d'Environnement`
   - Font Size : `32`
   - Alignment : Centré
   - Color : Blanc
4. RectTransform :
   - Pos X : `0`, Pos Y : `250`
   - Width : `700`, Height : `50`

### Étape 3.4 : Créer le Dropdown Environnement

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Dropdown - TextMeshPro**
2. Nommez-le : `EnvironnementDropdown`
3. RectTransform :
   - Pos X : `0`, Pos Y : `180`
   - Width : `600`, Height : `40`
4. Dans le component Dropdown :
   - Supprimez les options par défaut (Option A, B, C)

### Étape 3.5 : Créer le texte Description

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Text - TextMeshPro**
2. Nommez-le : `DescriptionText`
3. Configuration :
   - Font Size : `16`
   - Alignment : Centré, Top
   - Color : Gris clair (200, 200, 200)
   - Enable Word Wrapping : ✓
4. RectTransform :
   - Pos X : `0`, Pos Y : `110`
   - Width : `700`, Height : `80`

### Étape 3.6 : Créer le Dropdown Position

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Dropdown - TextMeshPro**
2. Nommez-le : `PositionDropdown`
3. RectTransform :
   - Pos X : `-200`, Pos Y : `20`
   - Width : `280`, Height : `40`

### Étape 3.7 : Créer le Dropdown Difficulté

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Dropdown - TextMeshPro**
2. Nommez-le : `DifficulteDropdown`
3. RectTransform :
   - Pos X : `200`, Pos Y : `20`
   - Width : `280`, Height : `40`

### Étape 3.8 : Créer l'InputField Durée

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Input Field - TextMeshPro**
2. Nommez-le : `DureeInputField`
3. Configuration :
   - Placeholder Text : `Durée (secondes)`
   - Content Type : **Integer Number**
4. RectTransform :
   - Pos X : `-200`, Pos Y : `-50`
   - Width : `280`, Height : `40`

### Étape 3.9 : Créer le Slider Assistance

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Slider**
2. Nommez-le : `NiveauAssistanceSlider`
3. Configuration dans Inspector :
   - Min Value : `1`
   - Max Value : `10`
   - Whole Numbers : ✓
   - Value : `1`
4. RectTransform :
   - Pos X : `200`, Pos Y : `-50`
   - Width : `280`, Height : `20`

### Étape 3.10 : Créer le Label du Slider

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Text - TextMeshPro**
2. Nommez-le : `NiveauAssistanceLabel`
3. Configuration :
   - Text : `Niveau d'assistance: 1`
   - Font Size : `14`
   - Alignment : Centré
4. RectTransform :
   - Pos X : `200`, Pos Y : `-80`
   - Width : `280`, Height : `30`

### Étape 3.11 : Créer le texte Status

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Text - TextMeshPro**
2. Nommez-le : `StatusText`
3. Configuration :
   - Font Size : `14`
   - Alignment : Centré
   - Color : Blanc
   - Enable Word Wrapping : ✓
4. RectTransform :
   - Pos X : `0`, Pos Y : `-150`
   - Width : `700`, Height : `100`

### Étape 3.12 : Créer le bouton Démarrer

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Button - TextMeshPro**
2. Nommez-le : `StartButton`
3. Configuration :
   - Le texte du bouton : `DÉMARRER`
   - Font Size : `18`
   - Button Color : Vert (0, 200, 0)
4. RectTransform :
   - Pos X : `-100`, Pos Y : `-260`
   - Width : `150`, Height : `50`

### Étape 3.13 : Créer le bouton Annuler

1. Clic droit sur `EnvironnementSelectionPanel` → **UI** → **Button - TextMeshPro**
2. Nommez-le : `CancelButton`
3. Configuration :
   - Le texte du bouton : `ANNULER`
   - Font Size : `18`
   - Button Color : Rouge (200, 0, 0)
4. RectTransform :
   - Pos X : `100`, Pos Y : `-260`
   - Width : `150`, Height : `50`

---

## 🔗 PARTIE 4 : CONNECTER LE SCRIPT À L'UI (15 minutes)

### Étape 4.1 : Créer le GameObject Controller

1. **Hierarchy** → Clic droit → **Create Empty**
2. Nommez-le : `EnvironnementSelectionController`
3. Sélectionnez ce GameObject
4. **Inspector** → **Add Component**
5. Cherchez : `EnvironnementSelectionUI`
6. Ajoutez-le

### Étape 4.2 : Assigner les références dans l'Inspector

Sélectionnez `EnvironnementSelectionController` et dans le script `EnvironnementSelectionUI` :

**Drag & Drop depuis Hierarchy vers Inspector :**

1. **Form Panel** ← `EnvironnementSelectionPanel`
2. **Environnement Dropdown** ← `EnvironnementDropdown`
3. **Position Dropdown** ← `PositionDropdown`
4. **Difficulte Dropdown** ← `DifficulteDropdown`
5. **Duree Input Field** ← `DureeInputField`
6. **Niveau Assistance Slider** ← `NiveauAssistanceSlider`
7. **Niveau Assistance Label** ← `NiveauAssistanceLabel`
8. **Description Text** ← `DescriptionText`
9. **Status Text** ← `StatusText`
10. **Start Button** ← `StartButton`
11. **Cancel Button** ← `CancelButton`

### Étape 4.3 : Configurer les boutons

**Pour le bouton CancelButton :**
1. Sélectionnez `CancelButton` dans Hierarchy
2. Dans Inspector, trouvez le component **Button**
3. Dans la section **On Click ()** :
   - Cliquez sur **+**
   - Drag & Drop `EnvironnementSelectionController` dans le champ Object
   - Dans le menu déroulant : **EnvironnementSelectionUI** → **HideForm**

**Pour le bouton StartButton :**
1. Sélectionnez `StartButton`
2. Dans **On Click ()** :
   - Cliquez sur **+**
   - Drag & Drop `EnvironnementSelectionController`
   - Menu : **EnvironnementSelectionUI** → **OnStartSession**

---

## 🎮 PARTIE 5 : CRÉER UN BOUTON D'ACCÈS DANS LE MENU (10 minutes)

### Étape 5.1 : Créer un bouton dans votre menu principal

1. Dans votre Canvas principal (ou là où vous avez vos autres boutons)
2. **Clic droit** → **UI** → **Button - TextMeshPro**
3. Nommez-le : `SelectEnvironnementButton`
4. Texte du bouton : `Sélectionner Environnement`
5. Positionnez-le où vous voulez dans votre menu

### Étape 5.2 : Connecter le bouton

1. Sélectionnez `SelectEnvironnementButton`
2. Dans **On Click ()** :
   - Cliquez sur **+**
   - Drag & Drop `EnvironnementSelectionController`
   - Menu : **EnvironnementSelectionUI** → **ShowForm**

### Étape 5.3 : Masquer le panel au démarrage

1. Sélectionnez `EnvironnementSelectionPanel`
2. En haut de l'Inspector, **décochez la case** à côté du nom
   - Cela rendra le panel inactif au démarrage

---

## ✅ PARTIE 6 : TEST FINAL (5 minutes)

### Étape 6.1 : Test de l'interface

1. **Cliquez sur Play** ▶️
2. Le panel de sélection doit être caché
3. **Cliquez sur "Sélectionner Environnement"**
4. Le panel doit apparaître avec :
   - Les 6 environnements dans le dropdown
   - Une description qui change quand vous sélectionnez un environnement
   - Les positions disponibles
   - Les difficultés disponibles
   - Un champ durée pré-rempli
   - Un slider d'assistance

### Étape 6.2 : Test de configuration

1. Sélectionnez "Forêt Virtuelle"
2. Choisissez une position
3. Choisissez une difficulté
4. Ajustez le niveau d'assistance
5. **Cliquez sur DÉMARRER**
6. Vérifiez la **Console** : un message de configuration doit apparaître
7. **Cliquez sur ANNULER** pour fermer

---

## 🚀 PARTIE 7 : INTÉGRATION AVEC VOS SESSIONS (BONUS)

### Étape 7.1 : Créer le SessionLauncher

Si vous voulez enregistrer automatiquement les sessions :

1. Sélectionnez `EnvironnementSelectionController` (ou créez un nouveau GameObject)
2. **Add Component** → `SessionLauncher`
3. Dans l'Inspector du SessionLauncher :
   - **Patient Id** : Entrez l'ID d'un patient existant (ex: `patient-001`)
   - **Environnement UI** : Drag & Drop le GameObject qui a le script `EnvironnementSelectionUI`

### Étape 7.2 : Modifier le bouton Démarrer pour lancer une session

1. Sélectionnez `StartButton`
2. Dans **On Click ()** :
   - Ajoutez un deuxième événement **+**
   - Drag & Drop le GameObject avec `SessionLauncher`
   - Menu : **SessionLauncher** → **StartNewSession**

Maintenant quand l'utilisateur clique sur "Démarrer", une session sera automatiquement créée et enregistrée dans `sessions.json` !

---

## 📊 PARTIE 8 : CRÉER LA LISTE DES ENVIRONNEMENTS (OPTIONNEL)

Si vous voulez une interface pour voir tous les environnements :

### Étape 8.1 : Créer le Panel de liste

1. Clic droit sur `MainCanvas` → **UI** → **Panel**
2. Nommez-le : `EnvironnementsListPanel`
3. RectTransform : Stretch (Alt+Shift + clic sur center)
4. Décochez-le pour le masquer au démarrage

### Étape 8.2 : Ajouter un ScrollView

1. Clic droit sur `EnvironnementsListPanel` → **UI** → **Scroll View**
2. Configurez la zone de contenu

### Étape 8.3 : Créer le controller

1. Create Empty → `EnvironnementsListController`
2. Add Component → `EnvironnementsListUI`
3. Assignez les références

---

## 🎯 RÉSUMÉ DES ÉTAPES ESSENTIELLES

✅ **Minimum pour que ça fonctionne :**
1. ✓ Vérifier que les fichiers sont présents
2. ✓ Tester avec `EnvironnementsTestRunner`
3. ✓ Créer l'UI (Partie 3)
4. ✓ Connecter le script (Partie 4)
5. ✓ Créer un bouton d'accès (Partie 5)
6. ✓ Tester (Partie 6)

📦 **Optionnel mais utile :**
- Partie 7 : Enregistrement automatique des sessions
- Partie 8 : Interface de liste des environnements

---

## 🐛 DÉPANNAGE

### Problème : "Aucun environnement chargé"
**Solution** : 
- Vérifiez que `environnements.json` est dans `Assets/HospitalData/StreamingAssets/`
- Vérifiez le contenu du fichier JSON (pas d'erreurs de syntaxe)

### Problème : "NullReferenceException"
**Solution** :
- Vérifiez que toutes les références UI sont bien assignées dans l'Inspector
- Vérifiez qu'aucun champ n'est vide

### Problème : "Le panel ne s'affiche pas"
**Solution** :
- Vérifiez que le panel est bien masqué au démarrage (GameObject désactivé)
- Vérifiez que le bouton appelle bien `ShowForm()`
- Vérifiez dans la Console s'il y a des erreurs

### Problème : "Les dropdowns sont vides"
**Solution** :
- Le fichier JSON existe et est correctement formaté
- Les environnements ont bien des `PositionsDisponibles` et `NiveauxDifficulte`

---

## 📝 CHECKLIST FINALE

Avant de dire que c'est terminé, vérifiez :

- [ ] Aucune erreur dans la Console
- [ ] Le panel s'ouvre quand je clique sur le bouton
- [ ] Les 6 environnements apparaissent dans le dropdown
- [ ] La description change quand je sélectionne un environnement
- [ ] Les positions se mettent à jour selon l'environnement
- [ ] Les difficultés se mettent à jour selon l'environnement
- [ ] Le slider d'assistance fonctionne
- [ ] Le bouton Démarrer affiche la configuration dans Status
- [ ] Le bouton Annuler ferme le panel
- [ ] (Optionnel) Une session est créée dans sessions.json

---

## 🎉 C'EST TERMINÉ !

Vous avez maintenant un système complet de sélection d'environnements fonctionnel dans Unity !

**Prochaines étapes suggérées :**
1. Personnaliser l'apparence de l'UI selon votre charte graphique
2. Ajouter des images pour chaque environnement
3. Intégrer avec votre système VR/de jeu
4. Créer vos propres environnements personnalisés

**Besoin d'aide ?** Consultez les autres fichiers de documentation :
- `ENVIRONNEMENTS_README.md` - Documentation technique complète
- `QUICKSTART.md` - Guide de démarrage rapide
