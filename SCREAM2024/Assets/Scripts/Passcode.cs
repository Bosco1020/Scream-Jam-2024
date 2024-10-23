using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName="createPasscode")]
public class Passcode : ScriptableObject
{
    public string Code;

    public void appendCode(int input)
    {
        Code += input.ToString();
    }

    public void clearCode()
    {
        Code = "";
    }
}
