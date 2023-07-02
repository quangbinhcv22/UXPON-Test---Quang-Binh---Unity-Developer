using Cinemachine;
using UnityEngine;

namespace View
{
    public class GameplayCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera playCamera;
        [SerializeField] private CinemachineVirtualCamera waitCamera;

        private void OnEnable()
        {
            Gameplay.Gameplay.OnStart += SwitchState_Play;
            Gameplay.Gameplay.OnEnd += SwitchState_Wait;

            SwitchState_Wait();
        }

        private void OnDisable()
        {
            Gameplay.Gameplay.OnStart -= SwitchState_Play;
            Gameplay.Gameplay.OnEnd -= SwitchState_Wait;
        }


        private void SwitchState_Play()
        {
            playCamera.gameObject.SetActive(true);
            waitCamera.gameObject.SetActive(false);
        }

        private void SwitchState_Wait()
        {
            playCamera.gameObject.SetActive(false);
            waitCamera.gameObject.SetActive(true);
        }
    }
}