using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace View
{
    public static class Vfx
    {
        public static async void PlayOneShot(ParticleSystem prefab, Transform target, float delay = 0f)
        {
            if (prefab)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
                var vfx = Object.Instantiate(prefab);
                vfx.transform.position = target.position;
            }
        }
    }
}