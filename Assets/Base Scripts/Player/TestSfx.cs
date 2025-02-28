using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestSfx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private bool ribsMiniGameSatarted = false;
    private bool skullMiniGameSatarted = false;
    public TargetController ribsMiniGame;
    public TargetController skullMiniGame;
    private int index;
    public Camera cam;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        if (ribsMiniGameSatarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        if (raycastHit.collider.gameObject.name == "Rib")
                        {
                            index++;
                            TargetController controller = raycastHit.collider.gameObject.GetComponent<TargetController>();
                            controller.defaultEvent.Invoke();

                            if (index == 7)
                            {
                                index = 0;
                                ribsMiniGame.EndMiniGame();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                ribsMiniGameSatarted = false;
                            }
                        }
                        else
                        {
                            Debug.Log("Mission Failed");
                        }
                    }
                }
            }
        }

        if (skullMiniGameSatarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        if (raycastHit.collider.gameObject.name == "Skull")
                        {
                            index++;
                            TargetController controller = raycastHit.collider.gameObject.GetComponent<TargetController>();
                            controller.defaultEvent.Invoke();

                            if (index == 6)
                            {
                                index = 0;
                                skullMiniGame.EndMiniGame();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                skullMiniGameSatarted = false;
                            }
                        }
                        else
                        {
                            Debug.Log("Mission Failed");
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //acid base and salt
            if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
            {
                if (raycastHit2.collider != null)
                {
                    Debug.Log("Exp");
                    Animator anim = raycastHit2.collider.transform.parent.GetComponent<Animator>();
                    ExpirementAnim(anim);
                }
            }
        }

        if (phMiniGameStarted)
        {
            phValueNumberText.text = phValues[currentInd];

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                //acid base and salt
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
                {
                    if (raycastHit2.collider != null)
                    {
                        if (raycastHit2.collider.gameObject.name == phValues[currentInd])
                        {
                            currentInd++;
                            TargetController controller = raycastHit2.collider.gameObject.GetComponent<TargetController>();
                            controller.defaultEvent.Invoke();

                            if (currentInd == 7)
                            {
                                currentInd = 0;
                                phMiniGame.EndMiniGame();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                phMiniGameStarted = false;
                            }
                        }
                        else
                        {
                            Debug.Log("Mission Failed");
                        }
                    }
                }
            }
        }

        if (saltsMiniGameStarted)
        {
            phValueNumberText.text = saltsQuestions[currentInd];

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                //acid base and salt
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
                {
                    if (raycastHit2.collider != null)
                    {
                        if (raycastHit2.collider.gameObject.name == salts[currentInd])
                        {
                            currentInd++;
                            TargetController controller = raycastHit2.collider.gameObject.GetComponent<TargetController>();
                            controller.defaultEvent.Invoke();
                            if (currentIndex == 6)
                            {
                                currentInd = 0;
                                saltsMiniGame.EndMiniGame();
                                saltsMiniGameStarted = false;
                            }
                        }
                        else
                        {
                            Debug.Log("Mission Failed");
                        }
                    }
                }
            }
        }

        if (lastMiniGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                //acid base and salt
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
                {
                    if (raycastHit2.collider != null)
                    {
                        if (raycastHit2.collider.gameObject.name == "FirstMix")
                        {
                            canPourNextLiq = true;

                            Animator anim = raycastHit2.collider.gameObject.GetComponent<Animator>();
                            anim.SetTrigger("Trigger");
                        }
                        else
                        {
                            if (canPourNextLiq)
                            {
                                Animator anim = raycastHit2.collider.gameObject.GetComponent<Animator>();
                                anim.SetTrigger("Trigger");

                                TargetController targetController = raycastHit2.transform.parent.GetComponentInChildren<TargetController>();

                                StartCoroutine(DelayMiniGameEnd(targetController));
                            }
                        }
                    }
                }
            }
        }

    }

    public void RibsMiniGame()
    {
        ribsMiniGameSatarted = true;
    }

    public void SkullMiniGame()
    {
        skullMiniGameSatarted = true;
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
    }

    public TargetController movementsMiniGame;
    public void DropJoints()
    {
        index++;

        if (index == 5)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            movementsMiniGame.EndMiniGame();
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    //conservation of plants and animals
    public Npc lion;
    public void SetTargetToLion(Transform target)
    {
        lion.moveposition = null;
        lion.moveposition = target;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Npc bear;
    public void SetTargetToBear(Transform target)
    {
        bear.moveposition = target;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Npc croc;
    public void SetTargetToCroc(Transform target)
    {
        croc.moveposition = target;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void SetTargetAsPlayer(Npc npc)
    {
        npc.moveposition = InventoryManager.Instance.player.gameObject.transform;
    }

    public TargetController wastebinMiniGame;
    private int currentIndex = 0;
    public void DropWaste()
    {
        currentIndex++;

        if (currentIndex == 6)
        {
            wastebinMiniGame.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void TurnOnTrees(GameObject trees)
    {
        trees.SetActive(true);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void AnimalsToVehicle(Animator anim)
    {
        anim.SetTrigger("Trigger");
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    //acids bases and salt 10
    public TextMeshProUGUI phValueNumberText;
    private int currentInd = 0;
    private bool phMiniGameStarted = false;
    private bool saltsMiniGameStarted = false;
    private bool lastMiniGameStarted = false;
    private bool canPourNextLiq = false;
    public TargetController phMiniGame;
    public TargetController saltsMiniGame;
    public TargetController lastMiniGame;
    public List<string> phValues;
    public List<string> salts;
    public List<string> saltsQuestions;
    public int beakerCount = 0;
    public GameObject acbQuestions;
    public GameObject inQuestions;
    public List<Button> absQuestionBtn = new List<Button>();
    public List<Button> inQuestionBtn = new List<Button>();
    private TargetController currentMiniGame;

    IEnumerator QuestionsPopUp()
    {
        yield return new WaitForSeconds(2);
        acbQuestions.SetActive(true);
    }

    public void Expiriment(Animator anim)
    {
        anim.SetTrigger("Trigger");

        StartCoroutine(QuestionsPopUp());


        TargetController controller = anim.gameObject.GetComponent<TargetController>();

        currentMiniGame = controller;

        int correctIndex = int.Parse(controller.checkName);

        for (int i = 0; i < absQuestionBtn.Count; i++)
        {
            absQuestionBtn[i].onClick.RemoveAllListeners();
            if (i == correctIndex)
            {
                absQuestionBtn[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                absQuestionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }
        for (int i = 0; i < inQuestionBtn.Count; i++)
        {
            inQuestionBtn[i].onClick.RemoveAllListeners();
            if (i == controller.rotationCount)
            {
                inQuestionBtn[i].onClick.AddListener(CorrectInAnswer);
            }
            else
            {
                inQuestionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }

    }

    public void CorrectAnswer()
    {
        acbQuestions.SetActive(false);
        inQuestions.SetActive(true);
    }

    public void CorrectInAnswer()
    {
        inQuestions.SetActive(false);

        beakerCount++;
        currentMiniGame.EndMiniGame();

        if (beakerCount == 10)
        {
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void PHMiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        phMiniGameStarted = true;
    }

    public void WrongAnswer()
    {
        Debug.Log("Mission Failed");
    }

    public void ExpirementAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
        StartCoroutine(MissionStart(anim));
    }

    IEnumerator MissionStart(Animator anim)
    {
        TargetController targetController = anim.transform.GetComponentInChildren<TargetController>();
        yield return new WaitForSeconds(3);
        targetController.subObject.gameObject.SetActive(true);
    }

    IEnumerator DelayMiniGameEnd(TargetController miniGame)
    {
        yield return new WaitForSeconds(3);
        CorrectAnswer(miniGame);
    }

    public void CorrectAnswer(TargetController miniGame)
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        miniGame.EndMiniGame();
    }

}
