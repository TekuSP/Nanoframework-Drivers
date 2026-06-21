# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

