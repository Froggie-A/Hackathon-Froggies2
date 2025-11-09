using System.Runtime.InteropServices.WindowsRuntime;
using Mono.Cecil.Cil;
using UnityEngine;

public class InstructionsBScript : MonoBehaviour
{
    public GameObject instructionB;


    public void Start()
    {
        instructionB.SetActive(false);
    }
    
    public void isActive(GameObject instructionB)
    {
      
        instructionB.SetActive(true);
    
    }

    public void isNotActive(GameObject instructionB)
    {

        instructionB.SetActive(false);
        
    }
   

    
}
