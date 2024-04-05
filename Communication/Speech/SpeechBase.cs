using DeadlyMath;
using System.Collections.Generic;
using UnityEngine;

namespace Communication
{
    [DisallowMultipleComponent]
    public class SpeechBase : Singleton<SpeechBase>
    {
        public List<WordNode> nodes = new List<WordNode>();

        public void GetWord(float sound, out Word word)
        {
            word = Word.None;

            var smallestDifference = float.MaxValue;
            foreach (var node in nodes)
            {
                var diff = Mathf.Abs(sound - node.median);
                if (diff < smallestDifference)
                {
                    smallestDifference = diff;
                    word = node.word;
                }
            }
        }
    }
}