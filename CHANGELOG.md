# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [v0.4.17-preview] - 2026-08-23

This release overhauls CI/CD automation with weekly prerelease workflows, automated version bumping, and AI-powered changelog generation. All driver packages receive NuGet dependency updates, and GitHub Actions dependencies are brought to their latest versions.

### Added

- ✨ Add `nanoframework_weekly_prerelease.yml` workflow for automated weekly prerelease version bumps on a Sunday schedule (`607039ac`, PR #212)
- ✨ Add `nanoframework_version_bump.yml` workflow for manual major/minor/patch/custom version bumps with validation (`6482bbcf`, PR #214)
- ✨ Add `VERSION` file as the single source of truth for package versioning
- ✨ Add root-level `Directory.Build.props` with `RestorePackagesWithLockFile` and `RestoreLockedMode` enabled for reproducible builds (`f89f3746`)
- ✨ Add NuGet package caching to build, CodeQL, and release workflows (`40aee2b1`, `ab4026a4`, `daaf0c0b`)
- ✨ Add concurrency groups with `cancel-in-progress: true` to build and CodeQL workflows to prevent redundant runs (`26156540`, `f3c51aad`)
- ✨ Add `id-token` permission to release workflow for OIDC-based NuGet authentication (`eb71822b`)
- ✨ Add `workflow_run` trigger to changelog workflow so it fires after the release workflow completes (`86d3dd2b`)

### Changed

- 🔄 Rework release workflow (`nanoframework_release.yml`) to support multi-trigger: release published event, `VERSION` file changes on master, and `workflow_call` with prerelease/version inputs (`07423ba5`, `153f2207`)
- 🔄 Switch changelog generation to use Anthropic (Claude) as the Git-Iris provider (`258f85ad`)
- 🔄 Change default version bump fragment from `minor` to `patch` (`4572f180`, `7e047c7b`)
- 🔄 Upgrade Mergify configuration to current format (`a880c285`, PR #202)
- 🔄 Move `Directory.Build.props` from `Meteostanice/` to project root (`f89f3746`, `2164899c`)
- 🔄 Update .NET SDK target from 9.0.x to 10.0.x across all workflows (`da90cf3c`)

#### GitHub Actions dependency bumps

- `actions/checkout`: 6.0.2 → 7.0.1
- `actions/setup-dotnet`: 5.1.0 → 6.0.0
- `actions/setup-java`: 5.2.0 → 5.7.0
- `actions/cache`: 5.0.3 → 6
- `actions/upload-artifact`: 6.0.0 → 7.0.1
- `nuget/setup-nuget`: 2.0.1 → 4.0
- `microsoft/setup-msbuild`: 2 → 3
- `richardrigutins/replace-in-files`: 2 → 3
- `peter-evans/create-pull-request`: 8.1.0 → 8.1.1
- `nanoframework/nanobuild`: 1.18 → 1.19
- `nanoframework/nanodu`: 1.0.26 → 1.0.27

#### NuGet dependency bumps (all driver packages)

- `nanoFramework.Runtime.Events`: 1.11.32 → 1.11.39
- `nanoFramework.System.Device.Gpio`: 1.1.57 → 1.1.64
- `nanoFramework.System.Device.Spi`: 1.3.82 → 1.3.90
- `nanoFramework.System.IO.Ports`: 1.1.132 → 1.1.142
- `nanoFramework.Hardware.Esp32`: 1.6.37 → 1.6.42
- `UnitsNet.nanoFramework.*` (Duration, Illuminance, Length, Pressure, Ratio, RelativeHumidity, Temperature, VolumeConcentration): 5.75.0 → 5.75.1

Affected driver packages: **CST816D**, **DriverBaseInterfaces** (Altitude, CO2, DewPoint, Gpio, Humidity, Infrared, Light, Pressure, Sensitivity, Temperature), **DriverBaseSPI**, **DriverBaseUART**, **HDC1080**, **LPS22HB**, **MHZ19B**, **PI4IOE5V6408**, **QMP6988**, **SHT3x**, **SHTC3**, **SSD1331**, **TCS34725**, **TSL2561**, and **Meteostanice**.

### Fixed

- 🐛 Fix NuGet restore command syntax in build workflow (`0077e952`)
- 🐛 Fix YAML indentation in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (`8e17d084`, `a4c86da9`, `99f1cf19`)
- 🐛 Fix NuGet cache path configuration across build, CodeQL, and release workflows, settling on user home directory path (`e63fc5b9`, `9767855a`, `fb1d3e85`)

### Removed

- 🔥 Remove `Meteostanice/Directory.Build.props` in favor of root-level props file (`2164899c`)
- 🔥 Remove redundant write permissions from weekly prerelease workflow (`67ca104f`)

### Metrics

- Total Commits: 157
- Files Changed: 81
- Insertions: +1,554
- Deletions: -765
<!-- -------------------------------------------------------------- -->

## [v0.4.16-preview] - 2026-08-16

This prerelease introduces automated weekly versioning and changelog generation workflows, refactors all CI/CD pipelines with NuGet caching and concurrency controls, and updates nanoFramework and UnitsNet dependencies across all driver packages.

### Added

- Add **weekly prerelease workflow** (`nanoframework_weekly_prerelease.yml`) that bumps the patch version every Sunday and opens a PR to `master` (607039a)
- Add **manual version bump workflow** (`nanoframework_version_bump.yml`) supporting major, minor, patch, or custom SemVer increments with validation (6482bbcf)
- Add **AI-powered changelog generation** via `git-iris` with Anthropic provider, triggered after releases or on demand (`nanoframework_changelog.yml`) (258f85ad)
- Add `VERSION` file as the single source of truth for project semantic versioning
- Add `Directory.Build.props` at repository root enforcing `RestorePackagesWithLockFile` and `RestoreLockedMode` for reproducible NuGet restores (f89f3746)
- Add NuGet package caching (`actions/cache`) to build, release, and CodeQL workflows (40aee2b1, ab4026a4, daaf0c0b)
- Add concurrency groups with `cancel-in-progress` to build and CodeQL workflows to prevent redundant runs (26156540, f3c51aad)
- Add Mergify rules for auto-merging and auto-approving version bump PRs (`release/weekly-prerelease-version`, `release/manual-version-bump`) and changelog PRs (`update-changelog`)

### Changed

- Upgrade Mergify configuration to current format, replacing deprecated syntax (a880c285, PR #202)
- Update release workflow to support `workflow_call` trigger with version/prerelease inputs and `push` trigger on `VERSION` file changes
- Update release workflow to use OIDC-based NuGet authentication (`id-token: write`) instead of static API keys
- Bump .NET SDK to `10.0.x` and Java to version `17` (Zulu) across build, release, and CodeQL workflows
- Change default version bump increment from `minor` to `patch` for weekly prereleases (4572f180, 7e047c7b)
- Revert version from `0.5.0` back to `0.4.x` series after adjusting bump strategy (b01fa23a)

#### GitHub Actions Dependencies

- Bump `actions/checkout` from 6.0.2 to 7.0.1
- Bump `actions/upload-artifact` from 6.0.0 to 7.0.1
- Bump `actions/cache` from 5.0.3 to 6
- Bump `actions/setup-dotnet` from 5.1.0 to 6.0.0
- Bump `actions/setup-java` from 5.2.0 to 5.7.0
- Bump `microsoft/setup-msbuild` from 2 to 3
- Bump `nuget/setup-nuget` from 2.0.1 to 4.0
- Bump `peter-evans/create-pull-request` from 8.1.0 to 8.1.1
- Bump `richardrigutins/replace-in-files` from 2 to 3
- Bump `nanoframework/nanobuild` from 1.18 to 1.19
- Bump `nanoframework/nanodu` from 1.0.26 to 1.0.27

#### NuGet Dependencies (all driver packages)

- Bump `nanoFramework.Hardware.Esp32` from 1.6.37 to 1.6.42
- Bump `nanoFramework.Runtime.Events` from 1.11.32 to 1.11.39
- Bump `nanoFramework.System.Device.Gpio` from 1.1.57 to 1.1.64
- Bump `nanoFramework.System.Device.Spi` from 1.3.82 to 1.3.90
- Bump `nanoFramework.System.IO.Ports` from 1.1.132 to 1.1.142
- Bump all `UnitsNet.nanoFramework.*` packages from 5.75.0 to 5.75.1 (Duration, Illuminance, Length, Pressure, Ratio, RelativeHumidity, Temperature, VolumeConcentration)

### Fixed

- Fix NuGet restore command syntax in build workflow (0077e952)
- Fix YAML indentation in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (8e17d084, a4c86da9, 99f1cf19)
- Fix NuGet cache path resolution, settling on user home directory after several iterations (e63fc5b9)
- Remove stale `Meteostanice/Directory.Build.props` in favor of the new root-level file (2164899c)

### Metrics

- Total Commits: 152
- Files Changed: 81
- Insertions: +1,492
- Deletions: -765
<!-- -------------------------------------------------------------- -->

## [v0.4.15-preview] - 2026-08-09

This release introduces **automated weekly prerelease workflows** and **manual version bump tooling**, overhauls CI/CD pipeline configuration (NuGet caching, concurrency, permissions), and updates all NuGet and GitHub Actions dependencies across every driver package.

### Added

- Add `nanoframework_weekly_prerelease.yml` workflow for automated Sunday prerelease version bumps with PR creation and auto-merge via Mergify (607039a)
- Add `nanoframework_version_bump.yml` workflow for on-demand major/minor/patch/custom version bumps with SemVer validation (6482bbc)
- Add `nanoframework_changelog.yml` trigger on `workflow_run` completion for automated changelog generation after releases (86d3dd2)
- Add root-level `Directory.Build.props` enforcing NuGet package lock file restoration in locked mode (f89f374)
- Add `VERSION` file as single source of truth for package versioning, currently at `0.4.15`
- Add NuGet package caching steps to build, CodeQL, and release workflows (40aee2b, ab4026a, daaf0c0)
- Add concurrency settings to build and CodeQL workflows to cancel redundant runs (2615654, f3c51aa)
- Add `id-token` and `checks` permissions to prerelease workflow (eb71822, 82ec7e9)
- Add Mergify rules for automatic approval and merge of version bump PRs matching `release/(weekly-prerelease-version|manual-version-bump)` branches

### Changed

- **NuGet dependencies (nanoFramework core):**
  - `nanoFramework.Hardware.Esp32` 1.6.37 → 1.6.42 (Meteostanice)
  - `nanoFramework.Runtime.Events` 1.11.32 → 1.11.39 (all drivers using events)
  - `nanoFramework.System.Device.Gpio` 1.1.57 → 1.1.64 (CST816D, PI4IOE5V6408, DriverBaseInterfaces.Gpio, DriverBaseSPI, Meteostanice)
  - `nanoFramework.System.Device.Spi` 1.3.82 → 1.3.90 (DriverBaseSPI, SSD1331, Meteostanice)
  - `nanoFramework.System.IO.Ports` 1.1.132 → 1.1.142 (DriverBaseUART, MHZ19B, Meteostanice)
- **NuGet dependencies (UnitsNet):**
  - `UnitsNet.nanoFramework.*` 5.75.0 → 5.75.1 for all eight unit packages: Duration, Illuminance, Length, Pressure, Ratio, RelativeHumidity, Temperature, VolumeConcentration
- **GitHub Actions (major bumps):**
  - `actions/checkout` 6.0.2 → 7.0.1
  - `actions/cache` 5.0.3 → 6
  - `actions/setup-dotnet` 5.1.0 → 6.0.0
  - `actions/upload-artifact` 6.0.0 → 7.0.1
  - `nuget/setup-nuget` 2.0.1 → 4.0
  - `microsoft/setup-msbuild` 2 → 3
  - `richardrigutins/replace-in-files` 2 → 3
- **GitHub Actions (minor/patch bumps):**
  - `actions/setup-java` 5.2.0 → 5.7.0
  - `peter-evans/create-pull-request` 8.1.0 → 8.1.1
  - `nanoframework/nanobuild` 1.18 → 1.19
  - `nanoframework/nanodu` 1.0.26 → 1.0.27
- Upgrade Mergify configuration to current format, add version-bump automation rules, exclude auto-generated PRs from thank-you comments (a880c28, PR #202)
- Change default weekly version bump from `minor` to `patch` increment (4572f18, 7e047c7)
- Update build workflow to use .NET 10.0.x and `nanobuild` v1.20 with preview features
- Consolidate `Directory.Build.props` from `Meteostanice/` subdirectory to repository root (2164899)
- Use Anthropic provider for Git-Iris changelog generation (258f85a)

### Fixed

- Fix NuGet restore command syntax in build workflow (0077e95)
- Fix YAML indentation in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (8e17d08, a4c86da, 99f1cf1)
- Fix NuGet cache path configuration across build, CodeQL, and release workflows after iterating through relative, absolute, and user-directory approaches (47b891b, e63fc5b, 9767855)

### Removed

- Remove standalone NuGet cache setup step from build workflow in favor of `actions/cache` integration (d81d6be)
- Delete `Meteostanice/Directory.Build.props` after consolidation to root (2164899)

### Metrics

- Total Commits: 192
- Files Changed: 81
- Insertions: +1,428
- Deletions: -765
<!-- -------------------------------------------------------------- -->

## [v0.4.14-preview] - 2026-08-02

This release overhauls the CI/CD pipeline with automated weekly prerelease versioning, NuGet package caching, .NET 10.0 migration, and AI-powered changelog generation. All driver packages receive NuGet dependency updates.

### Added

- ✨ Add `nanoframework_weekly_prerelease.yml` workflow for automated weekly prerelease builds on a Sunday schedule (607039ac)
- ✨ Add `nanoframework_version_bump.yml` workflow for manual semantic version bumping with major/minor/patch/custom options (6482bbcf)
- ✨ Add `VERSION` file as single source of truth for project versioning, consumed by release and changelog workflows
- ✨ Add `Directory.Build.props` at repository root with `RestorePackagesWithLockFile` and `RestoreLockedMode` for reproducible NuGet restores (f89f3746)
- ✨ Add NuGet package caching via `actions/cache` across build, release, and CodeQL workflows (40aee2b1, ab4026a4, daaf0c0b)
- ✨ Add concurrency groups with `cancel-in-progress: true` to build and CodeQL workflows (26156540, f3c51aad)
- ✨ Add GitHub Release creation step with NuGet package assets in `nanoframework_release.yml` using `softprops/action-gh-release@v3`
- ✨ Add OIDC-based NuGet authentication via `NuGet/login@v1`, replacing hardcoded API keys in the release workflow
- ✨ Add `workflow_run` trigger to changelog workflow so it runs automatically after releases (86d3dd2b)
- ✨ Add Mergify auto-merge and auto-approve rules for `release/weekly-prerelease-version` and `release/manual-version-bump` branches

### Changed

- 🔄 Migrate from .NET 9.0.x to **.NET 10.0.x** across all CI workflows
- 🔄 Switch changelog generation from `craicoverflow/install-git-chglog` to **Git-Iris** (`hyperb1iss/git-iris@v2`) with Anthropic AI backend (258f85ad)
- 🔄 Change default weekly version bump from minor to **patch** increment (4572f180, 7e047c7b)
- 🔄 Upgrade Mergify configuration to current YAML format (a880c285, PR #202)
- 🔄 Enforce `-LockedMode` on all `nuget restore` commands for deterministic builds
- 🔄 Update `nanoframework/nanobuild` from 1.18 to 1.20 with `usePreview: true`

#### GitHub Actions version bumps

- `actions/checkout` 6.0.2 → 7.0.1
- `actions/setup-dotnet` 5.1.0 → 6.0.0
- `actions/setup-java` 5.2.0 → 5.6.0
- `actions/cache` 5.0.3 → 6
- `actions/upload-artifact` 6.0.0 → 7.0.1
- `microsoft/setup-msbuild` 2 → 3
- `nuget/setup-nuget` 2.0.1 → 4.0
- `nanoframework/nanodu` 1.0.26 → 1.0.27
- `richardrigutins/replace-in-files` 2 → 3
- `peter-evans/create-pull-request` 8.1.0 → 8.1.1

#### NuGet dependency updates (all driver packages)

- `nanoFramework.Runtime.Events` 1.11.32 → 1.11.39 (CST816D, DriverBaseInterfaces.Gpio, DriverBaseSPI, DriverBaseUART, PI4IOE5V6408, MHZ19B)
- `nanoFramework.System.Device.Gpio` 1.1.57 → 1.1.64 (CST816D, DriverBaseInterfaces.Gpio, DriverBaseSPI, PI4IOE5V6408)
- `nanoFramework.System.Device.Spi` 1.3.82 → 1.3.90 (DriverBaseSPI)
- `nanoFramework.System.IO.Ports` 1.1.132 → 1.1.142 (DriverBaseUART, MHZ19B)
- `UnitsNet.nanoFramework.Temperature` 5.75.0 → 5.75.1 (DriverBaseInterfaces.Temperature, HDC1080, LPS22HB, SHT3x)
- `UnitsNet.nanoFramework.RelativeHumidity` 5.75.0 → 5.75.1 (HDC1080, SHT3x)
- `UnitsNet.nanoFramework.Pressure` 5.75.0 → 5.75.1 (LPS22HB)
- `UnitsNet.nanoFramework.Ratio` 5.75.0 → 5.75.1 (MHZ19B)
- `UnitsNet.nanoFramework.VolumeConcentration` 5.75.0 → 5.75.1 (MHZ19B)

### Fixed

- 🐛 Fix `nanoframework_build.yml` workflow syntax and indentation issues (da1731fa, a4c86da9)
- 🐛 Fix `nuget restore` command syntax across workflows (0077e952)
- 🐛 Fix indentation in `codeql.yml` restore-keys block (8e17d084)
- 🐛 Fix indentation in `nanoframework_release.yml` (99f1cf19)

### Removed

- 🔥 Remove `Meteostanice/Directory.Build.props` in favor of the new root-level `Directory.Build.props` (2164899c)
- 🔥 Remove standalone NuGet cache setup step from build workflow, replaced by unified caching strategy (d81d6be1)

### Metrics

- Total Commits: 155
- Files Changed: 81
- Insertions: +1,357
- Deletions: -765
<!-- -------------------------------------------------------------- -->

## [v0.4.13-preview] - 2026-07-26

This prerelease introduces **automated version management** with weekly prerelease and manual version bump workflows, upgrades all CI/CD pipelines to .NET 10.0 with OIDC-based NuGet authentication, and updates nanoFramework core and UnitsNet dependencies across all driver packages.

### Added

- ✨ Add weekly prerelease workflow (`nanoframework_weekly_prerelease.yml`) for automated Sunday patch version bumps (607039ac)
- ✨ Add manual version bump workflow (`nanoframework_version_bump.yml`) supporting major, minor, patch, and custom SemVer increments (6482bbcf)
- ✨ Add `VERSION` file as centralized version source of truth for all release workflows
- ✨ Add root-level `Directory.Build.props` with `RestorePackagesWithLockFile` and `RestoreLockedMode` for reproducible NuGet restores (f89f3746)
- ✨ Add NuGet package caching with lock file-based cache keys in build, release, and CodeQL workflows
- ✨ Add concurrency groups to build and CodeQL workflows to cancel redundant runs (26156540, f3c51aad)
- ✨ Add OIDC-based NuGet authentication via `NuGet/login@v1` in release workflow, replacing long-lived API keys
- ✨ Add automated GitHub release creation via `softprops/action-gh-release@v3` with prerelease flag support
- ✨ Add AI-powered changelog generation using `hyperb1iss/git-iris@v2` with Anthropic API (258f85ad)
- ✨ Add Mergify auto-merge rules for version bump PRs (`release/weekly-prerelease-version`, `release/manual-version-bump`)

### Changed

- 🔄 Upgrade .NET SDK from `9.0.x` to `10.0.x` across build, release, and CodeQL workflows
- 🔄 Upgrade Mergify configuration to current format, add conflict detection comments and merged PR cleanup (a880c285)
- 🔄 Bump `nanoframework/nanobuild` from 1.18 to 1.20 with preview mode enabled
- 🔄 Update `nanoframework/nanodu` to v1.0.27
- 🔄 Change default version bump fragment from `minor` to `patch` (7e047c7b)
- 🔄 Add `workflow_run` trigger to changelog workflow for post-release automation (86d3dd2b)
- 🔄 Add `push` trigger on `VERSION` file and `workflow_call` support to release workflow
- 🔄 Replace `craicoverflow/install-git-chglog` with `hyperb1iss/git-iris@v2` for changelog generation
- 🔄 Add `-LockedMode` flag to NuGet restore commands for deterministic builds

**GitHub Actions bumps (dependabot):**
- `actions/checkout`: 6.0.2 → 7.0.1
- `actions/setup-dotnet`: 5.1.0 → 6.0.0
- `actions/setup-java`: 5.2.0 → 5.6.0
- `actions/upload-artifact`: 6.0.0 → 7.0.1
- `actions/cache`: 5.0.3 → 6
- `microsoft/setup-msbuild`: 2 → 3
- `nuget/setup-nuget`: 2.0.1 → 4.0
- `richardrigutins/replace-in-files`: 2 → 3
- `peter-evans/create-pull-request`: 8.1.0 → 8.1.1

**NuGet dependency updates (all driver packages):**
- `nanoFramework.Runtime.Events`: 1.11.32 → 1.11.37
- `nanoFramework.System.Device.Gpio`: 1.1.57 → 1.1.62
- `nanoFramework.System.IO.Ports`: 1.1.132 → 1.1.139
- `UnitsNet.nanoFramework.*` (Temperature, RelativeHumidity, Pressure, Ratio, VolumeConcentration, Duration, Length, Illuminance): 5.75.0 → 5.75.1

**Affected drivers:** CST816D, HDC1080, LPS22HB, MHZ19B, PI4IOE5V6408, QMP6988, SHT3x, SHTC3, SSD1331, TCS34725, TSL2561, DriverBaseSPI, DriverBaseUART, and all DriverBaseInterfaces (Altitude, CO2, DewPoint, Gpio, Humidity, Infrared, Light, Pressure, Sensitivity, Temperature)

### Fixed

- 🐛 Fix NuGet restore command syntax in build workflow (0077e952)
- 🐛 Fix YAML indentation in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (8e17d084, a4c86da9, 99f1cf19)
- 🐛 Fix NuGet cache path configuration, settling on user home directory path (e63fc5b9)

### Security

- 🔒 Remove write permissions from weekly prerelease workflow, add explicit `checks` and `id-token` permissions (67ca104f, 82ec7e9b, eb71822b)
- 🔒 Add OIDC-based NuGet authentication to eliminate stored API key secrets

### Metrics

- Total Commits: 178
- Files Changed: 81
- Insertions: +1,290
- Deletions: -765
<!-- -------------------------------------------------------------- -->

## [v0.4.12-preview] - 2026-07-21

This prerelease introduces a fully automated weekly release pipeline with version bumping, AI-powered changelog generation, and NuGet package caching. It also brings NuGet dependency updates across all driver packages and the Meteostanice application.

### Added

- ✨ Add weekly prerelease workflow (`nanoframework_weekly_prerelease.yml`) for automated Sunday patch version bumps and PR creation (607039a)
- ✨ Add manual version bump workflow (`nanoframework_version_bump.yml`) with SemVer validation and major/minor/patch/custom options (6482bbc)
- ✨ Add AI-powered changelog generation using Git-Iris with Anthropic, triggered on release completion (ba37304, 258f85a)
- ✨ Add `VERSION` file as the single source of truth for project versioning (6482bbc)
- ✨ Add `Directory.Build.props` at repository root to enable `RestorePackagesWithLockFile` and `RestoreLockedMode` across all projects (f89f374)
- ✨ Add NuGet package caching (`actions/cache`) to build, release, and CodeQL workflows (40aee2b, ab4026a, daaf0c0)
- ✨ Add concurrency settings to build and CodeQL workflows to cancel redundant runs (2615654, f3c51aa)
- ✨ Add Mergify auto-merge and auto-approve rules for version bump PRs on `release/weekly-prerelease-version` and `release/manual-version-bump` branches (6482bbc)

### Changed

- 🔄 Bump `nanoFramework.Runtime.Events` from 1.11.32 to 1.11.37 (affects all drivers and Meteostanice) (bac32a1)
- 🔄 Bump `nanoFramework.System.Device.Gpio` from 1.1.57 to 1.1.62 (affects CST816D, DriverBaseInterfaces.Gpio, DriverBaseSPI, PI4IOE5V6408, SSD1331, TCS34725, Meteostanice) (bac32a1)
- 🔄 Bump `nanoFramework.System.Device.Spi` from 1.3.82 to 1.3.89 (affects SSD1331, DriverBaseSPI, Meteostanice) (bac32a1)
- 🔄 Bump `nanoFramework.System.IO.Ports` from 1.1.132 to 1.1.139 (affects DriverBaseUART, MHZ19B, Meteostanice) (bac32a1)
- 🔄 Bump `nanoFramework.Hardware.Esp32` from 1.6.37 to 1.6.40 (affects Meteostanice) (bac32a1)
- 🔄 Bump 8 `UnitsNet.nanoFramework.*` packages from 5.75.0 to 5.75.1: Duration, Illuminance, Length, Pressure, Ratio, RelativeHumidity, Temperature, VolumeConcentration (affects HDC1080, LPS22HB, MHZ19B, QMP6988, SHT3x, SHTC3, TCS34725, TSL2561, DriverBaseInterfaces, Meteostanice) (d3a279a)
- 🔄 Bump `actions/checkout` from 6.0.2 to 7.0.1
- 🔄 Bump `actions/cache` from 5.0.3 to 6
- 🔄 Bump `actions/setup-dotnet` from 5.1.0 to 6.0.0
- 🔄 Bump `actions/setup-java` from 5.2.0 to 5.6.0
- 🔄 Bump `actions/upload-artifact` from 6.0.0 to 7.0.1
- 🔄 Bump `nuget/setup-nuget` from 2.0.1 to 4.0
- 🔄 Bump `microsoft/setup-msbuild` from 2 to 3
- 🔄 Bump `peter-evans/create-pull-request` from 8.1.0 to 8.1.1
- 🔄 Bump `richardrigutins/replace-in-files` from 2 to 3
- 🔄 Bump `nanoframework/nanobuild` from 1.18 to 1.19
- 🔄 Bump `nanoframework/nanodu` from 1.0.26 to 1.0.27
- 🔄 Upgrade Mergify configuration to current format (a880c28)
- 🔄 Change default weekly version bump from minor to patch increment (4572f18, 7e047c7)
- 🔄 Move `Directory.Build.props` from `Meteostanice/` to repository root for solution-wide coverage (f89f374, 2164899)

### Fixed

- 🐛 Fix NuGet restore command syntax in build workflow (0077e95)
- 🐛 Fix YAML indentation in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (8e17d08, a4c86da, 99f1cf1)
- 🐛 Fix NuGet cache path configuration across build, release, and CodeQL workflows (47b891b, 3bc9173, e63fc5b, 9767855, fb1d3e8, 6b2da22)
- 🐛 Fix `nanoframework_build.yml` workflow configuration (da1731f)

### Metrics

- Total Commits: 155
- Files Changed: 81
- Insertions: +1,237
- Deletions: -765
<!-- -------------------------------------------------------------- -->

## [v0.4.11-preview] - 2026-07-12

This release overhauls the CI/CD pipeline with automated weekly prerelease versioning, NuGet package caching, OIDC-based NuGet publishing, and upgrades to .NET 10.0.x. All driver packages receive updated nanoFramework NuGet dependencies.

### Added

- ✨ Add weekly prerelease workflow (`nanoframework_weekly_prerelease.yml`) that auto-bumps the patch version every Sunday and creates a PR (607039a, PR #212)
- ✨ Add manual version bump workflow (`nanoframework_version_bump.yml`) with support for major, minor, patch, and custom SemVer inputs, including monotonic version validation (6482bbc, PR #214)
- ✨ Add `VERSION` file as single source of truth for project versioning
- ✨ Add `Directory.Build.props` at repo root for shared NuGet package restore configuration (f89f374)
- ✨ Add NuGet package caching via `actions/cache@v6` across build, CodeQL, and release workflows (40aee2b, ab4026a, 5ae73a3)
- ✨ Add concurrency groups with `cancel-in-progress: true` to build, CodeQL, and release workflows (26156540, f3c51aa)
- ✨ Add Mergify auto-approval and auto-merge rules for version bump PRs (`release/weekly-prerelease-version`, `release/manual-version-bump`) (6482bbc)
- ✨ Add AI-powered changelog generation via `hyperb1iss/git-iris@v2` with Anthropic provider, triggered on release publication and workflow_run (6482bbc, 86d3dd2)

### Changed

- 🔄 Upgrade .NET SDK from 9.0.x to 10.0.x in build workflow (da90cf3), CodeQL workflow (f40e260), and release workflow (45e8797)
- 🔄 Upgrade `nanoframework/nanobuild` from v1.18 to v1.20 with `usePreview: true` enabled (ff59e4f bumped to v1.19, then 45e8797 and f40e260 bumped to v1.20)
- 🔄 Upgrade Mergify configuration to current YAML format (a880c28)
- 🔄 Switch NuGet restore to locked mode (`-LockedMode` / `--locked-mode`) across all workflows
- 🔄 Change default weekly version bump from minor to patch increment (4572f18, 7e047c7)
- ♻️ Update 5 nanoFramework NuGet dependencies across all driver packages and Meteostanice (bac32a1):
  - `nanoFramework.Hardware.Esp32` 1.6.37 → 1.6.40
  - `nanoFramework.Runtime.Events` 1.11.32 → 1.11.37
  - `nanoFramework.System.Device.Gpio` 1.1.57 → 1.1.62
  - `nanoFramework.System.Device.Spi` 1.3.82 → 1.3.89
  - `nanoFramework.System.IO.Ports` 1.1.132 → 1.1.139
- 🔄 Bump GitHub Actions dependencies (net effect across range):
  - `actions/checkout` 6.0.2 → 7.0.0
  - `actions/setup-dotnet` 5.1.0 → 5.4.0
  - `actions/setup-java` 5.2.0 → 5.4.0
  - `actions/cache` 5.0.3 → 6
  - `actions/upload-artifact` 6.0.0 → 7.0.1
  - `nuget/setup-nuget` 2.0.1 → 4.0
  - `microsoft/setup-msbuild` 2 → 3
  - `richardrigutins/replace-in-files` 2 → 3
  - `peter-evans/create-pull-request` 8.1.0 → 8.1.1
  - `nanoframework/nanodu` 1.0.26 → 1.0.27

### Security

- 🔒 Switch release workflow NuGet publishing from a stored API key secret to OIDC-generated temporary API keys via `NuGet/login@v1`, using `id-token: write` permission (7682e58, eb71822)

### Fixed

- 🐛 Fix NuGet restore command syntax across workflows (0077e95)
- 🐛 Fix indentation in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (8e17d08, a4c86da, 99f1cf1)

### Metrics

- Total Commits: 131
- Files Changed: 48
- Insertions: +975
- Deletions: -563
<!-- -------------------------------------------------------------- -->

## [v0.4.10-preview] - 2026-07-05

Overhaul CI/CD pipelines with automated weekly prerelease versioning, OIDC-based NuGet authentication, NuGet package caching, and .NET 10 migration. Update nanoFramework driver dependencies across all packages.

### Added

- ✨ Add `nanoframework_weekly_prerelease.yml` workflow for automated weekly patch version bumps, triggered on a Sunday schedule or manually via `workflow_dispatch` (607039a)
- ✨ Add `nanoframework_version_bump.yml` workflow for manual major/minor/patch/custom version bumps with SemVer validation (6482bbcf)
- ✨ Add `VERSION` file to track the current release version (`0.4.10`), used by both version bump and release workflows
- ✨ Add root-level `Directory.Build.props` enabling `RestorePackagesWithLockFile` and `RestoreLockedMode` for reproducible builds across all projects (f89f3746)
- ✨ Add NuGet package caching via `actions/cache@v6` in build, release, and CodeQL workflows (40aee2b1, ab4026a4)
- ✨ Add concurrency groups with `cancel-in-progress: true` to build and CodeQL workflows to prevent redundant parallel runs (26156540, f3c51aad)
- ✨ Add `workflow_run` trigger to changelog workflow, enabling automatic changelog generation after releases (86d3dd2b)
- ✨ Add AI-powered changelog generation using `hyperb1iss/git-iris@v2` with Anthropic, replacing `craicoverflow/install-git-chglog` (258f85ad)
- ✨ Add Mergify auto-merge rules for `release/weekly-prerelease-version` and `release/manual-version-bump` PR branches

### Changed

- 🔄 Upgrade .NET SDK from 9.0 to 10.0 across build, release, and CodeQL workflows
- 🔄 Upgrade `nanoframework/nanobuild` from v1.18 to v1.20 with `usePreview: true` enabled
- 🔄 Upgrade `actions/checkout` from v6.0.2 to v7.0.0
- 🔄 Upgrade `actions/setup-dotnet` from v5.1.0 to v5.4.0
- 🔄 Upgrade `actions/setup-java` from v5.2.0 to v5.4.0
- 🔄 Upgrade `microsoft/setup-msbuild` from v2 to v3
- 🔄 Upgrade `nuget/setup-nuget` from v2.0.1 to v4.0
- 🔄 Upgrade `actions/upload-artifact` from v6.0.0 to v7.0.1
- 🔄 Upgrade `actions/cache` from v5.0.3 to v6
- 🔄 Upgrade `peter-evans/create-pull-request` from v8.1.0 to v8.1.1
- 🔄 Upgrade `richardrigutins/replace-in-files` from v2 to v3
- 🔄 Upgrade `nanoframework/nanodu` from v1.0.26 to v1.0.27
- 🔄 Change release workflow trigger: now fires on `push` to master when `VERSION` file changes, replacing manual GitHub Release events
- 🔄 Change default version bump strategy from `minor` to `patch` in weekly prerelease workflow (7e047c7b)
- 🔄 Upgrade Mergify configuration to current format, replacing deprecated syntax (#202)
- 🔄 Switch NuGet restore to `-LockedMode` for deterministic dependency resolution
- 🔄 Update nanoFramework NuGet dependencies across all driver packages:
  - `nanoFramework.Runtime.Events`: 1.11.32 → 1.11.37 (CST816D, DriverBaseInterfaces.Gpio, PI4IOE5V6408, TCS34725)
  - `nanoFramework.System.Device.Gpio`: 1.1.57 → 1.1.62 (CST816D, DriverBaseInterfaces.Gpio, PI4IOE5V6408, TCS34725, Meteostanice)
  - `nanoFramework.System.Device.Spi`: 1.3.82 → 1.3.89 (DriverBaseSPI, SSD1331, Meteostanice)
  - `nanoFramework.System.IO.Ports`: 1.1.132 → 1.1.139 (DriverBaseUART, MHZ19B)
  - `nanoFramework.Hardware.Esp32`: 1.6.37 → 1.6.40 (Meteostanice)

### Security

- 🔒 Replace long-lived `secrets.NUGET_KEY` API key with OIDC-based temporary token authentication via `NuGet/login@v1` in the release workflow (7682e586)
- 🔒 Add `id-token: write` permission to weekly prerelease workflow for OIDC token federation (eb71822b)

### Removed

- 🔥 Remove per-project `Meteostanice/Directory.Build.props` in favor of root-level shared configuration (2164899c)

### Metrics

- Total Commits: 131
- Files Changed: 48
- Insertions: +917
- Deletions: -563
<!-- -------------------------------------------------------------- -->

## [v0.4.9-preview] - 2026-06-28

CI/CD overhaul: adds automated weekly prerelease and manual version bump workflows, migrates changelog generation from `git-chglog` to AI-powered `git-iris`, upgrades all GitHub Actions dependencies, and introduces NuGet package caching with locked restore across all workflows. 120 commits across 11 files (+715, -410).

### Added

- ✨ Add `nanoframework_weekly_prerelease.yml` workflow for automated weekly patch-version bumps via scheduled cron (`0 0 * * 0`) or manual dispatch (607039a)
- ✨ Add `nanoframework_version_bump.yml` workflow for manual version bumps (major, minor, patch, or custom SemVer) with validation and automatic PR creation (6482bbc)
- ✨ Add `VERSION` file to track the current release version (`0.4.9`), used by release and changelog workflows as the source of truth
- ✨ Add root-level `Directory.Build.props` enabling `RestorePackagesWithLockFile` and `RestoreLockedMode` for deterministic NuGet restores
- ✨ Add NuGet package caching step (`actions/cache@v6`) to build, release, and CodeQL workflows, keyed on `packages.lock.json` hashes
- ✨ Add concurrency groups to build (`build-${{github.ref}}`) and CodeQL (`analyze-${{github.ref}}`) workflows to cancel redundant runs
- ✨ Add `workflow_run` trigger to changelog workflow so it fires automatically after the release workflow completes
- ✨ Add Mergify rules for automatic merge, approval, and update of version bump PRs matching `release/(weekly-prerelease-version|manual-version-bump)` branches
- ✨ Add `softprops/action-gh-release@v3` step to release workflow for creating or updating GitHub Releases with NuGet artifacts
- ✨ Add `NuGet/login@v1` OIDC-based NuGet authentication step in release workflow, replacing static API key for `nuget.org` pushes

### Changed

- 🔄 Migrate changelog generation from `craicoverflow/install-git-chglog@v1.0.0` / `git-chglog` to `hyperb1iss/git-iris@v2` with Anthropic API (258f85a)
- 🔄 Upgrade `actions/checkout` from v6.0.2 to v7.0.0 across all workflows
- 🔄 Upgrade `actions/setup-dotnet` from v5.1.0 to v5.3.0 and .NET SDK target from 9.0.x to 10.0.x
- 🔄 Upgrade `actions/cache` from v5.0.3 to v6
- 🔄 Upgrade `nuget/setup-nuget` from v2.0.1 to v4.0
- 🔄 Upgrade `microsoft/setup-msbuild` from v2 to v3
- 🔄 Upgrade `actions/upload-artifact` from v6.0.0 to v7.0.1
- 🔄 Upgrade `actions/setup-java` from v5.2.0 to v5.3.0
- 🔄 Upgrade `richardrigutins/replace-in-files` from v2 to v3
- 🔄 Upgrade `peter-evans/create-pull-request` from v8.1.0 to v8.1.1
- 🔄 Upgrade `nanoframework/nanobuild` from v1.18 to v1.20 with `usePreview: true`
- 🔄 Upgrade `nanoframework/nanodu` from v1.0.26 to v1.0.27
- 🔄 Upgrade Mergify configuration to current format, normalizing indentation (a880c28, PR #202)
- 🔄 Refactor release workflow to support `push` triggers on `VERSION` file changes and `workflow_call` inputs for prerelease/version parameters
- 🔄 Add explicit `permissions` block (contents, pull-requests, packages, id-token) and `concurrency` group to release workflow
- 🔄 Switch NuGet restore to locked mode (`-LockedMode` flag) in build, release, and CodeQL workflows
- 🔄 Change default weekly version bump fragment from `minor` to `patch` (4572f18)

### Fixed

- 🐛 Fix indentation in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` cache restore-keys blocks (8e17d08, a4c86da, 99f1cf1)
- 🐛 Fix NuGet restore command syntax in build workflow (0077e95)

### Removed

- 🔥 Remove `craicoverflow/install-git-chglog@v1.0.0` action and `git-chglog` CLI usage from changelog workflow, replaced by Git-Iris
- 🔥 Remove Mergify `delete_head_branch` rule (branch cleanup now handled by PR settings)
- 🔥 Remove release workflow `matrix.configuration` strategy in favor of a single `Release` build with `Configuration` set as an env var
<!-- -------------------------------------------------------------- -->

## [v0.4.8-preview] - 2026-06-21

This release overhauls the CI/CD pipeline with automated version management, weekly prerelease publishing, NuGet caching, and upgrades all GitHub Actions dependencies to their latest versions.

### Added

- ✨ Add **weekly prerelease workflow** (`nanoframework_weekly_prerelease.yml`) that auto-bumps the patch version every Sunday and creates a PR (607039a)
- ✨ Add **manual version bump workflow** (`nanoframework_version_bump.yml`) supporting major, minor, patch, and custom version fragments with downgrade protection (6482bbc)
- ✨ Add `VERSION` file as the single source of truth for project versioning (5f80bb9)
- ✨ Add `Directory.Build.props` at the repository root for centralized MSBuild configuration with NuGet lock-file support (f89f374)
- ✨ Add **NuGet package caching** to build, release, and CodeQL workflows for faster CI runs (40aee2b, ab4026a, daaf0c0)
- ✨ Add **concurrency groups** to build and CodeQL workflows to cancel redundant runs (2615654, f3c51aa)
- ✨ Add **NuGet OIDC authentication** (`NuGet/login@v1`) to the release workflow for keyless NuGet.org publishing (eb71822)
- ✨ Add `workflow_run` trigger to changelog workflow so it executes automatically after a release (86d3dd2)
- ✨ Add GitHub Release creation via `softprops/action-gh-release@v3` in the release workflow with prerelease asset support (0f7751a)

### Changed

- 🔄 Upgrade .NET SDK from **9.0.x** to **10.0.x** across all workflows (45e8797)
- 🔄 Upgrade `actions/checkout` from v6.0.2 to **v7.0.0** (8885662)
- 🔄 Upgrade `actions/setup-dotnet` from v5.1.0 to **v5.3.0** (6b6096d)
- 🔄 Upgrade `nuget/setup-nuget` from v2.0.1 to **v4.0** (2bf2101)
- 🔄 Upgrade `microsoft/setup-msbuild` from v2 to **v3** (d5c4855)
- 🔄 Upgrade `actions/upload-artifact` from v6.0.0 to **v7.0.1** (dd1d7ec)
- 🔄 Upgrade `actions/cache` from v5.0.3 to **v5.0.5** (d3b1f82)
- 🔄 Upgrade `richardrigutins/replace-in-files` from v2 to **v3** (5cade87)
- 🔄 Upgrade `peter-evans/create-pull-request` from v8.1.0 to **v8.1.1** (43819d1)
- 🔄 Upgrade `nanoframework/nanobuild` from v1.18 to **v1.19** with `usePreview: true` (ff59e4f)
- 🔄 Upgrade `nanoframework/nanodu` from v1.0.26 to **v1.0.27** (1964f41)
- 🔄 Upgrade `actions/setup-java` from v5.2.0 to **v5.3.0** (a9fde80)
- 🔄 Replace `craicoverflow/install-git-chglog` with **`hyperb1iss/git-iris@v2`** (Anthropic-powered) for AI-generated changelogs (258f85a)
- 🔄 Upgrade Mergify configuration to current format and add auto-merge rules for version bump PRs (a880c28, 0d43193)
- 🔄 Switch NuGet restore to **`-LockedMode`** for reproducible builds across all workflows
- 🔄 Change default version bump fragment from `minor` to `patch` (4572f18)
- 🔄 Move `Directory.Build.props` from `Meteostanice/` to repository root for broader coverage (2164899, f89f374)
- 🔄 Refactor release workflow to support multiple triggers: release events, `VERSION` file pushes, and `workflow_call` (07423ba)

### Fixed

- 🐛 Fix NuGet cache path configuration across build, release, and CodeQL workflows (9767855, fb1d3e8, 6b2da22)
- 🐛 Fix YAML indentation errors in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (8e17d08, a4c86da, 99f1cf1)
- 🐛 Fix `nuget restore` command syntax for nanoFramework solution (0077e95)
- 🐛 Fix prerelease version detection and tag preservation in release workflow (40d7b23, 5ab4dcd)
- 🐛 Fix version regression from 0.5.0 back to 0.4.x series (b01fa23)

### Security

- 🔒 Add explicit **permissions scoping** (`contents`, `packages`, `checks`, `id-token`) to release and prerelease workflows (67ca104, 82ec7e9, eb71822)
- 🔒 Enable **OIDC-based NuGet authentication** replacing static API key usage in the release pipeline
<!-- -------------------------------------------------------------- -->

## [v0.4.7-preview] - 2026-06-14

CI/CD overhaul: centralize version management in a `VERSION` file, add automated weekly prerelease and manual version bump workflows, upgrade all GitHub Actions to current versions (.NET 10, MSBuild v3, NuGet v4), switch to OIDC-based NuGet authentication, and enable locked-mode NuGet restore for reproducible builds.

### Added

- ✨ Add weekly prerelease workflow (`nanoframework_weekly_prerelease.yml`) that auto-bumps the patch version every Sunday and opens a PR to trigger the release pipeline (607039a)
- ✨ Add manual version bump workflow (`nanoframework_version_bump.yml`) supporting major, minor, patch, and custom SemVer increments with validation (6482bbc)
- ✨ Add `VERSION` file as the central source of truth for release versioning, replacing sole reliance on Git tags (afb3797)
- ✨ Add `Directory.Build.props` at the repository root to enable locked-mode NuGet restoration (`RestorePackagesWithLockFile`, `RestoreLockedMode`) for deterministic builds (4538ffb)
- ✨ Add NuGet package caching steps (via `actions/cache@v5`) to build, CodeQL, and release workflows for faster CI runs (daaf0c0, ab4026a, 40aee2b)
- ✨ Add concurrency controls to build and CodeQL workflows to cancel redundant in-progress runs (26156540, f3c51aa)
- ✨ Add OIDC-based NuGet authentication (`NuGet/login`) in the release workflow, replacing static `NUGET_KEY` secrets with temporary API keys (eb71822)
- ✨ Add Mergify rules for auto-merging and auto-approving version bump PRs on `release/weekly-prerelease-version` and `release/manual-version-bump` branches

### Changed

- 🔄 Upgrade .NET SDK from 9.0.x to 10.0.x across build, CodeQL, and release workflows
- 🔄 Bump `microsoft/setup-msbuild` from v2 to v3 in all workflows
- 🔄 Bump `nuget/setup-nuget` from v2.0.1 to v4.0 across build, CodeQL, dependabot, and release workflows
- 🔄 Bump `nanoframework/nanobuild` from v1.18 to v1.20 with `usePreview: true` enabled
- 🔄 Bump `actions/checkout` from v6.0.2 to v6.0.3
- 🔄 Bump `actions/setup-dotnet` from v5.1.0 to v5.3.0
- 🔄 Bump `actions/upload-artifact` from v6.0.0 to v7.0.1
- 🔄 Bump `actions/cache` from v5.0.3 to v5.0.5
- 🔄 Bump `richardrigutins/replace-in-files` from v2 to v3
- 🔄 Bump `peter-evans/create-pull-request` from v8.1.0 to v8.1.1
- 🔄 Downgrade `nanoframework/nanodu` from v1.0.26 to v1.0.25 in the dependabot workflow (5b3a6e9)
- 🔄 Switch changelog generation from `git-chglog` to **Git-Iris** (AI-powered, Anthropic-backed) with custom Keep a Changelog instructions (258f85a)
- 🔄 Expand release workflow triggers to include `push` on `VERSION` file changes and `workflow_call` for reusable invocation
- 🔄 Add `-LockedMode` flag to `nuget restore` commands for reproducible dependency resolution
- 🔄 Upgrade Mergify configuration to current format and fix "Dependenabot" typo (a880c28)
- 🔄 Change default version bump fragment from minor to patch (7e047c7)
- 🔄 Remove matrix strategy from release workflow (only Release configuration was used)

### Fixed

- 🐛 Fix various YAML indentation issues in `codeql.yml`, `nanoframework_build.yml`, and `nanoframework_release.yml` (8e17d08, a4c86da, 99f1cf1)
- 🐛 Fix `nuget restore` command syntax in build workflow (0077e95)
- 🐛 Fix NuGet cache path configuration, settling on `~/.nuget/packages` after iterating through several path strategies (e63fc5b)

### Security

- 🔒 Remove broad `write` permissions from weekly prerelease workflow and scope to `contents: write`, `pull-requests: write`, `checks: read` (67ca104, 82ec7e9)
- 🔒 Add explicit `id-token: write` permission to release workflow for OIDC-based NuGet publishing (eb71822)

### Metrics

- Total Commits: 122
- Files Changed: 11
- Insertions: +612
- Deletions: -199
<!-- -------------------------------------------------------------- -->

