using UnityEngine;

public class SocialMediaManager : MonoBehaviour
{
    public void OpenYoutubeChannel()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCjQumg_0L1q27SjqgMh7wfQ");
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/Stelios_Krom");
    }

    public void OpenLinkedln()
    {
        Application.OpenURL("https://www.linkedin.com/in/stelios-kromlidis-465b66283/");
    }
}
