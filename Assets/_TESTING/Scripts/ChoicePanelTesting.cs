using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class ChoicePanelTesting : MonoBehaviour
    {
        ChoicePanel panel;

        private void Start()
        {
            panel = ChoicePanel.instance;
            StartCoroutine(Running());
        }

        IEnumerator Running()
        {
            string[] choices = new string[]
           {
                "Witness? Is that camera on?",
                "Oh, nah!",
                "I didn't see nothing!",
                "Matta' Fact - I'm blind in my left eye and 43% blind in my right eye."
           };

            panel.Show("Did You Witness Anything Strange?", choices);

            while(panel.isWaitingOnUserChoice)
                yield return null;

            var decision = panel.lastDecision;

            Debug.Log($"Made choice {decision.answerIndex} '{decision.choices[decision.answerIndex]}'");
        }

    }
}