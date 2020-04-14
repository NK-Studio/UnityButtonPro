using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonPro : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,
    IPointerUpHandler
{
    #region Enum

    private enum BtnState
    {
        NORMAL,
        PRESS
    }

    #endregion

    #region Show Inspector

    [Header("이미지")]
    [SerializeField, Tooltip("기본 상태일 때")]
    private Sprite normalImage;

    [SerializeField, Tooltip("마우스가 닿았을 때")]
    private Sprite reachImage;

    [SerializeField, Tooltip("눌렸을 때")]
    private Sprite pressImage;

    [SerializeField, Tooltip("누르고 땟을 때, 함수를 실행합니다.")]
    private UnityEvent onDownUp;

    #endregion

    #region Hide Inspector

    //이미지 변수
    private Image image;

    //버튼의 상태에 대한 변수
    private BtnState _btnState;

    //그룹화가 형태인 버튼인지 아닌지 처리
    [HideInInspector]
    public ButtonGroup _buttonGroup;

    //그룹화로 만들시, 선택권이 있는 버튼
    [HideInInspector]
    public bool isSelected;

    #endregion

    private void Awake() => //초기화 해줍니다.
        image = GetComponent<Image>();

    //버튼을 누름
    public void OnPointerDown(PointerEventData eventData)
    {
        //왼쪽 마우스를 누른 경우에만 해당
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //활성화
        onSelectButton();

         //할당이 되어 있다면, 몇번째 버튼이 선택됬는지 버튼 그룹에게 알려준다.
         if (_buttonGroup == null) return;
        
         //현재 선택된 버튼이 자신임을 변경한다.
         _buttonGroup.SelectedNumber = ButtonGroup.ButtonSearch(_buttonGroup.buttonPros, this);
        
        //데이터 변경 처리
        _buttonGroup.notifyDataSetChanged();
    }

    //버튼을 땜
    public void OnPointerUp(PointerEventData eventData)
    {
        //왼쪽 마우스를 누른 경우에만 해당
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //그룹이 할당이 되어 있지 않을 때, 버튼을 땟을 시 : Not Select
        if (_buttonGroup == null)
            onNotSelectButton();

        //연결된 이벤트가 동작되도록 합니다.
        onDownUp?.Invoke();
    }

    //버튼 영역에 닿음
    public void OnPointerEnter(PointerEventData eventData)
    {
        //선택 상태가 아닐경우 : return
        if (isSelected) return;

        if (reachImage != null)
            image.sprite = reachImage;
    }

    //버튼 영역에서 나감
    public void OnPointerExit(PointerEventData eventData)
    {
        //누르고 있는 상태라면 : return
        if (_btnState == BtnState.PRESS) return;

        //선택된 상태가 아닐 때, 마우스가 UI영역 밖으로 나갔을 시 : 초기화
        if (!isSelected)
            onNotSelectButton();
    }

    public void onSelectButton()
    {
        if (pressImage != null)
            image.sprite = pressImage;

        _btnState = BtnState.PRESS;

        //버튼 그룹이 할당됬을 시 : Select
        if (_buttonGroup != null)
            isSelected = true;
    }

    public void onNotSelectButton()
    {
        if (normalImage != null)
            image.sprite = normalImage;

        _btnState = BtnState.NORMAL;

        //버튼 그룹이 할당됬을 시 : Not Select
        if (_buttonGroup != null)
            isSelected = false;
    }
}
