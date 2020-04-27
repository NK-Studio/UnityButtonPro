using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonProCreate : Editor
{
    #region Other

    [MenuItem("GameObject/UI/Button Pro/Button Pro")]
    private static void CreateButtonPro1(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("ButtonPro");
        Undo.AddComponent<ButtonPro>(go);
        var canvas = GameObject.Find("Canvas");
        go.transform.SetParent(canvas == null ? createCanvas().transform : canvas.transform);
        var _rect = go.GetComponent<RectTransform>();
        _rect.anchoredPosition3D = Vector3.zero;
        _rect.localScale = Vector3.one;
        go.layer = 5;
    }

    [MenuItem("GameObject/UI/Button Pro/Button Pro - text")]
    private static void CreateButtonPro2(MenuCommand menuCommand)
    {
        var go = new GameObject("ButtonPro");
        Undo.AddComponent<ButtonPro>(go);
        var canvas = GameObject.Find("Canvas");

        go.transform.SetParent(canvas == null ? createCanvas().transform : canvas.transform);
        go.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        go.layer = 5;

        var _text = new GameObject("Text");
        _text.transform.SetParent(go.transform);
        Undo.AddComponent<Text>(_text);

        var _rect = _text.GetComponent<RectTransform>();
        _rect.anchoredPosition3D = Vector3.zero;
        _rect.anchorMin = new Vector2(0, 0);
        _rect.anchorMax = new Vector2(1, 1);
        _rect.offsetMin = new Vector2(0, _rect.offsetMin.y);
        var offsetMax = _rect.offsetMax;
        offsetMax = new Vector2(0, offsetMax.y);
        offsetMax = new Vector2(offsetMax.x, 0);
        _rect.offsetMax = offsetMax;
        _rect.offsetMin = new Vector2(_rect.offsetMin.x, 0);
        _rect.pivot = new Vector2(0.5f, 0.5f);
        _rect.localScale = Vector3.one;
    }

    [MenuItem("GameObject/UI/Button Pro/Button Pro - TextMeshPro")]
    private static void CreateButtonPro3(MenuCommand menuCommand)
    {
        var go = new GameObject("ButtonPro");
        Undo.AddComponent<ButtonPro>(go);
        var canvas = GameObject.Find("Canvas");

        go.transform.SetParent(canvas == null ? createCanvas().transform : canvas.transform);
        go.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        go.layer = 5;

        var _text = new GameObject("Text (TMP)");
        _text.transform.SetParent(go.transform);
        Undo.AddComponent<TMPro.TextMeshProUGUI>(_text);
        
        var _rect = _text.GetComponent<RectTransform>();
        _rect.anchoredPosition3D = Vector3.zero;
        _rect.anchorMin = new Vector2(0, 0);
        _rect.anchorMax = new Vector2(1, 1);
        _rect.offsetMin = new Vector2(0, _rect.offsetMin.y);
        var offsetMax = _rect.offsetMax;
        offsetMax = new Vector2(0, offsetMax.y);
        offsetMax = new Vector2(offsetMax.x, 0);
        _rect.offsetMax = offsetMax;
        _rect.offsetMin = new Vector2(_rect.offsetMin.x, 0);
        _rect.pivot = new Vector2(0.5f, 0.5f);
        _rect.localScale = Vector3.one;
    }

    private static GameObject createCanvas()
    {
        GameObject g = new GameObject("Canvas");
        Canvas canvas = g.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler cs = g.AddComponent<CanvasScaler>();
        cs.scaleFactor = 1f;
        cs.dynamicPixelsPerUnit = 100f;
        Undo.AddComponent<GraphicRaycaster>(g);
        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
        g.layer = 5;

        if (!GameObject.Find("EventSystem"))
        {
            GameObject eventSystem = new GameObject("EventSystem");
            Undo.AddComponent<EventSystem>(eventSystem);
            Undo.AddComponent<StandaloneInputModule>(eventSystem);
        }

        return g;
    }

    #endregion
}
