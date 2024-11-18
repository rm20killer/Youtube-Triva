using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Unity.VisualScripting;

public class YoutubeAPI : MonoBehaviour
{
    public string videoId;
    public bool isActive = false;

    public float pullEveryXSeconds;
    private const string youtubeAPIURL = "https://www.googleapis.com/youtube/v3";
    private string apiKey;

    private HashSet<string> viewedETags;
    public Queue<Tuple<string, string, string, string>> history;

    public GameObject GameManger;
    private WhoAmI _whoAmI;

    void Awake()
    {
        _whoAmI = GameManger.GetComponent<WhoAmI>();
    }

    public void SetVideoId(string value)
    {
        videoId = value;
    }
    
    public void SetApi(string value)
    {
        apiKey = value;
    }
    
    IEnumerator Start()
    {
        StreamReader reader = new StreamReader("Assets/Resources/youtubeapikey.txt");
        apiKey = reader.ReadToEnd();

        if (videoId != "") // videoId must be provided at the beginning!!
        {
            viewedETags = new HashSet<string>();
            history = new Queue<Tuple<string, string, string, string>>();
            // get chatId from videoId
            var chatIDURL = $"{youtubeAPIURL}/videos?id={videoId}&key={apiKey}&part=liveStreamingDetails";
            var req = UnityWebRequest.Get(chatIDURL);
            yield return req.SendWebRequest();
            // Debug.Log(chatIDURL);
            var json = JSON.Parse (req.downloadHandler.text);
            var liveChatId = json["items"][0]["liveStreamingDetails"]["activeLiveChatId"].ToString();
            //remove "
            liveChatId = liveChatId.Replace(new StringBuilder().Append('"').ToString(),"");
            // form chatURL
            var chatURL = $"{youtubeAPIURL}/liveChat/messages?liveChatId={liveChatId}&part=snippet,authorDetails&key={apiKey}";
            // Debug.Log(chatURL);
            // pulling chats ...
            while(true)
            {
                if (!isActive)
                    yield return new WaitForSeconds(1);
                else
                {
                    req = UnityWebRequest.Get(chatURL);
                    Debug.Log("Request sent");
                    yield return req.SendWebRequest();

                    json = JSON.Parse (req.downloadHandler.text);
                    // Debug.Log(json);
                    var chats = json["items"];
                    foreach (var chat in chats)
                    {
                        var etag = chat.Value["etag"];
                        if (viewedETags.Contains(etag))
                            continue;
                        var snip = chat.Value["snippet"];
                        var author = chat.Value["authorDetails"];

                        // other useful keys:
                        // snippet:
                        // "publishedAt": "2021-10-14T06:41:08.539641+00:00", (UTC time)
                        // authorDetails:
                        // "profileImageUrl": "https://yt3.ggpht.com/ytc/AKedOLQjdwY5hM1Olj3PdpNs-f8eARuZfqxn6eKo36WaDA=s88-c-k-c0x00ffffff-no-rj",
                        // "isVerified": false,
                        // "isChatOwner": true,
                        // "isChatSponsor": false,
                        // "isChatModerator": false

                        Debug.Log (author["displayName"].ToString() + 
                                    " : " + snip["displayMessage"].ToString());

                        var tuple = new Tuple<string, string,string, string>
                                        (author["displayName"].ToString(),
                                         snip["displayMessage"].ToString(),
                                         author["profileImageUrl"].ToString(),
                                         snip["publishedAt"].ToString());
                        history.Enqueue(tuple);
                        viewedETags.Add(etag);
                        _whoAmI.CheckQuess(author["displayName"].ToString(),
                            snip["displayMessage"].ToString(),
                            author["profileImageUrl"].ToString(),
                            snip["publishedAt"].ToString());
                    }
                    // wait a little before next query.
                    // yield return new WaitForSeconds(json["pollingIntervalMillis"].AsFloat/1000);
                    yield return new WaitForSeconds(pullEveryXSeconds);
                }
            }
        }
    }
}