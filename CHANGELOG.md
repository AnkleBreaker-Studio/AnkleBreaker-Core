# Changelog

## [1.0.2] - 2026-03-02

### Fixed
- `ImplementsCategory` always returned `false` when `mustContainsAllCategories` was `true`: added missing `return true` after validation loop
- Templates referenced non-existent types (`RltyNetworkBehaviour`, `UnityAction`) and missed usings: updated to use Core base classes and `System.Action`

### Changed
- `AnkleBreakerCategory.CategoryIcon`: converted from public field to property with `[field: SerializeField]` and private set
- `AssetIdentityStruct` namespace renamed from `AnkleBreaker.Core.MasterStructs` to `AnkleBreaker.Core.Structs` to match folder name
- Installer: extracted shared `InstallPackageAsync` method, removing duplicated install logic
- Installer coroutine system: added `try/catch` for exceptions and cleanup of `EditorApplication.update` when queue is empty
- Added XML summary on `ImplementsType` to clarify inheritance check direction

### Removed
- Unused `System.Collections` and `System.Collections.Generic` usings in `IBehaviour.cs`

### Improved
- `README.md` expanded with installation instructions, content overview, and optional dependencies

## [1.0.1] - 2026-03-02

### Changed
- Renamed preprocessor define from `AB_UTILS` to `AB_UTILS_INSPECTOR` for clarity and granularity
- Updated `versionDefines` in both runtime and editor `.asmdef` files to support both the legacy monolithic `com.anklebreaker-studio.utils` and the new split `com.anklebreaker-studio.utils.inspector` package
- Auto-installer now installs `utils-inspector` instead of the full `AnkleBreaker-Utils` monolith
- Updated all `#if AB_UTILS` guards to `#if AB_UTILS_INSPECTOR` in `AnkleBreakerMonoBehaviour.cs`
- Renamed `InstallAnkleBreakerUtils()` to `InstallAnkleBreakerUtilsInspector()`

### Notes
- Backward compatible: projects using the old monolithic Utils package will continue to work (both package names trigger the same define)

## [1.0.0]

### Added
- Initial release of AnkleBreaker-Core
- Base classes: `AnkleBreakerMonoBehaviour`, `AnkleBreakerCategory`
- Master interfaces: `IIsReady`, `IBehaviour<T>`, `IAssetIdentitySO`
- Master delegates: 52 `ActionRef`/`ActionIn` delegate definitions
- Structs: `AssetIdentityStruct`
- Editor: Dependencies auto-installer, menu items
- Templates: MonoBehaviour and NetworkBehaviour script templates
