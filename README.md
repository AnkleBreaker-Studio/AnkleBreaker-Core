# AnkleBreaker-Core

Base classes, interfaces, and delegates for the AnkleBreaker package ecosystem.

## Installation

Add via Unity Package Manager using the Git URL:

```
https://github.com/AnkleBreaker-Studio/AnkleBreaker-Core.git
```

## What's Included

**Base Classes** — `AnkleBreakerMonoBehaviour` (abstract MonoBehaviour with event handler lifecycle and readiness tracking), `AnkleBreakerCategory` (abstract ScriptableObject for category systems with inheritance checks).

**Interfaces** — `IIsReady` (readiness state), `IBehaviour<T>` (config-based initialization), `IAssetIdentitySO` (identity with categories and localization).

**Delegates** — `ActionRef`, `ActionIn`, `ActionInRef` and variants supporting `ref`/`in` parameter modifiers up to 6 parameters.

**Structs** — `AssetIdentityStruct` (serializable title, description, thumbnail).

**Templates** — Script templates for MonoBehaviour and NetworkBehaviour following AB conventions.

## Optional Dependencies

AnkleBreaker-Core has no required dependencies. It optionally integrates with:

- **AnkleBreaker Utils Inspector** — Provides `[HideInNormalInspector]` attribute on base class fields. Auto-installed if not present.
- **Odin Inspector** — `AnkleBreakerCategory` inherits from `SerializedScriptableObject` when available.

## Requirements

- Unity 2022.3 LTS or later
