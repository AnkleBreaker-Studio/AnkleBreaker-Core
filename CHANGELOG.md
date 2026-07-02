# Changelog

## [1.2.0] - 2026-07-02

### Added
- **AnkleBreakerController**: base class for Controllers — EventHandlerRegister/UnRegister are sealed empty, so a Controller cannot subscribe to the event bus (compile-enforced); it is driven by its Manager and pushes intent via HandlerData Request helpers
- **AnkleBreaker_Controller template**: script template for Controllers (no event registration blocks)
- **AnkleBreaker_HandlerData template**: dedicated script template for HandlerData static event buses (canonical Notifications / Commands / Queries sections, one XML summary per event, pure contract rule)

### Changed
- **AnkleBreaker_MonoBehaviour template**: no longer embeds a HandlerData class in the same file — a HandlerData is one file per feature, created from the dedicated template

### Fixed
- **AnkleBreaker_NetworkBehaviour template**: remove suggested conditional guards from EventHandlerUnRegister (UnRegister is always unconditional, one -= per possible +=)
- **AnkleBreakerMonoBehaviour**: IsLocallyReady tooltip referenced OnStartClient/Server; this base flips it at the end of Start

## [1.1.0] - 2026-04-01

### Added
- **ABDefineAttribute**: Assembly-level attribute `[assembly: ABDefine("AB_XXX", typeName, assembly)]` for declarative plugin detection
- **ABDefineManager**: `[InitializeOnLoad]` scanner that collects all `[ABDefine]` attributes across loaded assemblies, detects plugin presence via `Type.GetType`, and sets/removes scripting define symbols automatically
- Conflict detection: `Debug.LogWarning` when two packages declare the same define with different canonical types
- Convention documentation for `AB_` prefixed defines (AB_WWISE, AB_FMOD, AB_I2_LOCALIZE)
- Editor tests for ABDefineAttribute (5 tests) and ABDefineManager (3 tests)

## [1.0.5] - 2026-03-06

### Fixed
- **Breaking change reverted**: renamed `Structs` folder back to `MasterStructs` and restored namespace `AnkleBreaker.Core.MasterStructs` (was `AnkleBreaker.Core.Structs` in 1.0.2–1.0.4) to maintain backward compatibility with existing projects

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
