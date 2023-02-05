using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEditor.PackageManager;

public class UIScrollToSelection : MonoBehaviour
{

    /* ### VARIABLES ============================================================== */

    // settings
    public float scrollSpeed = 10f;

    [SerializeField]
    private RectTransform layoutListGroup;

    // temporary variables
    [SerializeField]
    private RectTransform lastSelection;
    [SerializeField]
    private RectTransform selection;
    private bool scrollToSelection;

    // references
    private RectTransform scrollWindow;
    private RectTransform currentCanvas;
    private ScrollRect targetScrollRect;


    /* ### MAIN METHODS =========================================================== */
    // Use this for initialization
    private void Start()
    {
        targetScrollRect = GetComponent<ScrollRect>();
        scrollWindow = targetScrollRect.GetComponent<RectTransform>();
        currentCanvas = transform.root.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        ScrollRectToLevelSelection();
    }

    //private void SetCurrent

    private void ScrollRectToLevelSelection()
    {
        // check main references
        var referencesAreIncorrect = (targetScrollRect == null || layoutListGroup == null || scrollWindow == null);
        if (referencesAreIncorrect)
            return;

        //need to fix this
        // GetSel(selection);
        GetLastGameObjectSelected(selection);
        //if (selection != lastSelection)
        //    scrollToSelection = true;

        //// check if scrolling is possible
        //bool isScrollDirectionUnknown =
        //    (selection == null || lastSelection == null || scrollToSelection == false);

        //if (isScrollDirectionUnknown || selection.transform.parent != layoutListGroup.transform)
        //    return;

        //// move the current scroll rect to correct position
        //float selectionPos = -selection.anchoredPosition.x;
        //int direction = (int)Mathf.Sign(selection.anchoredPosition.x - lastSelection.anchoredPosition.x);

        //float elementHeight = layoutListGroup.sizeDelta.x / layoutListGroup.transform.childCount;
        //float maskHeight = currentCanvas.sizeDelta.x + scrollWindow.sizeDelta.x;
        //float listPixelAnchor = layoutListGroup.anchoredPosition.x;

        //// get the element offset value depending on the cursor move direction
        //float offlimitsValue = 0;
        //if (direction > 0 && selectionPos < listPixelAnchor)
        //{
        //    offlimitsValue = listPixelAnchor - selectionPos;
        //}
        //if (direction < 0 && selectionPos + elementHeight > listPixelAnchor + maskHeight)
        //{
        //    offlimitsValue = (listPixelAnchor + maskHeight) - (selectionPos + elementHeight);
        //}
        //// move the target scroll rect
        //targetScrollRect.verticalNormalizedPosition +=
        //    (offlimitsValue / layoutListGroup.sizeDelta.x) * Time.deltaTime * scrollSpeed;
        //// check if we reached our destination
        //if (Mathf.Abs(offlimitsValue) < 2f)
        //    scrollToSelection = false;
        //// save last object we were "heading to" to prevent blocking
        //// lastSelection = selection;
    }

    private void GetSel(RectTransform recentSelection)
    {
        // get calculation references
        EventSystem events = EventSystem.current;
        RectTransform lastSelectionTemp;
        if (recentSelection != events.currentSelectedGameObject)
            lastSelectionTemp = recentSelection;
        else
            lastSelectionTemp = lastSelection;

        selection = events.currentSelectedGameObject != null ?
           events.currentSelectedGameObject.GetComponent<RectTransform>() : null;
        lastSelection = lastSelectionTemp != null ?
          lastSelectionTemp : selection;
    }

    private void GetLastGameObjectSelected(RectTransform sel)
    {
        EventSystem events = EventSystem.current;
        if (events.currentSelectedGameObject == selection)
            return;

        selection = events.currentSelectedGameObject != null ?
       events.currentSelectedGameObject.GetComponent<RectTransform>() : null;
        lastSelection = sel != null ? sel.GetComponent<RectTransform>() : selection;

        Debug.Log("asda");
    }

}