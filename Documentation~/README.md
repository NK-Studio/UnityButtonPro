#ButtonPro
버튼 프로의 경우, 현재는 기능이 매우 적지만 계속 업데이트를 할 예정입니다.

메인 컴포넌트는 `ButtonPro`이며, 이 버튼들을 그룹화해주는 컴포넌트는 `Button Group`입니다.  

-----
![ex](images/ex1.PNG)
  
Normal Image : 일반적인 평소 버튼 상태  
Reach Image : 마우스가 버튼에 닿았을 때 상태  
Press Image : 마우스가 버튼을 눌렀을 때 상태  

OnDownUp : 누르고 나서 땟을 시, 동작되는 이벤트

![ex](images/ex2.PNG)

Button Pros : 그룹화가 되어야하는 `Button Pro` 입니다.   
Selected Number : 초기 선택되어야하는 Element Number 입니다. (기본 값 -1)

---
만약 버튼을 동적으로 추가하는 상황이 생긴다면,

`AddButton(ButtonPro button)`을 사용해주시기 바립니다.  
또는 직접적으로 버튼 그룹 리스트에 버튼을 추가한다면, `notifyAddButton()`를 통해 갱신을 해주어야합니다.  
주의 `AddButton(ButtonPro button)`으로 버튼을 추가할 시, 자동적으로 `notifyAddButton()`가 적용됩니다.

반대로 제거를 하는 경우 `RemoveButton(ButtonPro button)`을 사용하여 제거해주시면 되겠습니다.