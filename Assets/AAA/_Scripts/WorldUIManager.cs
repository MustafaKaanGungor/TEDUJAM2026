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
    //neden liste yapmadin aaaaaaaaaaa
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

    private Direction firstDirection;
    private Direction secondDirection;

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
            GameEvents.PlayerMadeASelection?.Invoke(firstDirection, secondDirection);
            GameEvents.ChangeInputAuthorityToNpc?.Invoke();
        });

        firstDirection1.onClick.AddListener(() =>
        {
           firstDirectionText.text = "North";
            firstDirection = Direction.NORTH;
        });
        firstDirection2.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Northeast"; 
            firstDirection = Direction.NORTHEAST;
        });firstDirection3.onClick.AddListener(() =>
        {
           firstDirectionText.text = "East"; 
            firstDirection = Direction.EAST;
        });firstDirection4.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Southeast"; 
            firstDirection = Direction.SOUTHEAST;
        });firstDirection5.onClick.AddListener(() =>
        {
           firstDirectionText.text = "South"; 
            firstDirection = Direction.SOUTH;
        });firstDirection6.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Southwest"; 
            firstDirection = Direction.SOUTHWEST;
        });firstDirection7.onClick.AddListener(() =>
        {
           firstDirectionText.text = "West"; 
            firstDirection = Direction.WEST;
        });firstDirection8.onClick.AddListener(() =>
        {
           firstDirectionText.text = "Northwest"; 
            firstDirection = Direction.NORTHWEST;
        });

        secondDirection1.onClick.AddListener(() =>
        {
           secondDirectionText.text = "North";
            secondDirection = Direction.NORTH;
        });
        secondDirection2.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Northeast"; 
            secondDirection = Direction.NORTH;
        });secondDirection3.onClick.AddListener(() =>
        {
           secondDirectionText.text = "East"; 
            secondDirection = Direction.EAST;
        });secondDirection4.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Southeast"; 
            secondDirection = Direction.SOUTHEAST;
        });secondDirection5.onClick.AddListener(() =>
        {
           secondDirectionText.text = "South"; 
            secondDirection = Direction.SOUTH;
        });secondDirection6.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Southwest"; 
            secondDirection = Direction.SOUTHWEST;
        });secondDirection7.onClick.AddListener(() =>
        {
           secondDirectionText.text = "West"; 
            secondDirection = Direction.WEST;
        });secondDirection8.onClick.AddListener(() =>
        {
           secondDirectionText.text = "Northwest"; 
            secondDirection = Direction.NORTHWEST;
        });
      
        secondStageUI.SetActive(false);
        speechBubble.SetActive(false);
    }

   public void ShowSpeechBubble(NpcDialogue npcDialogue)
   {
      speechtext.text = @$"I went to <color=""blue"">{npcDialogue.direction1}</color> and saw <color=""green"">{IslandTypeStringazer(npcDialogue.islandOnDirection1)}</color> then went to <color=""blue"">{npcDialogue.direction2}</color> and saw <color=""green"">{IslandTypeStringazer(npcDialogue.islandOnDirection2)}</color>.";
      speechBubble.SetActive(true);
   }

   public void ShowSecondStageUI(IslandType islandType)
   {
       speechtext.text = @$"I want to go to <color=""green"">{IslandTypeStringazer(islandType)}</color>";
       secondStageUI.SetActive(true);
   }

   private string IslandTypeStringazer(IslandType type) 
    {
        switch (type)
        {
            case IslandType.EMPTY:
                return "nothing";
            case IslandType.ISLAND1:
                return "Drake’s Lookout island";
            case IslandType.ISLAND2:
                return "Merchant’s Rest island";
            case IslandType.ISLAND3:
                return "Resolution Reach island";
            case IslandType.ISLAND4:
                return "Northumberland Skerry island";
            case IslandType.ISLAND5:
                return "Emerald Atoll island";
            case IslandType.ISLAND6:
                return "Anson’s Key island";
            case IslandType.DANGER1:
                return "dangerous sharks";
            case IslandType.DANGER2:
                return "dangerous pirates";
            case IslandType.DANGER3:
                return "a dangerous kraken";
            case IslandType.LANDMARK1:
                return "a landmark with a big tree";
            case IslandType.LANDMARK2:
                return "a landmark with a big rock";
            case IslandType.LANDMARK3:
                return "a landmark with a big shipwreck";
            case IslandType.LANDMARK4:
                return "a landmark with a big treasure chest";
            case IslandType.BASE:
                return "the your island";
            default:
                return type.ToString();
        }
    }
}
