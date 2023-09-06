using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [System.Serializable]struct ParticleGroup
    {
        public string typeName;
        public GameObject particlePrefab;
        List<GameObject> instances;
        public GameObject GetParticle()
        {
            foreach(GameObject obj in instances)
            {
                if (!obj.activeInHierarchy)
                    return obj;
            }
            GameObject newInstance = Instantiate(particlePrefab);
            newInstance.SetActive(false);
            instances.Add(newInstance);
            return newInstance;
        }
    }

    [SerializeField] ParticleGroup[] particles;
    [SerializeField] Dictionary<string, ParticleGroup> particlesTypes = new Dictionary<string,ParticleGroup>();
    static ParticleSystemManager Instance;
    public static ParticleSystemManager instance { get { return Instance; } }

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            Instance = this;
        DontDestroyOnLoad(this);

        foreach(ParticleGroup group in particles)
        {
            particlesTypes.Add(group.typeName, group);
        }
        
    }

    public void ThrowParticleSystem(string particleName, Transform transform)
    {
        GameObject newparticle = particlesTypes[particleName].GetParticle();

        newparticle.transform.parent = transform;
        newparticle.transform.position = Vector2.zero;
        newparticle.transform.localScale = Vector2.one;

        newparticle.SetActive(true);
    }
    public void ThrowParticleSystem(string particleName, Vector2 Position)
    {
        GameObject newparticle = particlesTypes[particleName].GetParticle();

        newparticle.transform.position = Position;
        newparticle.transform.localScale = Vector2.one;

        newparticle.SetActive(true);
    }
}
