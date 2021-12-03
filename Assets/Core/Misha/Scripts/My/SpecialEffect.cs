using UnityEngine;

namespace DCFAEngine
{
    public class SpecialEffect : MonoBehaviour
    {
        private enum StopActionMode
        {
            Disable,
            Destroy,
        }

        public Animator animator;
        public bool statciAngle = true;

        private Timer lifeTimer = new Timer();
        private Timer decayTimer = new Timer();
        private bool isPermanent = false;

        [SerializeField]
        private float decayDuration;

        [SerializeField]
        private StopActionMode stopAction = StopActionMode.Disable;

        public void ShowPermanent()
        {
            isPermanent = true;
            Show(0.1f);
        }
        public void AttachedShow(GameObject go, float lifeTime = 0f)
        {
            transform.parent = go.transform.parent;
            transform.localPosition = Vector3.zero;
            Show(lifeTime);
        }
        public void Show(float lifeTime = 0f)
        {
            if (gameObject.transform.parent == null || gameObject.transform.parent.gameObject.activeSelf)
            {
                if (gameObject.activeSelf == false)
                    gameObject.SetActive(true);

                animator.SetTrigger("Birth");
            }

            if (!isPermanent)
                lifeTimer.Start(lifeTime, LifeTimerOnStoped);
        }

        public void Kill()
        {
            isPermanent = false;
            lifeTimer.Stop();
            if (gameObject.activeInHierarchy)
            {
                decayTimer.Start(decayDuration, DecayTimerOnStoped);
                animator.SetTrigger("Death");
            }
        }

        private void DecayTimerOnStoped(Timer obj, Timer.StopResone resone)
        {
            if (gameObject == null)
                return; 

            if (resone == Timer.StopResone.Elapsed)
                switch (stopAction)
                {
                    case StopActionMode.Disable:
                        gameObject.SetActive(false);
                        break;
                    case StopActionMode.Destroy:
                        gameObject.SetActive(false);
                        Destroy(gameObject);
                        break;
                    default:
                        break;
                }
        }

        private void LifeTimerOnStoped(Timer obj, Timer.StopResone resone)
        {
            if (resone == Timer.StopResone.Elapsed)
                Kill();
        }

        private void LateUpdate()
        {
            if (statciAngle)
                transform.rotation = Quaternion.identity;
        }
    }
}