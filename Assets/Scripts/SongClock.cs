using UnityEngine;
using UnityEngine.Video;

public class SongClock : MonoBehaviour
{
    public static SongClock Instance { get; private set; }

    [SerializeField] private VideoPlayer videoPlayer;

    private bool _ready = false;

    void Awake() => Instance = this;

    void Start()
    {
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepared;
    }

    private void OnPrepared(VideoPlayer vp)
    {
        _ready = true;
        vp.Play();
    }

    public double GetSongTime()
    {
        if (!_ready) return 0;
        return videoPlayer.time;
    }
}