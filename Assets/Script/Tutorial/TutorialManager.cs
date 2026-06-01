using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager TM;
    public int currentStep = 0;
    public bool canPressStep = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextStep();
    }

    void Awake()
    {
        if(TM == null)
        {
            TM = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && canPressStep)
        {
            NextStep();
        }
    }

    public void NextStep()
    {
        currentStep++;
        
        switch(currentStep)
        {
            case 1:
                canPressStep = false;
                UITutorialGame.uITutorialGame.FingerCursorObjt.SetActive(true);
                UITutorialGame.uITutorialGame.FingerCursor.SetBool("FingerPathSpawn", true);
                UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathSpawn", true);
                break;
            case 2:
                StartCoroutine(CDPathUnits(3f));
                break;
            case 3:
                UITutorialGame.uITutorialGame.FingerCursorObjt.SetActive(true);
                UITutorialGame.uITutorialGame.FingerCursor.SetBool("FingerPathSpawn", true);
                UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathSpawn", true);
                break;
            case 4:
                StartCoroutine(CDPathDestroy(3));
                break;
            case 5:
                canPressStep = true;
                StartCoroutine(CDPathReturn(3));
                break;
            case 6:
                canPressStep = true;
                StartCoroutine(CDTimer(3));
                break;
            case 7:
                //canPressStep = true;
                StartCoroutine(CDDoneTutorial(3));
                break;
            default:
                break;
        }
    }

    IEnumerator CDPathUnits(float duration)
    {
        UITutorialGame.uITutorialGame.FingerCursorObjt.SetActive(false);
        UITutorialGame.uITutorialGame.FingerCursor.SetBool("FingerPathSpawn", false);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathSpawn", false);
        yield return new WaitForSeconds(1);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathUnits", true);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(true);
        UITutorialGame.uITutorialGame.ExplenationText.text = UITutorialGame.uITutorialGame.texts[0];
        yield return new WaitForSeconds(duration);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathUnits", false);
        UITutorialGame.uITutorialGame.ExplenationText.text = UITutorialGame.uITutorialGame.texts[1];
        yield return new WaitForSeconds(2);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(false);
        //tambahkan text ui bahwa bisa rotate juga
    }

    IEnumerator CDPathDestroy(float duration)
    {
        UITutorialGame.uITutorialGame.FingerCursorObjt.SetActive(false);
        UITutorialGame.uITutorialGame.FingerCursor.SetBool("FingerPathSpawn", false);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathSpawn", false);
        yield return new WaitForSeconds(duration);
        UITutorialGame.uITutorialGame.FingerCursorObjt.SetActive(true);
        UITutorialGame.uITutorialGame.FingerCursor.SetBool("FingerPathDestroy", true);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathDestroy", true);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(true);
        UITutorialGame.uITutorialGame.ExplenationText.text = UITutorialGame.uITutorialGame.texts[2];
        yield return new WaitForSeconds(2);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(false);
    }

    IEnumerator CDPathReturn(float duration)
    {
        UITutorialGame.uITutorialGame.FingerCursorObjt.SetActive(false);
        UITutorialGame.uITutorialGame.FingerCursor.SetBool("FingerPathSpawn", false);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathDestroy", false);
        yield return new WaitForSeconds(duration);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathReturn", true);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(true);
        UITutorialGame.uITutorialGame.ExplenationText.text = UITutorialGame.uITutorialGame.texts[3];
        yield return new WaitForSeconds(2);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(false);
    }

    IEnumerator CDTimer(float duration)
    {
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HolePathReturn", false);
        yield return new WaitForSeconds(duration);
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HoleTimer", true);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(true);
        UITutorialGame.uITutorialGame.ExplenationText.text = UITutorialGame.uITutorialGame.texts[4];
        yield return new WaitForSeconds(2);
        UITutorialGame.uITutorialGame.ExplenationText.text = UITutorialGame.uITutorialGame.texts[5];
        yield return new WaitForSeconds(2);
        UITutorialGame.uITutorialGame.ExplenationText.gameObject.SetActive(false);
    }

    IEnumerator CDDoneTutorial(float duration)
    {
        UITutorialGame.uITutorialGame.HoleHighlight.SetBool("HoleTimer", false);
        yield return new WaitForSeconds(duration);
        //menampilkan panel atau text akhir tutorial
        UITutorialGame.uITutorialGame.TutorialDone();
    }
}
