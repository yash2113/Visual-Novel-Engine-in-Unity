#if UNITY_EDITOR
using CHARACTERS;
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        public TMP_FontAsset tempFont;

        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        void Start()
        {
            //Character Stella = CharacterManager.instance.CreateCharacter("Generic");
            //Character Stella2 = CharacterManager.instance.CreateCharacter("Stella");
            //Character Adam = CharacterManager.instance.CreateCharacter("Adam");
            StartCoroutine(Test1());
        }

        IEnumerator Test()
        {
            Character_Live2D Rice = CreateCharacter("Rice") as Character_Live2D;
            Character_Live2D Mao = CreateCharacter("Mao") as Character_Live2D;
            Character_Live2D Natori = CreateCharacter("Natori") as Character_Live2D;

            Rice.SetPosition(new Vector2(0.3f, 0));
            Mao.SetPosition(new Vector2(0.4f, 0));
            Natori.SetPosition(new Vector2(0.5f, 0));

            yield return new WaitForSeconds(1);

            CharacterManager.instance.SortCharacters(new string[] { "Natori", "Mao", "Rice" });

            yield return new WaitForSeconds(1);

            Rice.SetPriority(5);
            Mao.SetPriority(6);

        }

        IEnumerator Test1()
        {
            Character Monk = CreateCharacter("Monk as Generic");

            yield return Monk.Say("Normal dialogue config");

            Monk.SetDialogueColor(Color.red);
            Monk.SetNameColor(Color.blue);

            yield return Monk.Say("Customized dialogue");

            Monk.ResetConfigurationData();

            yield return Monk.Say("Back to normal");
        }

    }
}
#endif