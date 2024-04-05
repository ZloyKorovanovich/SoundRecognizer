using UnityEngine;

namespace Communication
{
    [DisallowMultipleComponent]
    public class VoiceSource : MonoBehaviour
    {
        public LayerMask listners = 0;
        public float range = 20f;

        public void Say(Word word)
        {
            if (word == Word.None)
                return;

            var recivers = Physics.OverlapSphere(transform.position, range, listners);
            foreach( var reciver in recivers )
            {
                var listner = reciver.GetComponent<IVoiceListner>();
                listner?.Listen(word);
            }
        }
    }
}