# 변경 이력

*한국어 | [English](CHANGELOG.en.md)*

이 프로젝트의 주요 변경 사항을 기록합니다.
형식은 [Keep a Changelog](https://keepachangelog.com/) 를 느슨하게 따릅니다.

## [1.0.0.2] - 2026-09-04

IME 배지와 lock 표시 체계를 다듬고, 배치/표시 옵션을 확장한 업데이트.

### 추가
- Lock 표시 2채널:
  - `LockPill` - 배지 글리프 아래의 작은 색 점 행
  - `Track corner badge` - 배지 바깥 모서리에 붙는 lock dot
- `Caps Lock`, `Num Lock`, `Scroll Lock` 각각에 대해 독립 설정 추가:
  - 추적 on/off
  - corner 위치
  - dot size (`6 / 8 / 10 / 12 / 16 / 20 px`)
  - dot color (`Amber / Orange / Red / Pink / Purple / Indigo / Blue / Teal / Green / Lime / Gray / White`)
- 배치 기준(bounds) 옵션 추가:
  - `Cursor companion`: `Work area` / `Full monitor`
  - `Per-monitor widget`: `Work area` / `Full monitor`
- 배지 글리프 사용자 변경 옵션 추가:
  - `Hangul glyph`
  - `Latin glyph`
  - 배지 본문과 IME switch flash가 같은 glyph 설정을 사용

### 변경
- 기본 배지 opacity를 `100%`로 조정.
- 기본 cursor badge size는 `75%` 유지.
- 기본 cursor bounds는 `Full monitor`로 변경.
- tray icon `double click` 동작을 `About`에서 `Options` 열기로 변경.
- `Options` 창의 `Lock keys` / `Badge modes` 탭 구성을 현재 기능 기준으로 재정리.

### 제거
- `EdgeBar` 모드 제거.
  - 소스는 보존하되 빌드에서 제외.
- `MicroDot` 모드 제거.
  - 소스는 보존하되 빌드에서 제외.

### 참고
- `Active window corner` 배치는 기존처럼 window rect를 기준으로 하며, 최종 clamp는 work area 기준을 유지.
- 이번 버전도 한국어 IME 전용.

## [1.0.0.1] - 2026-08-29

최초 공개 릴리스.

### 추가
- `ImmGetDefaultIMEWnd` + `WM_IME_CONTROL` 로 현재 한/영 IME 상태를 감지하는
  트레이(NotifyIcon) 상주 앱.
- per-pixel alpha 레이어드 창(`UpdateLayeredWindow`)에 `K` / `E` 글자, 둥근 본체,
  부드러운 드롭 섀도우로 IME 상태 배지 렌더링.
- 독립 토글되는 표시 방식 3가지:
  - 활성 창 코너
  - 커서 컴패니언 (60Hz `SetWindowPos` 추적, 전역 마우스 훅 미사용)
  - 모니터별 위젯 (모든 모니터 또는 주 모니터, 코너 선택)
- 재시작 없이 즉시 적용되는 Options 창: 투명도, 커서 배지 크기,
  방식별 on/off·코너, 시작 프로그램 등록.
- 배경 투명도 10%–100% 조절, 글자·그림자 alpha 도 함께 페이드.
- 커서 배지 크기 25%–150%.
- 모니터별 DPI 대응.
- 전체화면 감지 — exclusive / borderless 전체화면 앱 위에서는 배지 숨김.
- 트레이 "Pause overlay" 명령 (런타임 전용).
- `HKCU\...\CurrentVersion\Run` 으로 시작 프로그램 등록, 최초 실행 시 기본 등록(seeding).
- 설정은 `HKCU\SOFTWARE\DHTOOL\DH.IME.Spot` 레지스트리 전용 (설정 파일 없음, NuGet 의존성 0).

### 성능
- IME 조회 타임아웃 단축, 포커스 이벤트 디바운스/코얼레스.
- 모니터/DPI 메트릭을 `HMONITOR` 별로 캐싱, 디스플레이 변경 시 무효화
  (`SystemEvents.DisplaySettingsChanged`).
- 위치만 바뀔 때 레이어드 창 콘텐츠 재전송을 건너뛰는 move-only 경로.
- 커서가 유휴일 때 커서 타이머를 약 8Hz 로 감속.

### 참고
- 이번 버전은 한국어 IME 전용. 다른 언어 IME(일본어·중국어·일반 레이아웃) 지원은
  다음 릴리스로 이월.

[1.0.0.1]: https://github.com/cyberkdh/DH.IME.Spot/releases/tag/v1.0.0.1
[1.0.0.2]: https://github.com/cyberkdh/DH.IME.Spot/releases/tag/v1.0.0.2
