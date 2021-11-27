using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStarter : MonoBehaviour, ISerializationCallbackReceiver
{
    [System.Serializable]
    private class ParticleGroup
    {
        public string name;
        public ParticleSystem[] particleSystems;
    }
    private Dictionary<string, ParticleGroup> particleGroupsDictionary;
    [SerializeField]
    private ParticleGroup[] particleGroups;

    public void Play(string groupName)
    {
        if(particleGroupsDictionary.TryGetValue(groupName, out ParticleGroup particleGroup))
        {
            foreach (var particleSystem in particleGroup.particleSystems)
            {
                particleSystem.Play();
            }
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize() { }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        particleGroupsDictionary = new Dictionary<string, ParticleGroup>();
        foreach (var particleGroup in particleGroups)
        {
            particleGroupsDictionary.Add(particleGroup.name, particleGroup);
        }
    }
}
