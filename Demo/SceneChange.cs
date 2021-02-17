using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maz.Unity.SmartEvent.Demo
{
    
    public class SceneChange : MonoBehaviour
    {
        
        [SerializeField]
        Object sceneObject;

        public Object Scene => sceneObject;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [ContextMenu("Change Scene")]
        void Change()
        {
            SceneManager.LoadScene(Scene.name);
		}
    }
}
