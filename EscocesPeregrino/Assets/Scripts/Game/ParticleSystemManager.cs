using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [System.Serializable]struct ParticleGroup
    {
        public string typeName;
        public GameObject particlePrefab;
        public Vector2 spawnOffset;
        public Vector3 spawnScale;
        public Vector3 spawnRotation;
        List<GameObject> instances;

        public void InitializeList()
        {
            instances = new List<GameObject>();
        }

        public GameObject GetParticle()
        {
            foreach(GameObject obj in instances)
            {
                if (obj != null && !obj.activeInHierarchy)
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
            group.InitializeList();
            particlesTypes.Add(group.typeName, group);
        }
    }

    public void ThrowParticleSystem(string particleName, Transform transform)
    {
        if (!particlesTypes.ContainsKey(particleName))
            return;
        ParticleGroup newGroup = particlesTypes[particleName];
        GameObject newparticle = newGroup.GetParticle();

        newparticle.transform.parent = transform;
        newparticle.transform.localPosition = Vector2.zero + newGroup.spawnOffset;
        newparticle.transform.localRotation = Quaternion.Euler(newGroup.spawnRotation);
        newparticle.transform.localScale = newGroup.spawnScale;

        newparticle.SetActive(true);
    }
    public void ThrowParticleSystem(string particleName, Vector2 Position)
    {
        if (!particlesTypes.ContainsKey(particleName))
            return;
        ParticleGroup newGroup = particlesTypes[particleName];
        GameObject newparticle = newGroup.GetParticle();

        newparticle.transform.position = Position;
        newparticle.transform.localRotation = Quaternion.Euler(newGroup.spawnRotation);
        newparticle.transform.localScale = Vector2.one;

        newparticle.SetActive(true);
    }
}
