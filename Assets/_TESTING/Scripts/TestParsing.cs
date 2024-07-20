#if UNITY_EDITOR
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        private void Start()
        {
            /*string line = "Speaker \"Dialogue \\\"Goes In\\\" Here!\" Command(arguments here)";

            DialogueParser.Parse(line);*/

            SendFileToParse();
        }

        private void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset("testFile");

            foreach (string line in lines)
            {
                if(line == string.Empty) continue;
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }
        }

    }
}
#endif