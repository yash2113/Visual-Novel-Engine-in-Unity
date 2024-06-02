using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace History
{
    [System.Serializable]
    public class HistoryState
    {
        public DialogueData dialogueData;
        public List<CharacterData> characters;
        public List<AudioData> audio;
        public List<GraphicData> graphics;

        public static HistoryState Capture()
        {
            HistoryState state = new HistoryState();
            state.dialogueData = DialogueData.Capture();
            state.characters = CharacterData.Capture();
            state.audio = AudioData.Capture();
            state.graphics = GraphicData.Capture();

            return state;   
        }

        public void Load()
        {

        }

    }
}