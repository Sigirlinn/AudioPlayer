﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using System.Windows.Forms;

namespace Аудиоплеер_2.Classes
{
    public class Audio
    {
        public bool cycle = false;
        public event EventHandler End;
        public event EventHandler Begin;
        int _stream;
        string[] _FileName;
        string[] _FilePath;
        string[] _time;
        int _number = 0;
        //для события конца песни
        SYNCPROC _sEndMus;


        public Audio()
        {
            // Для Конструктора MainWindow, чтобы форма показывалась
            try
            {
                Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SetLen(double len){
            Bass.BASS_ChannelSetPosition(_stream, len);

        }

        public double LenNow()
        {
            double len = Bass.BASS_ChannelBytes2Seconds(
                        _stream,
                        Bass.BASS_ChannelGetPosition(_stream));
            return len;
        }

        public double LenAll()
        {
            double len = Bass.BASS_ChannelBytes2Seconds(
                    _stream,
                    Bass.BASS_ChannelGetLength(_stream));
            return len;
        }

        public void MemberFile(string []Name, string[]Path)
        {
            _FileName = new string[Name.Length];
            _FilePath = new string[Name.Length];
            _time = new string[Name.Length];
            int bufstream = 0;
            double lengFile = 0;
            for (int i = 0; i < Name.Length; i++)
            {
                _FileName[i] = Name[i];
                _FilePath[i] = Path[i];
                bufstream = Bass.BASS_StreamCreateFile(
                    _FilePath[i],
                    0,
                    0,
                    BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN
                );
                lengFile = Bass.BASS_ChannelBytes2Seconds(
                    bufstream,
                    Bass.BASS_ChannelGetLength(bufstream));
                _time[i] = Long(lengFile);
                Bass.BASS_StreamFree(bufstream);
            }
        }


        private string Long(double count)
        {
            int hour = (int)count / 3600;
            int min = (int)count / 60;
            int sec = (int)count % 60;

            string hs = "", ms = "", ss = "";

            if (sec < 10) ss = "0";
            if (min < 10) ms = "0";
            if (hour < 10) hs = "0";

            string rez = "";
            if (hour <= 0)
                rez = "00:" + ms + min.ToString() + ":" + ss + sec.ToString();
            else 
                rez = hs + hour.ToString() + ":" + ms + min.ToString() + ":" + ss + sec.ToString();
            return rez;        
        }

        public void CreateStream(int number)
        {
            _number = number;
            if (_stream != 0)
            {
                Bass.BASS_ChannelStop(_stream);
                Bass.BASS_StreamFree(_stream);
            }
            _stream = Bass.BASS_StreamCreateFile(
                _FilePath[number], 
                0, 
                0,
                BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);
            double lengFile = Bass.BASS_ChannelBytes2Seconds(
                _stream, 
                Bass.BASS_ChannelGetLength(_stream));

            _sEndMus = new SYNCPROC(EndMusic);
            Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_ONETIME, 0, _sEndMus, IntPtr.Zero);
        }

        private void EndMusic(int handle, int channel, int data, IntPtr user)
        {
            if (End != null) End(this, new EventArgs());
            _number++;
            if (_number >= _FileName.Length)
            {
                _number = 0;
            }
            CreateStream(_number);

            if (_number == 0 && !cycle) return;

            Play();
        }

        public void Play()
        {
            if (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                Bass.BASS_ChannelPause(_stream);
                if (End != null) End(this, new EventArgs());
            }
            else if ((Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PAUSED) ||
                (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_STOPPED))
            {
                Bass.BASS_ChannelPlay(_stream, false);
                if (Begin != null) Begin(this, new EventArgs());
            }
        }

        public string[] GetShortFileName()
        {
            if (_FileName != null) return _FileName;
            else return null;

        }

        public string[] GetShortFileTime()
        {
            if (_time != null) return _time;
            else return null;

        }

        public void SetNumber(int delta){
            _number += delta;
            if (_number >= _FileName.Length) _number = 0;
            if (_number < 0) _number = _FileName.Length - 1;
        }

        public int GetNumber()
        {
            return _number;
        }

        public string GetTimeNow()
        {
            if (_stream != 0)
            {
                double len = Bass.BASS_ChannelBytes2Seconds(
                        _stream,
                        Bass.BASS_ChannelGetPosition(_stream));
                return Long(len);
            }
            else return "00:00";
        }

        public void GetFFT(float[] fft)
        {
            if (_stream != 0 && fft.Length == 4096)
            {
                Bass.BASS_ChannelGetData(_stream, fft, (int)BASSData.BASS_DATA_FFT8192);
            }
        }

        public void SetVolume(float value)
        {
            Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, value/100);

        }
    }
}
