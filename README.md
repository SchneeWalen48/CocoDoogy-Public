<div align="center">

# 🎮 CocoDoogy  
### 소코반 규칙을 기반으로 한 3D 모바일 퍼즐 게임
<a href="https://youtu.be/UCx8Xok3t5I">
  <img width="100" height="100" alt="Youtube_logo"
    src="https://github.com/user-attachments/assets/2aa6f449-7ffa-4dd2-9086-232f5499456f" />
</a>

<br><br>

<table>
  <tr>
    <td align="center" width="33%">
      <img alt="Play_img" src="https://github.com/user-attachments/assets/dd34c8ad-a9d2-4054-9770-303827f1ca54" width="100%" />
      <br/>
      <b>퍼즐 플레이</b>
    </td>
    <td align="center" width="33%">
      <img alt="Env_img" src="https://github.com/user-attachments/assets/1d9420ef-6cd9-45b8-8d9c-3cdee9d1de95" width="100%" />
      <br/>
      <b>기믹</b>
    </td>
    <td align="center" width="33%">
      <img alt="UI_img" src="https://github.com/user-attachments/assets/1101aa67-0672-49a3-81bf-6977f11d6ee3" width="100%" />
      <br/>
      <b>도움말</b>
    </td>
  </tr>
</table>

<br>

🐶 **기업협약 프로젝트로 진행된 소코반 기반 모바일 퍼즐 게임**  
🐶 **다양한 환경·상호작용 기믹을 결합한 3D 퍼즐 플레이가 특징**

</div>

<br><br><br>

---

## 📋 Table of Contents

- [Tech Stack](#tech-stack)
- [Overview](#overview)
- [Architecture](#architecture)
- [Core Systems](#core-systems)
  - [Player Movement & Input](#player-movement--input)
  - [PushableObjects (Puzzle Rule Core)](#pushableobjects-puzzle-rule-core)
  - [Animal Friends System & Environment](#animal-friends-system--environment)
  - [Signal System](#signal-system)
- [Shared Environment Systems](#shared-systems)
  - [Shockwave System](#shockwave)
  - [Range Visualization](#ringrange)
- [Supporting Systems](#support-systems)
  - [Game Info System](#info-system)
  - [Camera & Player Experience](#camera--ux)
  - [Treasure & Stage UI](#treasure--stage-ui)
  - [Build & Tooling](#tooling)
- [Developer](#developer)
  
<br><br>

---

<a id="tech-stack"></a>
## 🛠️ 기술 스택

- **Language :** C#  
- **Engine :** Unity 6
- **Version Control :** GitHub (Fork 기반 협업)

<br>

---

<a id="overview"></a>
## 🎯 Overview

<strong>CocoDoogy(코코두기)</strong>는 소코반 규칙을 기반으로 한 **그리드 퍼즐 게임**으로, 플레이어 이동과 다양한 환경·상호작용 기믹을 결합해 퍼즐 규칙을 확장한 **3D 모바일 퍼즐 게임**입니다.

- 플랫폼: Android  
- 개발 엔진: Unity 6
- 개발 기간: 2025.10.16 ~ 2025.12.09  
- 프로젝트 형태: 기업협약 팀 프로젝트 (개발 6명 / 기획 4명)

> 본 README에는 팀 프로젝트 중 제가 맡은 **퍼즐 규칙 시스템, 플레이어 이동, 환경 기믹 아키텍처를 전담 설계·구현**파트가 정리되어 있습니다.

<br><br>

---

<a id="point"></a>
## ⚙️ Design Notes
- **Strategy/Interface 기반 설계로 기믹 간 결합도 최소화**  
- **퍼즐 규칙의 일관성과 안정성을 최우선으로 유지**  
- **모바일 환경을 고려한 입력 / 카메라 / UI 흐름 설계**  
- **환경·충격·시그널·UI 시스템의 책임 명확화**  

<br><br>

---
<a id="architecture"></a>
### 전체 아키텍처

#### 📂 Source Entry
- [`/Assets/_Proj/Scripts`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts)

<img width="1348" height="952" alt="doogy whole architecture" src="https://github.com/user-attachments/assets/14a72f3f-993e-4fda-972a-65b4f572cc0c" />

### Pushables 플로우 차트
<img width="642" height="1610" alt="doogy pushables flow chart" src="https://github.com/user-attachments/assets/e7cd9806-93b0-4c0d-85bb-d4c3d8967295" />


### 시그널 시스템 아키텍처
<img width="874" height="626" alt="doogy signal system architecture" src="https://github.com/user-attachments/assets/c5491c19-4713-4d13-ac0b-6a5a94d259dd" />
<a id="key-systems"></a>

<br>

---

<a id="core-systems"></a>
## 🏅 Core Systems

#### 🔗 Detailed Design & Flow
<a href="노션 링크 나중에 첨부"><img with = "50" height="50" alt="notion icon" src="https://noticon-static.tammolo.com/dgggcrkxq/image/upload/v1570106347/noticon/hx52ypkqqdzjdvd8iaid.svg" /> 노션 기술문서 링크</a>

<a id="player-movement--input"></a>
### 🎮 Player Movement & Input

#### 📂 Code Reference
- [`/Scripts/Player`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Player)

💡 **모바일 환경을 고려한 입력·이동 시스템으로, 플레이어 조작과 퍼즐 규칙 간 결합도를 최소화하도록 설계**

- **주요 기능**
  - 가상 조이스틱 기반 입력 처리
    - 터치 개수에 따른 입력 모드 분기
      - 1손가락: 플레이어 이동
      - 2손가락: 카메라 둘러보기(Look Around) 모드
    - UI 위 터치 입력 차단 및 입력 각도 기반 방향 스냅
  - Rigidbody 기반 이동 파이프라인 구성
  - 퍼즐 상황별 이동 보정을 Strategy 패턴으로 분리
  - 카메라 조작, 상호작용, 탑승(IRider) 상태에 따른 입력 제어

<br>

---

<a id="pushableobjects-puzzle-rule-core"></a>
### 📦 PushableObjects (Puzzle Rule Core)

#### 📂 Code Reference
- [`/Scripts/Gimmicks/Objects`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects)

💡 **코코두기 퍼즐에서 모든 이동 규칙을 단일 파이프라인으로 관리하는 핵심 규칙 시스템**

- **주요 기능**
  - 이동·낙하·적층·충격 반응을 **공통 규칙**으로 처리
  - 플레이어·동물·환경 등 다양한 환경에서도 **동일한 이동 규칙 유지**
  - 규칙 우회 이동 방지로 퍼즐 안정성 확보

<br>

#### [`IPushHandler.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects/IPushHandler.cs)
💡 **플레이어 입력과 퍼즐 규칙을 분리하기 위한 인터페이스**

  - 플레이어는 “밀기 시도”만 전달
  - 실제 이동 판단과 실행은 PushableObjects가 전담

<br>

#### [`IRider.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects/IRider.cs)
💡 **탑승(적층) 구조를 처리하기 위한 인터페이스**

  - 상단 오브젝트를 함께 이동시키기 위한 동기화 처리
  - 이동 규칙을 변경하지 않고 적층 퍼즐 구현 가능

<br>

#### [`PushableBox.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects/PushableBox.cs)
💡 **기본 밀기 퍼즐 오브젝트**

  - PushableObjects의 기본 이동 규칙만을 그대로 따르는 최소 구현체

<br>

#### [`PushableOrb.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects/PushableOrb.cs)
💡 **이동 규칙은 유지하면서, 착지 이벤트를 퍼즐 트리거로 확장한 오브젝트**

  - 낙하 착지 시 충격파를 발생시키는 특수 퍼즐 요소
  - 충격 이벤트를 감지탑 및 문으로 전달
  - 기존 이동 규칙을 수정하지 않고 퍼즐 흐름 확장

<br>

---

<a id="animal-friends-system--environment"></a>
### 🐾 Animal Friends System & Environment

#### 📂 Code Reference
- [`/Scripts/Gimmicks/Animals`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Animals)
- [`/Scripts/Stage/Block/Water](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Stage/Block/Water)

💡 **퍼즐 규칙을 직접 변경하지 않고, 플레이어의 이동 방식과 퍼즐 흐름을 확장하는 상호작용 기반 기믹 시스템**

- **주요 기능**
  - PushableObjects의 이동 규칙을 그대로 사용
  - 규칙을 바꾸지 않고, 규칙을 사용하는 방식만 확장하는 설계
  - 각 동물은 서로 다른 상호작용을 제공하지만, 동일한 이동 규칙 파이프라인을 공유

<br>

#### 🐗 Boar
💡 **직선 돌진을 통해 퍼즐 오브젝트 체인을 이동시키는 돌진·충돌형 기믹**

  - 입력 방향으로 돌진하며 전방 퍼즐 오브젝트 체인을 밀어냄
  - 수평 체인과 적층 구조를 함께 고려한 퍼즐 상호작용 제공
  - 이동 규칙은 PushableObjects에 위임

<br>

#### 🐢 Turtle
💡 **빙판 규칙 기반 연속 이동 + 탑승 구조를 결합한 이동 보조 기믹**

  - 장애물에 부딪힐 때까지 자동 이동하는 슬라이드 퍼즐 기믹
  - 이동 중 상단 오브젝트를 함께 이동시키는 탑승 구조 제공

<br>

#### 🐃 Buffalo
💡 **충격파를 통해 퍼즐 상태 변화를 유도하는 고정형 환경 기믹**

  - 플레이어 상호작용을 트리거로 충격파를 발생
  - 쿨타임 기반 재사용 제한으로 퍼즐 흐름 제어

<br>

---

#### 🌊 Flow Water (Environment)
💡 **물 타일 위 오브젝트의 주기적인 이동을 유도하는 환경 보조 기믹**

  - 물 타일 위에 놓인 퍼즐 오브젝트를 주기적으로 이동
  - 이동 대상 및 방향 결정은 공통 규칙으로 처리
  - 실제 이동 방식은 전략 패턴으로 분리하여 확장성 확보

<br>

---


### 🌊 공통 환경 시스템

#### [`Shockwave.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Shockwave.cs)
💡 **여러 기믹에서 재사용되는 충격파(리프트) 환경 시스템**

- **주요 기능**
  - 반경 내 퍼즐 오브젝트에 충격(리프트) 효과를 전달하는 공용 환경 시스템
  - 동일 좌표 기반 적층 구조를 고려한 충격 전파 처리
  - 차폐(Occlusion) 옵션을 통한 충격 전파 제어

- **주요 메서드**
  - `Fire(Vector3 origin, float tile, ...)`: 지정 조건에 따라 충격파 실행
  - `IsLineBlocked(Vector3 origin, Collider targetCol, ...)`: 차폐 여부 검사(RaycastAll)

<br>

#### [`ShockPing.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects/ShockPing.cs)
💡 **충격파 발생 시 반경 내 감지탑으로 신호를 전달하는 중계 컴포넌트**

- **주요 기능**
  - 충격파 기준 반경 내 감지탑을 탐색하여 신호 전달

- **주요 메서드**
  - `PingTowers(Vector3 origin)`: 반경 내 감지탑에 충격 신호 전달

<br>

---

#### [`RingRange.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Animals/RingRange.cs)
💡 **범위(원형/부채꼴) 시각화를 위한 공용 링 렌더링 컴포넌트**

- **주요 기능**
  - 충격, 감지, 상호작용 범위를 원형 또는 부채꼴 형태로 시각화
  - 반경 및 각도 변경에 대응한 실시간 범위 갱신

- **주요 메서드**
  - `RebuildAll()`: 범위 시각화 재구성

<br>


---

<a id="shock-signal"></a>
### 🛰️ 충격파와 시그널 기믹

#### [`ShockDetectionTower.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects/ShockDetectionTower.cs)
💡 **충격파 감지 및 시그널 릴레이를 담당하는 감지탑**

- **주요 기능**
  - 충격파 이벤트를 수신하여 시그널로 변환
  - 쿨타임 기반 중복 감지 방지
  - 인접 감지탑으로 이벤트 릴레이 전파
  - Door 등 시그널 수신 기믹과 연결

- **주요 메서드**
  - `ReceiveShock(Vector3 origin)`: 충격 이벤트 수신
  - `SendSignal()`: 연결된 수신기에 시그널 전달
  - `RelayToNearbyTowers(Vector3 origin)`: 인접 감지탑으로 충격 이벤트 전파

<br>

#### [`Turret.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Gimmicks/Objects/Turret.cs)
💡 **시야각(FOV) 기반 타겟 감지 후 문을 제어하는 감시 기믹**

- **주요 기능**
  - 반경 및 시야각(FOV) 기준 타겟 감지
  - 감지 상태에 따라 시그널 전송
  - 탐지 범위 및 상태를 시각적으로 표시

- **주요 메서드**
  - `DetectTarget()`: 타겟 감지 처리
  - `UpdateRingColour(bool detected)`: 감지 상태 시각화

<br>

#### [`ISignals.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Stage/Block/Interfaces/ISignals.cs)
💡 **시그널 송·수신을 분리하기 위한 인터페이스 정의**

- **주요 기능**
  - Sender / Receiver 구조로 기믹 간 결합도 최소화
  - 감지탑, 터렛, 스위치 등 다양한 기믹 조합 지원

- **주요 메서드**
  - `SendSignal()`: 시그널 송신
  - `ReceiveSignal()`: 시그널 수신

<br>

#### [`DoorBlock.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Stage/Block/DoorBlock.cs)
💡 **시그널 수신에 따라 열리고 닫히는 문 기믹**

- **주요 기능**
  - 외부 기믹의 시그널을 수신하여 문 상태 제어
  - 열림 / 닫힘 상태에 따른 충돌 처리
  - 조건에 따라 영구 개방 상태 지원

- **주요 메서드**
  - `ReceiveSignal()`: 시그널 수신 시 문 상태 토글
  - `ToggleDoor(bool open)`: 문 애니메이션 및 콜라이더 상태 전환
  - `OpenPermanently()`: 충격 감지탑 조건에 따른 영구 개방 처리

<br>

---

### 💎 보물과 스테이지 UI

#### [`Treasure.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Stage/Block/Treasure.cs)
💡 **스테이지 보물 획득 처리 및 UI 흐름을 제어하는 보물 오브젝트 로직**

- **주요 기능**
  - 보물 획득 여부에 따라 상호작용 처리
  - 획득 시 UI 표시 및 입력 제어
  - 수집 완료 상태에 따른 시각 효과 반영

- **주요 메서드**
  - `Init(string id)`: 보물 ID 초기화
  - `OnTriggerEnter(Collider other)`: 플레이어 충돌 시 보물 획득 및 UI 처리

<br>

#### [`StageTitleInfo.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/UI/Option/StageTitleInfo.cs)
#### [`StageTitleIngame.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/UI/Option/StageTitleIngame.cs)
💡 **선택된 스테이지 정보를 UI에 표시하는 타이틀 컴포넌트**

- **주요 기능**
  - 스테이지 진입/인게임 화면에 현재 스테이지 정보를 표시하는 UI

<br>

---

### 🧭 도움말

#### [`GameInfoPopup.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/UI/Option/GameInfoPopup.cs)
💡 **매뉴얼 데이터를 기반으로 도움말 UI를 동적으로 구성하는 팝업 시스템**

- **주요 기능**
  - DataManager에 로드된 매뉴얼 데이터를 기반으로 탭 버튼 동적 생성
  - 탭 선택 시 매뉴얼 ID에 해당하는 제목, 설명, 이미지 갱신
  - 선택된 탭에 대한 시각적 강조 처리

- **주요 메서드**
  - `BuildTabsFromCSV()`: 매뉴얼 데이터 목록을 기반으로 탭 버튼 생성
  - `LoadManual(int manualId)`: 선택된 매뉴얼 데이터 UI 반영


<br>

---

### 🎥 카메라 시스템

#### [`CamControl.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Camera/CamControl.cs)
💡 **플레이어 추적, 둘러보기, 스테이지 카메라 워킹을 담당하는 카메라 컨트롤러**

- **주요 기능**
  - 플레이어 추적 모드와 자유 시점 모드 전환
  - 드래그 입력을 통한 카메라 둘러보기
  - 스테이지 연출용 카메라 워킹 지원

- **주요 메서드**
  - `FixedUpdate()`: 카메라 위치 갱신
  - `SetFollowingPlayer(bool follow)`: 추적 상태 전환


<br>

---

### 🔁 빌드 버전 자동화

#### [`AutoVersionIncrement.cs`](https://github.com/SchneeWalen48/CocoDoogy-Public/blob/main/Assets/_Proj/Scripts/Editor/AutoVersionIncrement.cs)
💡 **빌드 시점마다 게임 버전을 자동 증가시키는 Editor 스크립트**

- **주요 기능:**
  - 빌드 시 버전 자동 증가
  - 수동 버전 관리 실수 방지

- **주요 메서드:**
  - `OnPreprocessBuild(BuildReport report)`: 빌드 전 버전 갱신

<br>

---

<a id="developer"></a>
## 👨‍💻 개발자
<div align="center">

**김현지**

<br>

<a href="https://github.com/SchneeWalen48">
  <img src="https://img.shields.io/badge/SchneeWalen48-blue?style=for-the-badge&logo=GitHub&logoColor=ffffff&label=GitHub&labelColor=Black"/>
</a>

<br><br>

**CocoDoogy – 퍼즐 규칙과 시스템 설계를 중심으로 완성도를 추구한 프로젝트**

퍼즐 규칙 시스템, 플레이어 이동, 환경 기믹 아키텍처 설계 및 구현  
모바일 환경 기준 UX 검수 및 입력·피드백 흐름 개선

</div>
