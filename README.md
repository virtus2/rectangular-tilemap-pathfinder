# rectangular-tilemap-pathfinder
**rectangular-tilemap-pathfinder**는 유니티에서 사용하는 정적인 직사각형 타일맵의 최단경로 찾기 시스템입니다.  
A* 알고리즘을 사용하였으며 상하좌우 및 대각선 탐색을 통해서 시작점부터 끝점까지의 최단경로를 계산합니다.  

## 실행 화면
![](https://github.com/virtus2/rectangular-tilemap-pathfinder/blob/main/screenshots/1.png)
![](https://github.com/virtus2/rectangular-tilemap-pathfinder/blob/main/screenshots/2.png)
![](https://github.com/virtus2/rectangular-tilemap-pathfinder/blob/main/screenshots/3.png)

## 사용한 유니티 버전
Unity 2021.3.5f1

## 설치 안내
Assets > Import Package > Custom Package... > Open `rectangular-tilemap-pathfinder.unitypackage`

## 사용법
### 오브젝트 배치
1. `Rectangular Tilemap`을 만들고 `Grid`에 `AStarGrid.cs`를 추가합니다.  
(예제 `TestScene`를 참고해주세요. 플레이 모드에서 Left Mouse Button으로 시작점, Right Mouse Button으로 끝점을 설정하고 Space Bar로 최단 경로를 볼 수 있습니다.)  

2. `Grid` 하위 타일맵에 이동 가능한 타일을 타일 팔레트를 이용해 배치합니다.  
(예제의 `Walkable` 이 그러한 타일맵입니다.)

3. 인스펙터에서 `AStarGrid`의 `Walkable Map` 변수에 2번에서 사용한 타일맵을 할당합니다.

### 런타임
1. 월드 좌표 또는 좌표를 통해 최단 경로를 찾을 시작점과 끝점의 `AStarNode`를 얻습니다. 이미 두 `AStarNode`가 있으면 다음 단계를 진행합니다.

2. `AStarGrid` 인스턴스에서 `pathfinder.CreatePath` 함수를 호출합니다.  
(이때 `diagonal` 매개변수를 `true`로 하면 상하좌우에 더해 대각선도 탐색합니다.)

3. 탐색이 성공하면 최단 경로인 `List<AStarNode>`가 반환됩니다. 실패하면 `null`이 반환됩니다.
  
실제 사용 시 예제의 `Grid (Example)`처럼 게임의 배경으로 쓸 타일맵(`World Map`)을 배치하고 이에 맞게 `Walkable`에 타일을 배치하면 됩니다.

## 파일 정보 및 목록
* AStarNode.cs  
탐색에 사용되는 노드 클래스. 노드와 타일맵의 타일은 1:1로 매치됩니다.   
노드의 `xPos`와 `yPos`에는 타일 왼쪽 아래의 월드 좌표가 저장됩니다.
  
* AStarGrid.cs  
노드의 그리드를 관리하는 클래스.  
멤버 변수인 `walkableMap`에 할당된 타일맵에 대해서 그리드를 생성하고, 특정 노드의 이웃 노드를 계산하는 등의 기능을 합니다.  

* AStarPathfind.cs  
A* 알고리즘을 이용해 최단 경로를 계산하는 클래스.  
맨해튼 거리와 체비쇼프 거리로 휴리스틱값을 계산합니다.

## 예제 씬 정보
* Grid (Standard size)  
셀 크기가 (1, 1, 0)이고 셀 간격이 (0, 0, 0)인 기본 타일맵에 대한 예제.
  
* Grid (Custom size)  
셀 크기가 (0.5, 0.5, 0)이고 셀 간격이 (0.5, 0.5, 0)인 크기가 다른 타일맵에 대한 예제.
  
* Grid (Many tiles)  
기본 타일맵이지만 타일이 번잡하게 배치되어있는 케이스의 예제.
  
* Grid (Example)  
실제 게임에 사용 시 참고할 예제. 게임의 배경으로 쓸 타일맵인 `World Map`과 이동 가능한 타일맵인 `Walkable`가 있습니다.  

## 참고
* [Youtube: Unity3D - Implementing Astar with Tilemaps By Coding With Unity](https://youtu.be/HCt_CYOW9jg)  
* [Youtube: A* Pathfinding in Unity By Code Monkey](https://www.youtube.com/watch?v=alU04hvz6L4)  
