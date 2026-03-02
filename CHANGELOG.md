# Changelog

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
