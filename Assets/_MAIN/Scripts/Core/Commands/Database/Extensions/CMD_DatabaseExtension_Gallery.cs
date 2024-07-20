using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_Gallery : CMD_DatabaseExtension
    {
        private static string[] PARAM_SPEED = new string[] { "-spd", "-speed" };
        private static string[] PARAM_IMMEDIATE = new string[] { "-i", "-immediate" };
        private static string[] PARAM_BLENDTEX = new string[] { "-b", "-blend" };
        private static string[] PARAM_MEDIA = new string[] { "-m", "-media" };

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("showgalleryimage", new Func<string[], IEnumerator>(ShowGalleryImage));
            database.AddCommand("hidegalleryimage", new Func<string[], IEnumerator>(HideGalleryImage));
        }

        public static IEnumerator HideGalleryImage(string[] data)
        {
            GraphicLayer graphicLayer = GraphicPanelManager.instance.GetPanel("Cinematic").GetLayer(0, createIfDoesNotExist: true);

            if(graphicLayer.currentGraphic == null)
            {
                yield break;
            }

            float transitionSpeed = 0;
            bool immediate = false;
            string blendTexName = "";
            Texture blendTex = null;

            //Now get the parameters
            var parameters = ConvertDataToParameters(data);

            //Try to get if this is an immediate effect or not
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            //Try to get the speed of the transition if it is not an immedite effect
            if (!immediate)
                parameters.TryGetValue(PARAM_SPEED, out transitionSpeed, defaultValue: 1);

            //Try to get the blending texture for the media if we are using one
            parameters.TryGetValue(PARAM_BLENDTEX, out blendTexName);

            if (!immediate && blendTexName != string.Empty)
            {
                blendTex = Resources.Load<Texture>(FilePaths.resources_blendTextures + blendTexName);
            }

            if(!immediate)
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { graphicLayer.Clear(immediate: true); });

            graphicLayer.Clear(transitionSpeed, blendTex, immediate);

            if(graphicLayer.currentGraphic != null)
            {
                var graphicObject = graphicLayer.currentGraphic;

                yield return new WaitUntil(() => graphicObject == null);
            }
        }

        public static IEnumerator ShowGalleryImage(string[] data)
        {
            string mediaName = "";
            float transitionSpeed = 0;
            bool immediate = false;
            string blendTexName = "";
            Texture blendTex = null;

            //Now get the parameters
            var parameters = ConvertDataToParameters(data);

            //Try to get the graphic
            parameters.TryGetValue(PARAM_MEDIA, out mediaName);

            //Try to get if this is an immediate effect or not
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            //Try to get the speed of the transition if it is not an immedite effect
            if (!immediate)
                parameters.TryGetValue(PARAM_SPEED, out transitionSpeed, defaultValue: 1);

            //Try to get the blending texture for the media if we are using one
            parameters.TryGetValue(PARAM_BLENDTEX, out blendTexName);

            string pathToGraphic = FilePaths.resources_gallery + mediaName;
            Texture graphic = Resources.Load<Texture>(pathToGraphic);

            if(graphic == null)
            {
                Debug.LogError($"Could not find gallery image called '{mediaName}'.");
                yield break;
            }

            if (!immediate && blendTexName != string.Empty)
            {
                blendTex = Resources.Load<Texture>(FilePaths.resources_blendTextures + blendTexName);
            }

            //Lets try to get the layer to apply the media to
            GraphicLayer graphicLayer = GraphicPanelManager.instance.GetPanel("Cinematic").GetLayer(0, createIfDoesNotExist: true);

            if(!immediate)
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { graphicLayer?.SetTexture(graphic, filePath: pathToGraphic, immediate: true); });

            GalleryConfig.UnlockImage(mediaName);

            yield return graphicLayer.SetTexture(graphic, transitionSpeed, blendTex, pathToGraphic, immediate);
        }
    }
}