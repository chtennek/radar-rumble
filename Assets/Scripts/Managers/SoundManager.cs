﻿using UnityEngine;using UnityEngine.SceneManagement;using System.Collections.Generic;public class SoundManager : MonoBehaviour {    // Properties    private bool _isInitialized = false;    private GameObject _cachedGameObject;    private Transform _cameraTransform;    private float _volume = 1.0f;    // Audio sources    private Dictionary<string, AudioClip> _audioClips;    private AudioSource[] _audioSources;    private float[] _oldVolume;    private float[] _newVolume;    private float[] _transitionStart;    private float[] _transitionTime;    // Music channel    private int _musicChannel = -1;    public int MusicChannel {        get { return _musicChannel; }        set { _musicChannel = value; }    }    // =====================================================================================    // Construction    // =====================================================================================    /// <summary> Initialize this instance. </summary>    public void Initialize() {        // Initialize sound manager        _isInitialized = true;        // Cache the game object        _cachedGameObject = this.gameObject;        _audioClips = new Dictionary<string, AudioClip>();        // Create audio sources        _audioSources = new AudioSource[16];        for (int i = 0; i < _audioSources.Length; ++i) {            _audioSources[i] = (AudioSource)_cachedGameObject.AddComponent(typeof(AudioSource));        }        // Declaring arrays        _oldVolume = new float[_audioSources.Length];        _newVolume = new float[_audioSources.Length];        _transitionStart = new float[_audioSources.Length];        _transitionTime = new float[_audioSources.Length];        // Initializing arrays        for (int i = 0; i < _audioSources.Length; ++i) {            _oldVolume[i] = 1.0f;            _newVolume[i] = 1.0f;            _transitionStart[i] = 0.0f;            _transitionTime[i] = 0.00001f;        }    }    void OnEnable() {        SceneManager.sceneLoaded += OnLevelFinishedLoading;    }    void OnDisable() {        SceneManager.sceneLoaded -= OnLevelFinishedLoading;    }    /// <summary> Whenever a new level is loaded, check for a new camera instance. </summary>    /// <param name='level'> Current Level. </param>    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {        // Exit conditions        if (!_isInitialized) { return; }        // Check for a new camera and set it        AudioListener audioListener = GameObject.FindObjectOfType(typeof(AudioListener)) as AudioListener;        if (audioListener != null) {            GameObject camera = audioListener.gameObject;            if (camera != null) {                _cameraTransform = camera.transform;                this.transform.position = _cameraTransform.position;            }        }        // Stop all audio sources if they're not looping        for (int i = 0; i < _audioSources.Length; i++) {            if (!_audioSources[i].loop) {                _audioSources[i].Stop();            }        }    }    /// <summary> Update the Sound Manager. </summary>    void Update() {        // Exit conditions        if (!_isInitialized) { return; }        // Update the position of the game object        if (_cameraTransform != null) {            this.transform.position = _cameraTransform.position;        }        // Transition the volume of the audio clips        for (int i = 0; i < _audioSources.Length; i++) {            // Tween the volume of the audio clip            _audioSources[i].volume = Mathf.Lerp(_oldVolume[i], _newVolume[i], Mathf.Min(1.0f, (Time.time - _transitionStart[i]) / _transitionTime[i]));            // When a sound reaches zero volume            if (_newVolume[i] <= 0 && _audioSources[i].volume <= 0 && _audioSources[i].isPlaying) {                if (i == _musicChannel) { _audioSources[i].Pause(); } // Pause it if it's music                else { _audioSources[i].Stop(); } // Stop it if it's a sound effect            }            // Restart music when unmuted            else if (i == _musicChannel && _newVolume[i] > 0 && !_audioSources[i].isPlaying) {                _audioSources[i].Play();            }        }    }    // =====================================================================================    // Volume    // =====================================================================================    /// <summary> Gets or sets the global volume. </summary>    /// <value> The volume. </value>    public float Volume {        get { return _volume; }        set {            _volume = Mathf.Clamp(value, 0.0f, 1.0f);            // Change volume for all audio sources            for (int i = 0; i < _audioSources.Length; i++) {                AudioSource audioSource = _audioSources[i];                if (audioSource.clip) {                    _oldVolume[i] = audioSource.volume;                    _newVolume[i] = _volume;                }            }        }    }    /// <summary> Immediately sets volume of the specified channel. </summary>    /// <param name="channelNum"> Channel number. </param>    /// <param name="newVol"> New volume setting, 0.0f to 1.0f </param>    public void SetVolume(int channelNum, float newVol) {        newVol = Mathf.Clamp(newVol, 0.0f, 1.0f) * _volume;        _oldVolume[channelNum] = newVol;        _newVolume[channelNum] = newVol;        _audioSources[channelNum].volume = newVol;    }    /// <summary> Linearly interpolates volume of the specified channel    /// from current value to the new value during the specified time. </summary>    /// <param name="channelNum"> Channel number. </param>    /// <param name="newVol"> New volume setting, 0.0f to 1.0f </param>    /// <param name="time"> Time in seconds </param>    public void SetVolume(int channelNum, float newVol, float time) {        newVol = Mathf.Clamp(newVol, 0.0f, 1.0f) * _volume;        _oldVolume[channelNum] = _audioSources[channelNum].volume;        _newVolume[channelNum] = newVol;        _transitionStart[channelNum] = Time.time;        _transitionTime[channelNum] = time;    }    /// <summary> Immediately sets volume of the specified clip.    /// This will effect the settings for all channels playing the given clip. </summary>    /// <param name="audioClip"> Audio clip. </param>    /// <param name="newVol"> New volume setting, 0.0f to 1.0f </param>    public void SetVolume(AudioClip audioClip, float newVol) {        newVol = Mathf.Clamp(newVol, 0.0f, 1.0f) * _volume;        for (int i = 0; i < _audioSources.Length; i++) {            AudioSource s = _audioSources[i];            if (s.clip == audioClip) {                _oldVolume[i] = newVol;                _newVolume[i] = newVol;                s.volume = newVol;            }        }    }    /// <summary> Linearly interpolates volume of the specified clip    /// from current value to the new value during the specified time.    /// This will effect the settings for all channels playing the given clip. </summary>    /// <param name="audioClip"> Audio clip. </param>    /// <param name="newVol"> New volume setting, 0.0f to 1.0f </param>    /// <param name="time"> Time in seconds </param>    public void SetVolume(AudioClip audioClip, float newVol, float time) {        newVol = Mathf.Clamp(newVol, 0.0f, 1.0f) * _volume;        for (int i = 0; i < _audioSources.Length; i++) {            AudioSource s = _audioSources[i];            if (s.clip == audioClip) {                _oldVolume[i] = s.volume;                _newVolume[i] = newVol;                _transitionStart[i] = Time.time;                _transitionTime[i] = time;            }        }    }    // =====================================================================================    // Play    // =====================================================================================    /// <summary> Loads a sound and plays the audioclip on any free channel. </summary>    /// <param name="audioName"> Name of the audio. </param>    /// <param name="loop"> Loop setting. </param>    /// <returns> Number of the assigned channel. -1 if no channel is found. </returns>    public int PlaySound(string audioName, float volume, bool loop) {        // If sound does not exist, load it first        if (!_audioClips.ContainsKey(audioName)) {            AudioClip audioClip = (AudioClip)Resources.Load("Sounds/" + audioName);            _audioClips.Add(audioName, audioClip);        }        // Play the sound        return PlaySound(_audioClips[audioName], volume, loop);    }    /// <summary> Plays given audio clip on any free channel. </summary>    /// <param name="audioClip"> Audio clip. </param>    /// <param name="volume"> Volume. </param>    /// <param name="loop"> Loop setting. </param>    /// <returns> Number of the assigned channel. -1 if no channel is found. </returns>    public int PlaySound(AudioClip audioClip, float volume, bool loop) {        for (int channelNum = 0; channelNum < _audioSources.Length; channelNum++) {            AudioSource audioSource = _audioSources[channelNum];            if (!audioSource.isPlaying) {                // Play audio clip                audioSource.clip = audioClip;                audioSource.loop = loop;                audioSource.Play();                SetVolume(channelNum, volume * _volume);                return channelNum;            }        }        return -1;    }    /// <summary> Plays given audio clip on any free channel included in the mask. </summary>    /// <param name="audioClip"> Audio clip. </param>    /// <param name="mask"> Channel mask, e.g. to specify 0th, 3rd and 11th channel, use 0x0809 </param>    /// <param name="volume"> Volume. </param>    /// <param name="loop"> Loop setting. </param>    /// <returns> Number of the assigned channel. -1 if no channel is found. </returns>    public int PlaySound(AudioClip audioClip, int mask, float volume, bool loop) {        for (int channelNum = 0; channelNum < _audioSources.Length; channelNum++) {            if ((mask & (1 << channelNum)) > 0 && !_audioSources[channelNum].isPlaying) {                // Play audio clip                _audioSources[channelNum].clip = audioClip;                _audioSources[channelNum].loop = loop;                _audioSources[channelNum].Play();                SetVolume(channelNum, volume * _volume);                return channelNum;            }        }        return -1;    }    // =====================================================================================    //  Resume    // =====================================================================================    /// <summary>    /// Plays a unique sound. Makes sure that multiples of the sound are not played simultaneously.    /// </summary>    /// <returns>The unique sound.</returns>    /// <param name="audioName">Audio name.</param>    /// <param name="volume">Volume.</param>    /// <param name="loop">If set to <c>true</c> loop.</param>    public int PlayUniqueSound(string audioName, float volume, bool loop) {        if (_audioClips.ContainsKey(audioName)) {            AudioClip audioClip = _audioClips[audioName];            for (int i = 0; i < _audioSources.Length; i++) {                AudioSource audioSource = _audioSources[i];                if (audioSource.isPlaying && audioSource.clip == audioClip) {                    return i; // Sound is already playing, so don't play another sound                }            }        }        // Otherwise, sound isn't playing, so play it on a new channel        return PlaySound(audioName, volume, loop);    }    // =====================================================================================    // Stop    // =====================================================================================    /// <summary> Stops a certain sound by its name. </summary>    /// <param name="audioName"> Name of the audio clip. </param>    public void StopSound(string audioName) {        if (_audioClips.ContainsKey(audioName)) {            foreach (AudioSource audioSource in _audioSources) {                if (audioSource.clip == _audioClips[audioName] && audioSource.isPlaying) {                    audioSource.Stop();                }            }        }    }    /// <summary> Stops all channels playing the given clip. </summary>    /// <param name="audioClip">Audio clip</param>    public void StopClip(AudioClip audioClip) {        foreach (AudioSource s in _audioSources) {            if (s.clip == audioClip && s.isPlaying) {                s.Stop();            }        }    }    /// <summary> Stops the given channel. </summary>    /// <param name="channelNum"> Channel number. </param>    public void StopChannel(int channelNum) {        _audioSources[channelNum].Stop();    }    /// <summary> Stops all sound channels. </summary>    /// <param name="channelNum"> Channel number. </param>    public void StopAllChannels() {        for (int i = 0; i < _audioSources.Length; i++) {            _audioSources[i].Stop();        }    }    // =====================================================================================    // Destruction    // =====================================================================================    private void OnDestroy() {        // Exit conditions        if (!_isInitialized) { return; }        _audioClips.Clear();    }}