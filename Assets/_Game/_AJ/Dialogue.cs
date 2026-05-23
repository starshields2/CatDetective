using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI speakerName;
    public bool textStart;
    public string unitName;
    public string[] lines;
    public float textSpeed;
    public float waitTime;
    [SerializeField] private Animation _animation;
    [SerializeField] private AudioSource _notification;

    public int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
       
    }

    void Awake()
    {
        speakerName.text = unitName;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    [ContextMenu("Dialogue")]
    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (!textStart)
        {
            textStart = true;
    foreach (char c in lines[index].ToCharArray())
        {
            yield return new WaitForSeconds(textSpeed);
            textComponent.text += c;  
        }
            textStart = false;
        }
        //index+=1;
    }

   public void NextLine()
    {
        if(index < lines.Length)
        {
            StopAllCoroutines();
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
         
            textComponent.text = "Error: NO MORE LINES!";
        }
        index += 1;
    }

    [ContextMenu("Handle Overworld Dialogue")]
    public void HandleOverworldDialogue()
    {
        textComponent.text = "";
        textStart = false;
        StartCoroutine(HandleOverworldDialogueCoroutine());

    }

    public IEnumerator HandleOverworldDialogueCoroutine()
    {
        _notification.Play();
        _animation.Play("PortraitNotificationUp");
        NextLine();
        yield return new WaitForSeconds(waitTime);
        _animation.Play("PortraitNotificationDown");
    }
}
