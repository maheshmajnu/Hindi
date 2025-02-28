using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoaxGames
{
    public class FadeAwayDecal : MonoBehaviour
    {
        [SerializeField] float m_elapsedTimeToDestroy = 5.0f;

        WaitForEndOfFrame m_waitForEndOfFrame = new WaitForEndOfFrame();

        // Start is called before the first frame update
        protected virtual IEnumerator Start()
        {
            if (m_elapsedTimeToDestroy > 0.01f)
            {
                var decal = this.GetComponent<Projector>();

                //Material mat = new Material(decal.material);
                //decal.material = mat;
                //Color color = mat.color;
                //float startFadeFactor = color.a;
                float endTime = Time.time + m_elapsedTimeToDestroy;

                while (Time.time < endTime)
                {
                    float interpolation = (endTime - Time.time) / m_elapsedTimeToDestroy; // 1 == start, 0 == end
                    //color.a = Mathf.Lerp(0, startFadeFactor, interpolation);
                    //mat.color = color;

                    yield return m_waitForEndOfFrame;
                }
            }

            Destroy(this.transform.parent.gameObject);
        }
    }
}