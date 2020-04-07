using UnityEngine;

public class ButtonGroup : MonoBehaviour
{
    #region Show Inspector

    [Header("그룹화 되어야하는 버튼들")]
    [Tooltip("그룹으로 지어줄 버튼들")]
    public ButtonPro[] buttonPros = new ButtonPro[2];

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
        if (buttonPros.Length < 1)
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
        SelectedNumber = Mathf.Clamp(SelectedNumber, 0, buttonPros.Length);

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
    public static int ButtonSearch(ButtonPro[] Buttons, ButtonPro Button)
    {
        //리턴될 변수
        int index = -1;

        //해당 버튼 개수만큼 반복
        for (int i = 0; i < Buttons.Length; i++)
        {
            //버튼 그룹중에 해당 버튼이 아니라면 : continue
            if (Buttons[i] != Button) continue;

            index = i;
        }

        return index;
    }

    /// <summary>
    /// SelectedNumber을 Set하고, 버튼의 렌더링 상태를 변경해야할 때, 호출합니다.
    /// </summary>
    public void notifyDataSetChanged()
    {
        buttonPros[_preNumber].onNotSelectButton();
        buttonPros[SelectedNumber].onSelectButton();
    }
}
