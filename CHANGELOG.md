# Changelog

All notable changes to this project are documented in this file.
The format is loosely based on [Keep a Changelog](https://keepachangelog.com/).

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
