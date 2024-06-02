using CHARACTERS;
using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class AudioTesting : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Running());
        }

        Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        IEnumerator Running2()
        {
            Character_Sprite Stella = CreateCharacter("Stella") as Character_Sprite;    
            Character Me = CreateCharacter("Me");    
            Stella.Show();

            yield return new WaitForSeconds(1);

            AudioManager.instance.PlaySoundEffect("Audio/SFX/RadioStatic", loop: true);

            yield return Me.Say("Please turn it off");

            AudioManager.instance.StopSoundEffect("RadioStatic");
            AudioManager.instance.PlayVoice("Audio/Voices/exclamation");

            Stella.Say("Okay!");
        }

        IEnumerator Running()
        {
            Character_Sprite Stella = CreateCharacter("Stella") as Character_Sprite;
            Character Me = CreateCharacter("Me");
            Stella.Show();


            GraphicPanelManager.instance.GetPanel("background").GetLayer(0, true).SetTexture("Graphics/BG Images/villagenight");
            
            AudioManager.instance.PlayTrack("Audio/Ambience/RainyMood", 0);
            AudioManager.instance.PlayTrack("Audio/Music/Calm", 1, pitch: 0.7f);

            yield return Stella.Say("Yes, of course!");

            AudioManager.instance.StopTrack(1);
        }

    }
}