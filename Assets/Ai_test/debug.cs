using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class debug : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public AiAgent agent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = agent.stateMachine.currentState.ToString();
    }
}
