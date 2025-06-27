using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;
    //needs to be changed when V3 comes out in December
    //Can also be changed anytime for the itch.io page if moved.
    private string publicLeaderboardKey =
        "2f02a04435d7720ae9ac24f61304cbb951853fdc78632cbeaa310fc1254093c4";

    // Start is called before the first frame update
    void Start()
    {
        GetLeaderboard();
    }

    //function for getting the leaderboard updated names and scores
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Capacity;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }
    //function to add in the new name and score
    public void setLeaderboardentry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username,
        score, ((msg) =>
        {
            //username.Substring(0, 15); //limit name to 20 characters
            //if we want to add a list of names that can't be inputted then
            //if(System.Array.indexOf (bannedWords, name) != -1) return;
            GetLeaderboard();
        }));
    }
}
