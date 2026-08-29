# 변경 이력

*[English](CHANGELOG.md) | 한국어*

이 프로젝트의 주요 변경 사항을 기록합니다.
형식은 [Keep a Changelog](https://keepachangelog.com/) 를 느슨하게 따릅니다.

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
