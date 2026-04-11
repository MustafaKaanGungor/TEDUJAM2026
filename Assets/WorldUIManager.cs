using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldUIManager : MonoBehaviour
{
    public static WorldUIManager instance {get; private set;}
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TMP_Text speechtext;
    [SerializeField] private Button firstStageContinueButton;
    [SerializeField] private GameObject secondStageUI;
    [SerializeField] private Button secondStageContinueButton;
    [SerializeField] private Button firstDirection1;
    [SerializeField] private Button firstDirection2;
    [SerializeField] private Button firstDirection3;
    [SerializeField] private Button firstDirection4;
    [SerializeField] private Button firstDirection5;
    [SerializeField] private Button firstDirection6;
    [SerializeField] private Button firstDirection7;
    [SerializeField] private Button firstDirection8;
    [SerializeField] private Button secondDirection1;
    [SerializeField] private Button secondDirection2;
    [SerializeField] private Button secondDirection3;
    [SerializeField] private Button secondDirection4;
    [SerializeField] private Button secondDirection5;
    [SerializeField] private Button secondDirection6;
    [SerializeField] private Button secondDirection7;
    [SerializeField] private Button secondDirection8;
    [SerializeField] private TMP_Text firstDirectionText;
    [SerializeField] private TMP_Text secondDirectionText;

    private void Awake() {
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        firstStageContinueButton.onClick.AddListener(() =>
        {
            secondStageUI.SetActive(true);
            firstStageContinueButton.gameObject.SetActive(false);
            GameEvents.ChangeInputAuthorityToNpc?.Invoke();
        });

        secondStageContinueButton.onClick.AddListener(() =>
        {
            speechBubble.SetActive(false); 
            firstStageContinueButton.gameObject.SetActive(true);
            secondStageUI.SetActive(false);
            GameEvents.ChangeInputAuthorityToNpc?.Invoke();
        });

        firstDirection1.onClick.AddListener(() =>
        {
           firstDirectionText.text = "North"; 
        });
        firstDirection2.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Northeast"; 
        });firstDirection3.onClick.AddListener(() =>
        {
           firstDirectionText.text = "East"; 
        });firstDirection4.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Southeast"; 
        });firstDirection5.onClick.AddListener(() =>
        {
           firstDirectionText.text = "South"; 
        });firstDirection6.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Southwest"; 
        });firstDirection7.onClick.AddListener(() =>
        {
           firstDirectionText.text = "West"; 
        });firstDirection8.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Northwest"; 
        });

        secondDirection1.onClick.AddListener(() =>
        {
           secondDirectionText.text = "North"; 
        });
        secondDirection2.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Northeast"; 
        });secondDirection3.onClick.AddListener(() =>
        {
           secondDirectionText.text = "East"; 
        });secondDirection4.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Southeast"; 
        });secondDirection5.onClick.AddListener(() =>
        {
           secondDirectionText.text = "South"; 
        });secondDirection6.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Southwest"; 
        });secondDirection7.onClick.AddListener(() =>
        {
           secondDirectionText.text = "West"; 
        });secondDirection8.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Northwest"; 
        });

        secondStageUI.SetActive(false);
        speechBubble.SetActive(false);
    }

   public void ShowSpeechBubble(string text)
   {
      speechtext.text = text;
      speechBubble.SetActive(true);
   }

}
