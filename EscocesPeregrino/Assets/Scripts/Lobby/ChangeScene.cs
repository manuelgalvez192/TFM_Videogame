using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string changeLevel;
    [SerializeField] private GameObject PanelLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Inside");
            StartCoroutine(LoadAsync());
        }
    }
    
    IEnumerator LoadAsync()
    {
        PanelLoader.SetActive(true);
        
        yield return new WaitForSeconds(2);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(changeLevel);

    }
}
