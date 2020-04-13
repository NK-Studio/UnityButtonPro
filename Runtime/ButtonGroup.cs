using System.Collections.Generic;
using UnityEngine;

public class ButtonGroup : MonoBehaviour
{
    #region Show Inspector

    [Header("그룹화 되어야하는 버튼들")]
    [Tooltip("그룹으로 지어줄 버튼들")]
    public List<ButtonPro> buttonPros = new List<ButtonPro>();

    [Header("초기에 선택되어 있어야하는 버튼")]
    [SerializeField, Tooltip("초기 선택될 버튼")]
    private int _SelectedNumber = -1;

    #endregion

    #region Hide Inspector

    public int SelectedNumber
    {
        get => _SelectedNumber;
        set
        {
            _preNumber = _SelectedNumber;
            _SelectedNumber = value;
        }
    }

    //이전에 선택된 버튼입니다.
    private int _preNumber;

    #endregion

    private void Awake()
    {
        //초기화
        _preNumber = SelectedNumber;

        //등록된 버튼이 1미만이면 : return
        if (buttonPros.Count < 1)
            return;

        //전체 오브젝트가 선택되지 않도록하고, 그룹화 한다.
        foreach (var btnPro in buttonPros)
        {
            btnPro.isSelected = false;
            btnPro._buttonGroup = this;
        }

        //선택 넘버가 따로 설정되어있지 않다면,
        if (SelectedNumber == -1)
            return;

        //선택 넘버가 값이 오버되는 것을 방지
        SelectedNumber = Mathf.Clamp(SelectedNumber, 0, buttonPros.Count);

        //해당 버튼이 선택되도록 변경.
        buttonPros[SelectedNumber].isSelected = true;
    }

    private void Start()
    {
        //선택 넘버가 따로 설정되어있지 않다면 : return
        if (SelectedNumber == -1)
            return;

        buttonPros[SelectedNumber].onSelectButton();
    }

    /// <summary>
    /// 해당 버튼이 버튼 그룹에서 몇번째 인덱스인지 반환합니다.
    /// </summary>
    /// <param name="Buttons">그룹화된 버튼</param>
    /// <param name="Button">찾을 버튼</param>
    public static int ButtonSearch(List<ButtonPro> Buttons, ButtonPro Button)
    {
        //리턴될 변수
        int index = -1;

        //해당 버튼 개수만큼 반복
        for (int i = 0; i < Buttons.Count; i++)
        {
            //버튼 그룹중에 해당 버튼이 아니라면 : continue
            if (Buttons[i] != Button) continue;

            index = i;
        }

        return index;
    }
    
    /// <summary>
    /// 인자로 넘어온 버튼을 버튼 그룹에 추가합니다.
    /// </summary>
    /// <param name="button">그룹 시킬 버튼</param>
    public void AddButton(ButtonPro button)
    {
        if (!buttonPros.Contains(button))
        {
            buttonPros.Add(button);
            notifyAddButton();
        }else
            Debug.LogWarning("이미 수록된 버튼입니다.");
    }
    
    /// <summary>
    /// 인자로 넘어온 버튼을 그룹에서 제거합니다.
    /// </summary>
    /// <param name="button">제거할 버튼</param>
    public void RemoveButton(ButtonPro button)
    {
        if (buttonPros.Remove(button))
        {
            //이전 버튼 그룹에서 탈퇴
            button._buttonGroup = null;
            button.isSelected = false;

            //선택 넘버가 값이 오버되는 것을 방지
            SelectedNumber = Mathf.Clamp(SelectedNumber, 0, buttonPros.Count - 1);
            _preNumber = _SelectedNumber;

            //화면 버튼 렌더링 갱신
            buttonPros[SelectedNumber].onSelectButton();
            button.onNotSelectButton();
        }
    }

    /// <summary>
    /// 인자로 넘어온 인덱스 값을 기반으로 그룹에서 제거합니다.
    /// </summary>
    /// <param name="index">제거할 버튼 인덱스</param>
    public void RemoveButton(int index)
    {
        //범위 초과 방지
        index = Mathf.Clamp(index, 0, buttonPros.Count);

        //지우기전 해당 버튼 객체 수록
        var preButton = buttonPros[index];

        //제거
        buttonPros.RemoveAt(index);

        //이전 버튼 그룹에서 탈퇴
        preButton._buttonGroup = null;
        preButton.isSelected = false;

        //선택 넘버가 값이 오버되는 것을 방지
        SelectedNumber = Mathf.Clamp(SelectedNumber, 0, buttonPros.Count - 1);
        _preNumber = _SelectedNumber;

        //화면 버튼 렌더링 갱신
        buttonPros[SelectedNumber].onSelectButton();
        preButton.onNotSelectButton();
    }
    
    /// <summary>
    /// SelectedNumber을 Set하고, 버튼의 렌더링 상태를 변경해야할 때, 호출합니다.
    /// </summary>
    public void notifyDataSetChanged()
    {
        buttonPros[_preNumber].onNotSelectButton();
        buttonPros[SelectedNumber].onSelectButton();
    }

    /// <summary>
    /// 버튼을 동적으로 추가할 경우 정보를 다시 갱신합니다.
    /// </summary>
    public void notifyAddButton()
    {
        //등록된 버튼이 1미만이면 : return
        if (buttonPros.Count < 1)
            return;

        //전체 오브젝트가 선택되지 않도록하고, 그룹화 한다.
        foreach (var btnPro in buttonPros)
        {
            btnPro.isSelected = false;
            btnPro._buttonGroup = this;
        }

        //선택 넘버가 값이 오버되는 것을 방지
        SelectedNumber = Mathf.Clamp(SelectedNumber, 0, buttonPros.Count);

        //해당 버튼이 선택되도록 변경.
        buttonPros[SelectedNumber].isSelected = true;

        buttonPros[SelectedNumber].onSelectButton();
    }
}
