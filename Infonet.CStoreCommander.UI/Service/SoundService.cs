using Infonet.CStoreCommander.EntityLayer;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Service
{
    public class SoundService
    {
        private MediaPlaybackList _playbackList = new MediaPlaybackList();
        private MediaElement _player = new MediaElement();
        private MediaPlayer _mediaPlayer = BackgroundMediaPlayer.Current;
        private MediaPlayer _callingMediaPlayer = BackgroundMediaPlayer.Current;
        private MediaSource _stopSoundMediaSource;
        private MediaSource _callingSoundMediaSource;
        private DispatcherTimer _stopSoundTimer;
        private DispatcherTimer _callingSoundTimer;
        private List<string> _callingPumps = new List<string>();
        private string _currentCallingPump;

        string SoundFolderLocation = "ms-appx:///Sounds/";
        public readonly InfonetLog _log = InfonetLogManager.GetLogger<SoundService>();

        public int StoppedSoundCount = 0;
        private static readonly Lazy<SoundService> _lazy = new Lazy<SoundService>(() => new SoundService());
        public static SoundService Instance => _lazy.Value;

        public List<Sounds> Sounds;

        public void SetStopSoundToMedia()
        {
            var file = GetFileWithFileNumber(SoundTypes.stopped, string.Empty);
            var filePath = Path.Combine(SoundFolderLocation, file);
            _stopSoundMediaSource = MediaSource.CreateFromUri(new Uri(filePath));
            _mediaPlayer.Source = _stopSoundMediaSource;
            SetupStopSoundTimer();
        }

        private void SetupStopSoundTimer()
        {
            if (_stopSoundTimer == null)
            {
                _stopSoundTimer = new DispatcherTimer();
            }

            _stopSoundTimer.Interval = new TimeSpan(0, 0, 3);
            _stopSoundTimer.Tick -= StopSoundTimerTick;
            _stopSoundTimer.Tick += StopSoundTimerTick;
            _stopSoundTimer.Start();
        }

        private void SetupCallingSoundTimer()
        {
            if (_callingSoundTimer == null)
            {
                _callingSoundTimer = new DispatcherTimer();
            }

            // Calling this here to avoid here delay in first sound
            if (_mediaPlayer.CurrentState != MediaPlayerState.Playing)
            {
                CallingSoundTimerTick(null, null);
            }

            _callingSoundTimer.Interval = new TimeSpan(0, 0, 3);
            _callingSoundTimer.Tick -= CallingSoundTimerTick;
            _callingSoundTimer.Tick += CallingSoundTimerTick;
            _callingSoundTimer.Start();
        }

        private void CallingSoundTimerTick(object sender, object e)
        {
            if (_callingPumps?.Count > 0)
            {
                if (string.IsNullOrEmpty(_currentCallingPump))
                {
                    _currentCallingPump = _callingPumps.FirstOrDefault();
                }
                else
                {
                    var index = _callingPumps.IndexOf(_currentCallingPump);
                    _currentCallingPump = _callingPumps.ElementAt((index + 1) % _callingPumps.Count);
                }
                var file = GetFileWithFileNumber(SoundTypes.calling, _currentCallingPump);
                var filePath = Path.Combine(SoundFolderLocation, file);
                _callingSoundMediaSource = MediaSource.CreateFromUri(new Uri(filePath));
                _mediaPlayer.Source = _callingSoundMediaSource;
                _mediaPlayer.Play();
            }
        }

        private void StopSoundTimerTick(object sender, object e)
        {
            if (StoppedSoundCount > 0 && _callingPumps.Count == 0)
            {
                SoundService.Instance.SetStopSoundToMedia();
                _mediaPlayer.Play();
            }
        }

        public void PlaySoundFile(SoundTypes fileName, string fileNumber)
        {
            try
            {
                var file = GetFileWithFileNumber(fileName, fileNumber);
                SetFileToMediaElement(file);
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
            }
        }

        public void PlaySoundFile(SoundTypes fileName)
        {
            try
            {
                string file = fileName.Equals(SoundTypes.resume) ?
                    GetFileName(SoundTypes.stopped) : GetFileName(fileName);

                if (fileName.Equals(SoundTypes.resume))
                {
                    _mediaPlayer.Play();
                }
                else if (!fileName.Equals(SoundTypes.stopped))
                {
                    SetFileToMediaElement(file);
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
            }
        }

        private void SetFileToMediaElement(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                RemovedPlayedFiles();
                var filePath = Path.Combine(SoundFolderLocation, file);
                _playbackList.Items.Add(GetMediaElement(filePath));
                _player.SetPlaybackSource(_playbackList);
                _player.Play();
            }
        }

        private void RemovedPlayedFiles()
        {
            if (_playbackList.Items != null)
            {
                for (int i = _playbackList.Items.Count - 1; i >= 0; i--)
                {
                    var media = _playbackList.Items.ElementAt(i);

                    if (media.Source.State == MediaSourceState.Opened ||
                       media.Source.State == MediaSourceState.Failed)
                    {
                        _playbackList.Items.Remove(media);
                    }
                }
            }
        }

        private string GetFileName(SoundTypes file)
        {
            return (from s in Sounds
                    where s.Name.ToLower().Equals(file.ToString().ToLower())
                    select s.File).FirstOrDefault();

        }

        private string GetFileWithFileNumber(SoundTypes fileType, string fileNumber)
        {
            var fileName = GetFileName(fileType);

            if (!string.IsNullOrEmpty(fileNumber))
            {
                var formatIndex = fileName.IndexOf('.');
                fileName = fileName.Insert(formatIndex, fileNumber);
            }

            return fileName;
        }

        private static MediaPlaybackItem GetMediaElement(string uri)
        {
            var mediaSource = MediaSource.CreateFromUri(new Uri(uri));
            return new MediaPlaybackItem(mediaSource);
        }

        private bool IfFileExist(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;
            string filePath = Path.Combine(folder.Path, fileName);

            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void PlaySoundFileForCalling(string pumpId)
        {
            try
            {
                var file = GetFileWithFileNumber(SoundTypes.calling, pumpId);
                if (!_callingPumps.Contains(pumpId))
                {
                    _callingPumps.Add(pumpId);
                }
                SetupCallingSoundTimer();
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
            }
        }

        internal void RemoveCallingQueue(string pumpId)
        {
            _callingPumps?.Remove(pumpId);
        }
    }
}
