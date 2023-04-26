using Discord;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Discord_Controller : MonoBehaviour
{
    public long applicationID;
    [Space]
    public string details = "";
    public string state = "";
    [Space]
    public string largeImage = "titlelogo";
    public string largeText = "Herby's Tanks";
    [Space]
    public bool isPlayingCampaign;
    public int levelCount;


    private long time;

    public Discord.Discord discord;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();



        //UpdateStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            details = "Just browsing the menus!";
            state = "";
        }
        else if (SceneManager.GetActiveScene().name == "Campaign")
        {
            details = "Playing Campaign";
            isPlayingCampaign = true;
            state = "Currently on level: " + levelCount.ToString();
        }else if (SceneManager.GetActiveScene().name == "PVP")
        {
            details = "Fighting their friends in PVP combat!";
        }
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }
    private void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = state,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to discord!");
            });
        }
        catch
        {
            Destroy(gameObject);
        }
    }
}
