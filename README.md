# DH.IME.Spot

*한국어 | [English](README.en.md)*

멀티모니터 환경에서 현재 **한/영 IME 상태**를 마우스 커서, 활성 창, 각 모니터
근처에 작은 배지로 표시해 주는 가벼운 Windows 트레이 유틸리티입니다.
지금 어느 언어로 입력되는지 한눈에 알 수 있습니다.

![DH.IME.Spot](docs/screenshot.png)

- **버전:** 1.0.0.2
- **플랫폼:** Windows 10 이상 (x86/x64)
- **런타임:** .NET Framework 4.6 (Windows 10 이상 기본 포함)
- **의존성:** 없음 (NuGet 패키지 미사용)

## 기능

- **IME 상태 배지** — per-pixel alpha 레이어드 배지에 사용자 지정 가능한 `Hangul glyph` / `Latin glyph` 표시.
- **표시 방식 3가지, 각각 독립 토글:**
  - *활성 창 코너* — 포커스된 창의 지정 코너에 배지 고정.
  - *커서 컴패니언* — 마우스 커서를 따라다님 (60Hz 부드러운 추적, 전역 마우스 훅 미사용, `Work area` / `Full monitor` 선택 가능).
  - *모니터별 위젯* — 현재 모니터 또는 모든 모니터의 지정 코너에 고정 배지 (`Work area` / `Full monitor` 선택 가능).
- **Lock 표시 2채널**
  - `LockPill` — 글리프 아래의 작은 색 점 행
  - `Track corner badge` — 배지 모서리에 붙는 lock dot
- **Lock key별 독립 옵션** — `Caps/Num/Scroll` 각각 on/off, corner, dot size, dot color 설정.
- **기본 배지 opacity 100%** — 글자·그림자 alpha도 배경과 함께 조정.
- **커서 배지 크기** 옵션 (25%–150%).
- **모니터별 DPI 대응.**
- **전체화면 자동 숨김** — exclusive / borderless 전체화면 앱 위에서는 배지가 숨겨짐.
- **시작 프로그램 등록** 토글.
- 트레이 메뉴 **Pause overlay** (런타임 전용, 저장 안 함).
- tray icon **double click -> Options**.
- 설정은 **레지스트리**에 저장 — 설정 파일을 만들지 않음.

## 설치 / 실행

1. 아래 방법으로 빌드하거나 릴리스에서 `DH.IME.Spot.exe` 를 받습니다.
2. `DH.IME.Spot.exe` 실행 → 알림 영역(시스템 트레이)에 아이콘이 나타납니다.
3. 트레이 아이콘 우클릭 또는 double click → **Options…** 에서 표시 방식, lock 옵션, glyph, 투명도, 크기, 시작 등록 설정.
4. 우클릭 → **Exit** 로 종료.

설치 관리자 없이 단일 실행 파일(~53 KB)이며 관리자 권한이 필요 없습니다.

### 다운로드 후 실행이 차단될 때

릴리스 zip은 인터넷에서 받은 파일이라 Windows가 차단 표시(Mark of the Web)를
붙입니다. 압축을 풀면 exe에도 전파되어 SmartScreen이 실행을 막을 수 있습니다.
(코드 서명이 없는 개인 유틸리티라 발생하는 정상 동작입니다.)

풀기 **전에** zip에서 차단을 해제하는 것이 가장 깔끔합니다:

1. 받은 `DH.IME.Spot-vX.X.X.X.zip` 우클릭 → **속성**
2. 하단의 **[차단 해제] / [Unblock]** 체크 → **확인**
3. 그 다음 압축을 풀면 됩니다.

이미 풀었다면 exe에 같은 방법을 쓰거나, PowerShell에서:

```powershell
Get-ChildItem -Recurse | Unblock-File
```

SmartScreen 파란 창이 떠도 **추가 정보 → 실행**으로 진행할 수 있습니다.

![Options v1.0.0.2](docs/options2.png)

## 빌드

.NET Framework 4.6 타게팅 팩이 설치된 Visual Studio 2022(또는 MSBuild)가 필요합니다.

```
MSBuild DH.IME.SpotSol\workspace\DH.IME.Spot\DH.IME.Spot.csproj /t:Rebuild /p:Configuration=Release
```

출력: `DH.IME.SpotSol\output\Release\DH.IME.Spot.exe`

솔루션 파일: `DH.IME.SpotSol\DH.IME.SpotSol.slnx` (VS2022 `.slnx` 형식).
프로젝트는 classic(non-SDK) `.csproj`, C# 7.3, `WinExe` 입니다.

## 동작 원리

- IME 상태는 포커스된 창의 기본 IME 창에서 읽습니다
  (`ImmGetDefaultIMEWnd` + `WM_IME_CONTROL` / `IMC_GETCONVERSIONMODE`, `IME_CMODE_NATIVE` 비트).
- 포커스 변화는 좁은 단일 이벤트 `SetWinEventHook`
  (`EVENT_SYSTEM_FOREGROUND`, `EVENT_OBJECT_FOCUS`) 으로 추적하고,
  창 이벤트가 발생하지 않는 한/영 토글은 짧은 주기 폴링으로 보완합니다.
- 배지는 테두리 없는 `WS_EX_LAYERED` 창에 `UpdateLayeredWindow` 로 그립니다.

## 설정 (레지스트리)

설정 키: `HKEY_CURRENT_USER\SOFTWARE\DHTOOL\DH.IME.Spot`

| 값 | 형식 | 의미 |
|---|---|---|
| `BackgroundAlpha` | DWORD | 배지 배경 alpha, 26–255 (≈10%–100%) |
| `ActiveWindowEnabled` | DWORD | 활성 창 코너 배지 on/off |
| `ActiveWindowCorner` | REG_SZ | `TopLeft` / `TopRight` / `BottomLeft` / `BottomRight` |
| `CursorEnabled` | DWORD | 커서 컴패니언 배지 on/off |
| `CursorBadgeSize` | REG_SZ | `Scale25` … `Scale150` |
| `CursorBoundsMode` | REG_SZ | `WorkArea` / `MonitorArea` |
| `MonitorWidgetEnabled` | DWORD | 모니터별 위젯 배지 on/off |
| `MonitorWidgetCorner` | REG_SZ | 모니터별 위젯 코너 |
| `MonitorWidgetScope` | REG_SZ | `CurrentMonitor` / `AllMonitors` |
| `MonitorWidgetBoundsMode` | REG_SZ | `WorkArea` / `MonitorArea` |
| `BadgeLockPill` | DWORD | 글리프 아래 `LockPill` 표시 |
| `ShowCapsLock` / `ShowNumLock` / `ShowScrollLock` | DWORD | 각 lock key 추적 on/off |
| `CapsLockCorner` / `NumLockCorner` / `ScrollLockCorner` | REG_SZ | 각 lock dot의 corner |
| `CapsLockDotSize` / `NumLockDotSize` / `ScrollLockDotSize` | DWORD | 각 lock dot size |
| `CapsLockDotColor` / `NumLockDotColor` / `ScrollLockDotColor` | REG_SZ | 각 lock dot color |
| `HangulGlyph` / `LatinGlyph` | REG_SZ | 배지와 flash에 쓰는 사용자 glyph |
| `FlashEnabled` 외 `Flash*` 값들 | DWORD / REG_SZ | change flash 관련 옵션 |
| `FadeIdleEnabled` 외 `Fade*` 값들 | DWORD / REG_SZ | idle fade 관련 옵션 |
| `PollIntervalMs` | DWORD | IME polling interval |

시작 프로그램 등록은 별도로
`HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run` 의
`DH.IME.Spot` 값으로 저장됩니다.

### 완전 제거

`DH.IME.Spot.exe` 를 삭제한 뒤 다음을 제거합니다:

```
HKCU\SOFTWARE\DHTOOL\DH.IME.Spot
HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run  ->  "DH.IME.Spot" 값
```

## 라이선스

[MIT](LICENSE) © 2026 CYBERKDH
