# AGENTS.md - TEDUJAM2026 Unity Project

## Project Overview
- **Engine**: Unity 6000.0.59f2
- **Language**: C# 9.0 (.NET Standard 2.1)
- **Render Pipeline**: Universal Render Pipeline (URP)
- **Package Manager**: Unity Package Manager

## Build/Test Commands

### Building the Project
```bash
# Via Unity Editor: File > Build Settings > Build (Ctrl+Shift+B)
# Via command line:
"C:\Program Files\Unity\Hub\Editor\6000.0.59f2\Editor\Unity.exe" -buildTarget StandaloneWindows64 -quit
```

### Running Tests
```bash
# Unity Editor > Window > General > Test Runner
# Or via command line:
"C:\Program Files\Unity\Hub\Editor\6000.0.59f2\Editor\Unity.exe" -runTests -testResults results.xml -testPlatform editmode
```

### Code Analysis
- **Unity Analyzers**: `Unity.SourceGenerators.dll`, `Microsoft.Unity.Analyzers.dll`
- **VS Code**: Uses `visualstudiotoolsforunity.vstuc` extension

### Unity Commands
```bash
# Refresh scripts: Assets > Refresh (Ctrl+R) or:
"C:\Program Files\Unity\Hub\Editor\6000.0.59f2\Editor\Unity.exe" -openProject .
```

## Code Style Guidelines

### File Organization
- Scripts: `Assets/AAA/_Scripts/`
- One class per file, filename matches class name
- Unity manages `.meta` files automatically

### Naming Conventions

| Element | Convention | Example |
|---------|------------|---------|
| Classes | PascalCase | `SoundManager`, `CameraMovement` |
| Public Fields | PascalCase | `brushSize`, `totalXPixels` |
| Private Fields | _camelCase | `_soundPrefabs`, `_camera` |
| SerializeField | _camelCase | `[SerializeField] private int _health` |
| Methods | PascalCase | `OnMoveUp`, `CalculatePixel` |
| Constants | PascalCase | `MAX_HEALTH`, `DEFAULT_SIZE` |
| Interfaces | IPascalCase | `IPoolable` |

### Import Ordering
```csharp
using UnityEngine;                    // UnityEngine first
using UnityEngine.InputSystem;         // Other Unity namespaces
using DG.Tweening;                    // Third-party (alphabetical)
using Lean.Pool;                     // Lean Pool
using System.Collections.Generic;     // System namespaces last
```

### Formatting Rules
- Indentation: 4 spaces
- Braces: Opening brace on same line
- Line length: Max ~120 characters
- Blank lines to separate logical code blocks

### Type Usage
```csharp
// Use var when type is obvious
var soundPrefabs = new Dictionary<string, GameObject>();

// Use explicit types for public members
public int totalXPixels = 1024;
[SerializeField] private InputActionAsset _inputActions;
```

### SerializeField Guidelines
```csharp
[SerializeField] private List<SoundPrefab> _soundPrefabsList;
[SerializeField] private InputActionAsset _inputActions;
[SerializeField] private InputActionReference _actionReference;
```

### Error Handling
```csharp
// Unity-style error handling
if (condition)
{
    Debug.LogWarning($"Warning: {variable}");
}
else
{
    Debug.LogError($"Error: {details}");
}

// Use TryGetValue for dictionary access
if (_soundPrefabs.TryGetValue(key, out GameObject prefab))
{
    LeanPool.Spawn(prefab, position, Quaternion.identity);
}
```

### MonoBehaviour Lifecycle
- `Awake()`: Initialization
- `Start()`: Setup depending on other objects
- `OnEnable()`/`OnDisable()`: Event subscriptions (always unsubscribe in OnDisable)
- Use `Update()` sparingly; prefer coroutines

### Coroutine Patterns
```csharp
private IEnumerator DespawnAfterAudio()
{
    yield return new WaitForSeconds(_audioSource.clip.length);
    LeanPool.Despawn(gameObject);
}
```

### Unity-Specific Best Practices
- Cache `GetComponent<T>()` results
- Prefer `[SerializeField]` over public fields
- Null-check components after `GetComponent()`
- Use `CompareTag()` instead of string comparison

### Pooling
```csharp
using Lean.Pool;
LeanPool.Spawn(prefab, position, Quaternion.identity);
LeanPool.Spawn(prefab, position, Quaternion.identity, parentTransform);
LeanPool.Despawn(gameObject);
```

### Animation (DOTween)
```csharp
using DG.Tweening;
_camera.transform.DORotate(new Vector3(90, 0, 0), 1f);
```

### Input System
```csharp
using UnityEngine.InputSystem;
[SerializeField] private InputActionAsset _inputActions;
_inputActions.Enable();
_inputActions.FindAction("ActionName").performed += OnAction;
```

## Project Structure
```
Assets/
  AAA/_Scripts/         # Game scripts
  Plugins/              # Third-party (DOTween, Lean Pool)
    Demigiant/         # DOTween
    CW/                # Lean Pool, Lean Common
  Resources/           # Runtime-loaded assets
```

## Analyzers Enabled
- `Unity.SourceGenerators.dll` - Code generation
- `Unity.Properties.SourceGenerator.dll` - Properties
- `Unity.UIToolkit.SourceGenerator.dll` - UI Toolkit
- `Microsoft.Unity.Analyzers.dll` - Best practices

## Common Tasks
- Add script: Create in `Assets/AAA/_Scripts/` with PascalCase name
- Create prefab: GameObject > Create Empty, add components
- Add to scene: Drag prefab to Hierarchy or use `Instantiate()`
