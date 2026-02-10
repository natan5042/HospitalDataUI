# 🔍 Script de Vérification des Fichiers
# Exécutez ce script PowerShell pour vérifier que tous les fichiers nécessaires sont présents

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  VÉRIFICATION SYSTÈME ENVIRONNEMENTS" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$baseDir = $PSScriptRoot
$erreurs = 0
$avertissements = 0

# Fonction pour vérifier un fichier
function Test-FileExists {
    param(
        [string]$Path,
        [string]$Description,
        [bool]$Critical = $true
    )
    
    $fullPath = Join-Path $baseDir $Path
    if (Test-Path $fullPath) {
        Write-Host "✓ " -ForegroundColor Green -NoNewline
        Write-Host "$Description"
        return $true
    } else {
        if ($Critical) {
            Write-Host "✗ " -ForegroundColor Red -NoNewline
            Write-Host "$Description" -ForegroundColor Red
            $script:erreurs++
        } else {
            Write-Host "⚠ " -ForegroundColor Yellow -NoNewline
            Write-Host "$Description" -ForegroundColor Yellow
            $script:avertissements++
        }
        return $false
    }
}

Write-Host "📦 VÉRIFICATION DES MODÈLES DE DONNÉES" -ForegroundColor Yellow
Write-Host ""
Test-FileExists "Assets\HospitalData\Models\JsonRecords.cs" "JsonRecords.cs (modifié)"
Write-Host ""

Write-Host "💾 VÉRIFICATION DES GESTIONNAIRES" -ForegroundColor Yellow
Write-Host ""
Test-FileExists "Assets\HospitalData\Storage\EnvironnementsManager.cs" "EnvironnementsManager.cs"
Test-FileExists "Assets\HospitalData\Storage\HospitalDataService.cs" "HospitalDataService.cs (modifié)"
Write-Host ""

Write-Host "🎨 VÉRIFICATION DES SCRIPTS UI" -ForegroundColor Yellow
Write-Host ""
Test-FileExists "Assets\Scripts\HospitalUI\EnvironnementSelectionUI.cs" "EnvironnementSelectionUI.cs"
Test-FileExists "Assets\Scripts\HospitalUI\EnvironnementsListUI.cs" "EnvironnementsListUI.cs"
Test-FileExists "Assets\Scripts\HospitalUI\SessionLauncher.cs" "SessionLauncher.cs"
Test-FileExists "Assets\Scripts\HospitalUI\EnvironnementsTestRunner.cs" "EnvironnementsTestRunner.cs"
Write-Host ""

Write-Host "📄 VÉRIFICATION DES DONNÉES JSON" -ForegroundColor Yellow
Write-Host ""
$jsonExists = Test-FileExists "Assets\HospitalData\StreamingAssets\environnements.json" "environnements.json (CRITIQUE)"

# Vérifier le contenu du JSON si le fichier existe
if ($jsonExists) {
    try {
        $jsonPath = Join-Path $baseDir "Assets\HospitalData\StreamingAssets\environnements.json"
        $jsonContent = Get-Content $jsonPath -Raw | ConvertFrom-Json
        $envCount = $jsonContent[0].Environnements.Count
        
        if ($envCount -eq 6) {
            Write-Host "  → " -NoNewline
            Write-Host "Contient $envCount environnements ✓" -ForegroundColor Green
        } else {
            Write-Host "  → " -NoNewline
            Write-Host "Contient seulement $envCount environnements (attendu: 6) ⚠" -ForegroundColor Yellow
            $avertissements++
        }
        
        # Lister les environnements
        Write-Host "  → Environnements détectés:" -ForegroundColor Cyan
        foreach ($env in $jsonContent[0].Environnements) {
            Write-Host "    • $($env.NomEnvironnement) ($($env.IdEnvironnement))" -ForegroundColor Gray
        }
    } catch {
        Write-Host "  → " -NoNewline
        Write-Host "Erreur lors de la lecture du JSON: $($_.Exception.Message)" -ForegroundColor Red
        $erreurs++
    }
}
Write-Host ""

Write-Host "📚 VÉRIFICATION DE LA DOCUMENTATION" -ForegroundColor Yellow
Write-Host ""
Test-FileExists "README_DEMARRAGE.md" "README_DEMARRAGE.md" -Critical $false
Test-FileExists "CHECKLIST.md" "CHECKLIST.md" -Critical $false
Test-FileExists "GUIDE_IMPLEMENTATION_UNITY.md" "GUIDE_IMPLEMENTATION_UNITY.md" -Critical $false
Test-FileExists "HIERARCHIE_UI.md" "HIERARCHIE_UI.md" -Critical $false
Test-FileExists "QUICKSTART.md" "QUICKSTART.md" -Critical $false
Test-FileExists "ENVIRONNEMENTS_README.md" "ENVIRONNEMENTS_README.md" -Critical $false
Write-Host ""

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  RÉSUMÉ" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

if ($erreurs -eq 0) {
    Write-Host "✓ Tous les fichiers critiques sont présents !" -ForegroundColor Green
} else {
    Write-Host "✗ $erreurs fichier(s) critique(s) manquant(s) !" -ForegroundColor Red
}

if ($avertissements -gt 0) {
    Write-Host "⚠ $avertissements avertissement(s)" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "🎯 PROCHAINES ÉTAPES:" -ForegroundColor Cyan
Write-Host ""

if ($erreurs -eq 0) {
    Write-Host "1. Ouvrez Unity" -ForegroundColor White
    Write-Host "2. Consultez CHECKLIST.md pour l'implémentation" -ForegroundColor White
    Write-Host "3. Suivez GUIDE_IMPLEMENTATION_UNITY.md étape par étape" -ForegroundColor White
    Write-Host ""
    Write-Host "👉 Commencez ici: README_DEMARRAGE.md" -ForegroundColor Green
} else {
    Write-Host "⚠ Veuillez d'abord corriger les fichiers manquants" -ForegroundColor Yellow
    Write-Host "   Vérifiez que tous les scripts ont bien été créés" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Pause pour lire les résultats
Write-Host "Appuyez sur une touche pour continuer..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
