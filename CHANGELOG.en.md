# Changelog

*[한국어](CHANGELOG.md) | English*

All notable changes to this project are documented in this file.
The format is loosely based on [Keep a Changelog](https://keepachangelog.com/).

## [1.0.0.2] - 2026-09-04

Update focused on refining the IME badge and lock indicators, plus expanding placement and display options.

### Added
- Two lock-indicator channels:
  - `LockPill` - a small row of colored dots under the main badge glyph
  - `Track corner badge` - lock dots attached to the outer corners of the badge
- Independent settings for `Caps Lock`, `Num Lock`, and `Scroll Lock`:
  - tracking on/off
  - corner position
  - dot size (`6 / 8 / 10 / 12 / 16 / 20 px`)
  - dot color (`Amber / Orange / Red / Pink / Purple / Indigo / Blue / Teal / Green / Lime / Gray / White`)
- New placement bounds options:
  - `Cursor companion`: `Work area` / `Full monitor`
  - `Per-monitor widget`: `Work area` / `Full monitor`
- User-configurable badge glyphs:
  - `Hangul glyph`
  - `Latin glyph`
  - the main badge and IME switch flash share the same glyph settings

### Changed
- Default badge opacity is now `100%`.
- Default cursor badge size remains `75%`.
- Default cursor bounds are now `Full monitor`.
- Tray icon `double click` now opens `Options` instead of `About`.
- The `Lock keys` and `Badge modes` tabs in `Options` were reorganized around the current feature set.

### Removed
- Removed the `EdgeBar` mode.
  - The source is preserved, but excluded from the build.
- Removed the `MicroDot` mode.
  - The source is preserved, but excluded from the build.

### Notes
- `Active window corner` still uses the target window rect, with final clamping against the work area.
- This version is still Korean-IME-only.

## [1.0.0.1] - 2026-08-29

First public release.

### Added
- Tray (NotifyIcon) resident app that detects the current Korean/English IME state
  via `ImmGetDefaultIMEWnd` + `WM_IME_CONTROL`.
- IME state badge rendered on per-pixel alpha layered windows (`UpdateLayeredWindow`),
  with `K` / `E` glyph, rounded body, and soft drop shadow.
- Three independently toggleable placement modes:
  - Active window corner
  - Cursor companion (60 Hz `SetWindowPos` tracking, no global mouse hook)
  - Per-monitor widget (all monitors or primary only, selectable corner)
- Options window with live apply (no restart): opacity, cursor badge size,
  per-mode enable/corner, run-at-startup.
- Adjustable background opacity 10 %-100 %; glyph and shadow alpha fade with it.
- Cursor badge size 25 %-150 %.
- Per-monitor DPI awareness.
- Full-screen detection - badges hide over exclusive / borderless full-screen apps.
- "Pause overlay" tray command (runtime only).
- Run-at-startup via `HKCU\...\CurrentVersion\Run`; first-run seeding registers it by default.
- Registry-only settings under `HKCU\SOFTWARE\DHTOOL\DH.IME.Spot` (no config files, zero NuGet deps).

### Performance
- IME query timeout trimmed and focus events debounced/coalesced.
- Monitor/DPI metrics cached per `HMONITOR`, invalidated on display changes
  (`SystemEvents.DisplaySettingsChanged`).
- Move-only fast path skips layered-window content re-upload when only the position changes.
- Adaptive cursor timer slows to ~8 Hz while the cursor is idle.

### Notes
- Korean IME only in this version. Support for other languages' IMEs
  (Japanese, Chinese, generic layouts) is deferred to a future release.

[1.0.0.1]: https://github.com/cyberkdh/DH.IME.Spot/releases/tag/v1.0.0.1
[1.0.0.2]: https://github.com/cyberkdh/DH.IME.Spot/releases/tag/v1.0.0.2
