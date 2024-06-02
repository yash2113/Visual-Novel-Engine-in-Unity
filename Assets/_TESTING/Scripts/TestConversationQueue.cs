using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConversationQueue : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(Running());
    }
    IEnumerator Running()
    {
        List<string> lines = new List<string>()
        {
            "This is line 1 from original conversation.",
            "This is line 2 from original conversation.",
            "This is line 3 from original conversation."
        };

        yield return DialogueSystem.instance.Say(lines);

        DialogueSystem.instance.Hide();
    }

    private void Update()
    {
        List<string> lines = new List<string>();
        Conversation conversation = null;

        if(Input.GetKeyDown(KeyCode.Q))
        {
            lines = new List<string>()
            {
                "This is the start of an enqueued conversation.",
                "We can keep it going!"
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.Enqueue(conversation);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            lines = new List<string>()
            {
                "This is an important conversation.",
                "August 26,2024 is International Dogs Day!"
            };
            conversation = new Conversation(lines);
            DialogueSystem.instance.conversationManager.EnqueuePriority(conversation);
        }

    }

}
