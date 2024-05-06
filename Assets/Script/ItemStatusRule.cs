using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRule
{
    string effectString;
    public void Add(string newEffectString)
    {
        string[] infos = newEffectString.Split("_");

        switch (infos[0])
        {
            case "Coin":
                
                break;

            case "":

                break;
        }
    }

    public void RemoveEffect() 
    { 

    }

    public void ActiveEffectsAboutMoney()
    {

    }
}
