using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecentlyPlayedSaver : MonoBehaviour
{
    public RawImage recent1;
    public RawImage recent2;
    public RawImage recent3;

    public RawImage recentlyPlayedButton1;
    public RawImage recentlyPlayedButton2;
    public RawImage recentlyPlayedButton3;

    public void SetImages()
    {
        if(recentlyPlayedButton1 != null)
        {
            recent1.texture = recentlyPlayedButton1.texture;
        }

        if(recentlyPlayedButton2 != null)
        {
            recent2.texture = recentlyPlayedButton2.texture;
        }

        if( recentlyPlayedButton3 != null)
        {
            recent3.texture = recentlyPlayedButton3.texture;
        }
    }
}
