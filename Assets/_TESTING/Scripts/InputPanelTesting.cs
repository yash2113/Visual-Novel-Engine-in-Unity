using CHARACTERS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPanelTesting : MonoBehaviour
{
    public InputPanel inputPanel;

    private void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        Character Stella = CharacterManager.instance.CreateCharacter("Stella", revealAfterCreation: true);

        yield return Stella.Say("Hi, Whats your name?");

        inputPanel.Show("Whats your name?");

        while (inputPanel.isWaitingOnUserInput)
            yield return null;

        string characterName = inputPanel.lastInput;

        yield return Stella.Say($"Its very nice to meet you, {characterName}!");
    }


}
