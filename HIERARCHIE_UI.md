# 🎨 Hiérarchie UI à Créer dans Unity

## Structure visuelle du système d'environnements

```
📦 Hierarchy Unity
│
├── 🎮 MainCanvas (Canvas)
│   │
│   ├── 📋 EnvironnementSelectionPanel (Panel) [INACTIF au démarrage]
│   │   ├── 📝 TitleText (TextMeshPro) - "Sélection d'Environnement"
│   │   ├── 📥 EnvironnementDropdown (Dropdown TMP) - Liste des environnements
│   │   ├── 📄 DescriptionText (TextMeshPro) - Description de l'environnement
│   │   ├── 📥 PositionDropdown (Dropdown TMP) - Positions disponibles
│   │   ├── 📥 DifficulteDropdown (Dropdown TMP) - Niveaux de difficulté
│   │   ├── ⌨️ DureeInputField (InputField TMP) - Durée en secondes
│   │   ├── 🎚️ NiveauAssistanceSlider (Slider) - 0 à 5
│   │   ├── 🏷️ NiveauAssistanceLabel (TextMeshPro) - "Niveau d'assistance: X"
│   │   ├── 💬 StatusText (TextMeshPro) - Messages d'erreur/succès
│   │   ├── ✅ StartButton (Button) - "DÉMARRER"
│   │   └── ❌ CancelButton (Button) - "ANNULER"
│   │
│   └── 🔘 SelectEnvironnementButton (Button) - Bouton menu principal
│
├── 🎮 EnvironnementSelectionController (Empty GameObject)
│   └── 📜 Script: EnvironnementSelectionUI
│
├── 🎮 SessionLauncher (Empty GameObject) [OPTIONNEL]
│   └── 📜 Script: SessionLauncher
│
└── 🧪 EnvironnementsTestRunner (Empty GameObject) [POUR TESTS]
    └── 📜 Script: EnvironnementsTestRunner
```

---

## 📐 Positions et Tailles des Éléments UI

### Panel Principal
```
EnvironnementSelectionPanel
├─ Anchor: Center/Center
├─ Width: 800
├─ Height: 600
└─ Color: Noir (0,0,0, Alpha: 200/255)
```

### Éléments à l'intérieur du Panel

```
TitleText
├─ Position: (0, 250)
├─ Size: 700 x 50
└─ Font Size: 32

EnvironnementDropdown
├─ Position: (0, 180)
└─ Size: 600 x 40

DescriptionText
├─ Position: (0, 110)
├─ Size: 700 x 80
└─ Font Size: 16

PositionDropdown
├─ Position: (-200, 20)
└─ Size: 280 x 40

DifficulteDropdown
├─ Position: (200, 20)
└─ Size: 280 x 40

DureeInputField
├─ Position: (-200, -50)
└─ Size: 280 x 40

NiveauAssistanceSlider
├─ Position: (200, -50)
├─ Size: 280 x 20
├─ Min Value: 1
└─ Max Value: 10

NiveauAssistanceLabel
├─ Position: (200, -80)
├─ Size: 280 x 30
├─ Font Size: 14
└─ Default Text: "Niveau d'assistance: 1"

StatusText
├─ Position: (0, -150)
├─ Size: 700 x 100
└─ Font Size: 14

StartButton
├─ Position: (-100, -260)
├─ Size: 150 x 50
└─ Color: Vert (0, 200, 0)

CancelButton
├─ Position: (100, -260)
├─ Size: 150 x 50
└─ Color: Rouge (200, 0, 0)
```

---

## 🔗 Connexions Inspector

### EnvironnementSelectionController
**Script: EnvironnementSelectionUI**

```
Assignations à faire (Drag & Drop) :
├─ Form Panel ───────────────────► EnvironnementSelectionPanel
├─ Environnement Dropdown ───────► EnvironnementDropdown
├─ Position Dropdown ────────────► PositionDropdown
├─ Difficulte Dropdown ──────────► DifficulteDropdown
├─ Duree Input Field ────────────► DureeInputField
├─ Niveau Assistance Slider ─────► NiveauAssistanceSlider
├─ Niveau Assistance Label ──────► NiveauAssistanceLabel
├─ Description Text ─────────────► DescriptionText
├─ Status Text ──────────────────► StatusText
├─ Start Button ─────────────────► StartButton
└─ Cancel Button ────────────────► CancelButton
```

### StartButton
**Component: Button**

```
On Click ()
└─ EnvironnementSelectionController.OnStartSession()
```

### CancelButton
**Component: Button**

```
On Click ()
└─ EnvironnementSelectionController.HideForm()
```

### SelectEnvironnementButton (Bouton menu)
**Component: Button**

```
On Click ()
└─ EnvironnementSelectionController.ShowForm()
```

### SessionLauncher (OPTIONNEL)
**Script: SessionLauncher**

```
├─ Patient Id ───────────────────► "patient-001"
├─ Supervisor Id ────────────────► "" (vide)
└─ Environnement UI ─────────────► EnvironnementSelectionController
```

---

## 🎨 Aperçu Visuel du Panel

```
╔════════════════════════════════════════════════════════════╗
║                                                            ║
║              SÉLECTION D'ENVIRONNEMENT                     ║
║                                                            ║
║  ┌────────────────────────────────────────────────────┐   ║
║  │ 🌲 Forêt Virtuelle                            ▼   │   ║
║  └────────────────────────────────────────────────────┘   ║
║                                                            ║
║    Un environnement forestier calme avec sons             ║
║    naturels pour la rééducation visuelle                  ║
║                                                            ║
║  ┌──────────────────────┐  ┌──────────────────────┐       ║
║  │ Position: Assis   ▼  │  │ Difficulté: Moyen ▼  │       ║
║  └──────────────────────┘  └──────────────────────┘       ║
║                                                            ║
║  ┌──────────────────────┐  ┌──────────────────────┐       ║
║  │ Durée: 600          │  │ ━━━━━●━━━━━         │       ║
║  └──────────────────────┘  │ Niveau assistance: 2│       ║
║                            └──────────────────────┘       ║
║                                                            ║
║                                                            ║
║         [Informations de statut s'affichent ici]          ║
║                                                            ║
║                                                            ║
║         ┌──────────┐           ┌──────────┐               ║
║         │ DÉMARRER │           │ ANNULER  │               ║
║         └──────────┘           └──────────┘               ║
║                                                            ║
╚════════════════════════════════════════════════════════════╝
```

---

## ⚡ Ordre de Création Recommandé

1. **Canvas et Panel** (base)
2. **Titre** (pour voir la structure)
3. **Dropdown environnement** (élément principal)
4. **Description** (pour voir les infos)
5. **Dropdowns position et difficulté** (configuration)
6. **Input durée et slider** (paramètres)
7. **Labels et status** (feedback)
8. **Boutons** (actions)
9. **GameObject Controller** (logique)
10. **Connexions** (câblage)

---

## 🎯 Points de Vérification

**Après chaque création d'élément UI** :
- ✅ L'élément est dans le bon parent
- ✅ La position est correcte
- ✅ La taille est correcte
- ✅ Le texte/placeholder est correct

**Après connexion du script** :
- ✅ Aucun champ n'est vide dans l'Inspector
- ✅ Tous les éléments sont assignés
- ✅ Les boutons ont leurs événements configurés

**Avant le test final** :
- ✅ Le panel est désactivé au démarrage
- ✅ Aucune erreur de compilation
- ✅ Le fichier JSON existe et contient des données

---

## 💡 Astuces Unity

### Pour dupliquer rapidement :
1. Créez un élément UI
2. `Ctrl + D` pour le dupliquer
3. Renommez et repositionnez
4. Modifiez le texte/configuration

### Pour aligner plusieurs éléments :
1. Sélectionnez plusieurs GameObjects (Ctrl + clic)
2. Window → 2D → Rect Tool
3. Utilisez les boutons d'alignement en haut

### Pour tester rapidement :
1. Mode Play
2. Sélectionnez un GameObject
3. Modifiez dans Inspector (les changements sont temporaires)
4. Notez ce qui fonctionne
5. Stop Play et appliquez en mode Edit

---

## 🚀 Raccourcis Utiles

- `F` : Centrer la vue sur l'objet sélectionné
- `Ctrl + D` : Dupliquer
- `Ctrl + Shift + N` : Créer GameObject vide
- `Alt + Shift + Click` : Anchor presets avec position/size
- `T` : Rect Transform Tool (pour UI)

---

Cette hiérarchie vous donne une vue d'ensemble claire de ce qu'il faut créer.
Suivez le **GUIDE_IMPLEMENTATION_UNITY.md** pour les instructions détaillées étape par étape !
