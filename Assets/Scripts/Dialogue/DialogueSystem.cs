using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    bool isDilogueBoxOpen;
    bool canSpeak;
    bool isDisplayingSentence;
    bool isDisplayingDialogue;

    [Header("UI")]
    [SerializeField] GameObject UIPanel;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("Sounds")]
    [SerializeField] AudioClip transmissionOpen;
    [SerializeField] AudioClip transmissionClose;

    Animator panelAnimator;
    AudioSource audioSource;
    CanvasGroup canvasGroup;

    List<Dialogue> dialogueToPlay = new List<Dialogue>();

    Dialogue currentDialogueActive;
    int currentPriority = 0;

    public static Action<Dialogue> OnPlaySubtitles;

    readonly int anim_openDialogue = Animator.StringToHash("OpenDialogue");
    readonly int anim_closeDialogue = Animator.StringToHash("CloseDialogue");

    void Start()
    {
        panelAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        canvasGroup = UIPanel.GetComponent<CanvasGroup>();

        OnPlaySubtitles += PlaySubtitles;
    }

    public void PlaySubtitles(Dialogue subs)
    {
        if (canvasGroup.alpha == 0)
            canvasGroup.alpha = 1;

        if (subs.priority > currentPriority)
        {
            StopAllCoroutines();
            StartCoroutine(HighPriorityDialogue(subs));
        }
        else
        {
            dialogueToPlay.Add(subs);
            if (!isDilogueBoxOpen)
                StartCoroutine(PlayEachDialogue());
        }
    }

    private IEnumerator HighPriorityDialogue(Dialogue priorityDialogue)
    {
        //Close currente dialogue playing, remove it from the list and clean variables
        if (canSpeak)
            AnimationDialogueBox(anim_closeDialogue, transmissionClose);

        StopTalk(true);
        dialogueToPlay.Remove(currentDialogueActive);
        isDisplayingDialogue = false;
        isDisplayingSentence = false;

        //Clean text box
        dialogueText.text = "";

        StopTalk(false);
        //Wait until the dialogue box is completely close
        yield return new WaitUntil(() => !isDilogueBoxOpen);

        //Cache current dialogue and its priority
        currentDialogueActive = priorityDialogue;
        currentPriority = priorityDialogue.priority;

        //Set character speaking and open the dialogue box
        panelAnimator.SetInteger("CharID", priorityDialogue.charId);
        AnimationDialogueBox(anim_openDialogue, transmissionOpen);

        //Wait until the dialogue box is completely open
        yield return new WaitUntil(() => canSpeak);

        StartCoroutine(PlayEachSentence(priorityDialogue));
        isDisplayingDialogue = true;

        //Wait until finish all the sentences from the dialogue
        yield return new WaitUntil(() => !isDisplayingDialogue);

        //Remove dialogue finished from the list and close the dialogue box
        dialogueToPlay.Remove(priorityDialogue);
        AnimationDialogueBox(anim_closeDialogue, transmissionClose);
        StopTalk(false);

        //Wait until the dialogue box is completely close
        yield return new WaitUntil(() => !isDilogueBoxOpen);

        //Play the pending dialogues
        if(dialogueToPlay.Count > 0)
            StartCoroutine(PlayEachDialogue());
    }

    private IEnumerator PlayEachDialogue()
    {
        while (dialogueToPlay.Count > 0)
        {
            int lenght = dialogueToPlay.Count;

            //Loop for each dialogue
            for (int i = 0; i < lenght; i++)
            {
                //Clean text box
                dialogueText.text = "";

                //Cache current dialogue and its priority
                currentDialogueActive = dialogueToPlay[i];
                currentPriority = dialogueToPlay[i].priority;

                //Set character speaking and open the dialogue box
                panelAnimator.SetInteger("CharID", dialogueToPlay[i].charId);
                AnimationDialogueBox(anim_openDialogue, transmissionOpen);

                //Wait until the dialogue box is completely open
                yield return new WaitUntil(() => canSpeak);

                StartCoroutine(PlayEachSentence(dialogueToPlay[i]));
                isDisplayingDialogue = true;

                //Wait until finish all the sentences from the dialogue
                yield return new WaitUntil(() => !isDisplayingDialogue);

                //Remove dialogue finished from the list and close the dialogue box
                dialogueToPlay.Remove(dialogueToPlay[i]);
                AnimationDialogueBox(anim_closeDialogue, transmissionClose);
                StopTalk(false);

                //Wait until the dialogue box is completely close
                yield return new WaitUntil(() => !isDilogueBoxOpen);
            }
        }
    }

    private IEnumerator PlayEachSentence(Dialogue dialogue)
    {
        //Loop for each sentence in the dialogue
        for (int i = 0; i < dialogue.subtitles.Length; i++)
        {
            //Start showing all the letters of the sentence
            StartCoroutine(LetterByLetter(dialogue.subtitles[i].text, .03f));

            //Play speach
            audioSource.clip = dialogue.subtitles[i].speach;
            audioSource.Play();

            isDisplayingSentence = true;

            //Wait until finishe the current sentence
            yield return new WaitUntil(() => !isDisplayingSentence);
        }

        yield return new WaitForSeconds(1f);

        //End this dialogue
        isDisplayingDialogue = false;
    }

    private IEnumerator LetterByLetter(string text, float delay)
    {
        //Play talk animation if it`s stopped
        StopTalk(false);

        //Loop showing letter by letter of sentence
        for (int i = 0; i < text.Length; i++)
        {
            string currentText = text.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(.7f);

        //Stop talk animation
        StopTalk(true);

        yield return new WaitForSeconds(.3f);

        //End this sentence
        isDisplayingSentence = false;
    }

    private void StopTalk(bool state)
    {
        if (state)
            panelAnimator.SetTrigger("Reset");

        panelAnimator.speed = state ? 0 : 1;
    }

    private void AnimationDialogueBox(int animHash, AudioClip soundClip)
    {
        panelAnimator.SetTrigger(animHash);
        audioSource.clip = soundClip;
        audioSource.Play();
    }

    public void EnableCanSpeak() { canSpeak = true; }
    public void DisableCanSpeak() { canSpeak = false; }

    public void EnableIsDilogueBoxOpen() { isDilogueBoxOpen = true; }
    public void DisableIsDilogueBoxOpen() { isDilogueBoxOpen = false; }

}
