using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class Conversation
    {
        private List<string> lines = new List<string>();
        private int progress = 0;

        public Conversation(List<string> lines, int progress = 0)
        {
            this.lines = lines;
            this.progress = progress;
        }

        /*public int GetProgress() => progress;
        public void SetProgress(int value) => progress = value;
        public void IncrementProgress() => progress++;
        public int Count => lines.Count;
        public List<string> GetLines() => lines;
        public string CurrentLine() => lines[progress];
        public bool HasReachedEnd() => progress >= lines.Count;*/

        public int GetProgress() => progress;

        public void SetProgress(int value)
        {
            progress = value;
        }

        public void IncrementProgress()
        {
            progress++;
        }

        public int Count => lines.Count;

        public List<string> GetLines() => lines;

        public string CurrentLine()
        {
            string line = (progress < lines.Count) ? lines[progress] : string.Empty;
            return line;
        }

        public bool HasReachedEnd()
        {
            bool hasReachedEnd = progress >= lines.Count;
            return hasReachedEnd;
        }

    }
}