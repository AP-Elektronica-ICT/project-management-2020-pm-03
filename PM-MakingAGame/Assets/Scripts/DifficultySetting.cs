using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public enum Diff {Ez,Norm,Hard}
public class DifficultySetting : MonoBehaviour
{ 
    static public Diff difficultyMode = Diff.Ez;
   

    
    public void EasyMode()
    {
        difficultyMode = Diff.Ez;
    }

  
    public void NormalMode()
    {
        difficultyMode = Diff.Norm;
    }
  
    public void HardMode()
    {
        difficultyMode = Diff.Hard;
    }
    //Normal deafault setting

   

 
    


}
